using Domain.Entities;
using DTO.Cita;
using ErrorOr;
using Interface.Persistence;
using Interface.UseCases;
using Common;
using UseCases.Common.Mapping;
using Validator;

using Interface.Services;

namespace UseCases.Citas;

public class CitaApplication : ICitaApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppMapper _mapper;
    private readonly CitaDTOValidator _validationRules;
    private readonly IAppLogger<CitaApplication> _logger;
    private readonly ITurnstileService? _turnstileService;

    public CitaApplication(
        IUnitOfWork unitOfWork,
        IAppMapper mapper,
        CitaDTOValidator validationRules,
        IAppLogger<CitaApplication> logger,
        ITurnstileService? turnstileService = null)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
        _logger = logger;
        _turnstileService = turnstileService;
    }

    public async Task<ErrorOr<bool>> InsertAsync(CitaDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Cita>(dto);
        var success = await _unitOfWork.Citas.InsertAsync(entity);
        return success ? true : Error.Failure("Cita.InsertFailed", "No se pudo crear el registro.");
    }

    public async Task<ErrorOr<bool>> UpdateAsync(CitaDTO dto, CancellationToken cancellationToken = default)
    {
        var validation = await _validationRules.ValidateAsync(dto, cancellationToken);
        if (!validation.IsValid)
        {
            var firstError = validation.Errors[0];
            return Error.Validation(firstError.PropertyName, firstError.ErrorMessage);
        }

        var entity = _mapper.Map<Cita>(dto);
        var success = await _unitOfWork.Citas.UpdateAsync(entity);
        return success ? true : Error.NotFound("Cita.NotFound", "El registro especificado no existe o no pudo actualizarse.");
    }

    public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var success = await _unitOfWork.Citas.DeleteAsync(id);
        return success ? true : Error.NotFound("Cita.NotFound", "No se encontro el registro a eliminar.");
    }

    public async Task<ErrorOr<CitaDTO>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Citas.GetAsync(id);
        if (entity is null)
        {
            return Error.NotFound("Cita.NotFound", "El registro no fue encontrado.");
        }

        return _mapper.Map<CitaDTO>(entity);
    }

    public async Task<ErrorOr<IEnumerable<CitaDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Citas.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<CitaDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<IEnumerable<CitaDTO>>> GetAllWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Citas.GetAllWithPaginationAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<CitaDTO>>(entities);
        return ErrorOrFactory.From(dtos);
    }

    public async Task<ErrorOr<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        var count = await _unitOfWork.Citas.CountAsync();
        return count;
    }

    public async Task<ErrorOr<CitaDTO>> GetByTokenAsync(Guid token, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Citas.GetAllAsync();
        var entity = entities.FirstOrDefault(c => c.TokenAccesoCliente == token);
        if (entity is null)
        {
            return Error.NotFound("Cita.NotFound", "No se encontró la cita con el token proporcionado.");
        }

        return _mapper.Map<CitaDTO>(entity);
    }

    public async Task<ErrorOr<bool>> AccionClienteAsync(Guid token, string accion, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.Citas.GetAllAsync();
        var entity = entities.FirstOrDefault(c => c.TokenAccesoCliente == token);
        if (entity is null)
        {
            return Error.NotFound("Cita.NotFound", "La cita no existe o el token es inválido.");
        }

        var lowerAccion = (accion ?? "").Trim().ToLower();
        if (lowerAccion == "confirmar")
        {
            entity.Estado = Domain.Enums.EstadoCita.Confirmada;
        }
        else if (lowerAccion == "cancelar")
        {
            entity.Estado = Domain.Enums.EstadoCita.Cancelada;
            entity.CanceladoPor = Domain.Enums.OrigenCancelacion.Cliente;
            entity.MotivoCancelacion = "Cancelada por el cliente desde enlace de WhatsApp";
        }
        else
        {
            return Error.Validation("Accion.Invalida", "La acción solicitada no es válida. Use 'confirmar' o 'cancelar'.");
        }

        var success = await _unitOfWork.Citas.UpdateAsync(entity);
        return success ? true : Error.Failure("Cita.UpdateFailed", "No se pudo actualizar el estado de la cita.");
    }

    public async Task<ErrorOr<PublicReservaResponseDTO>> RegistrarReservaPublicaAsync(PublicReservaDTO dto, string? clientIp = null, CancellationToken cancellationToken = default)
    {
        // 1. Filtro Honeypot: si el bot llena este campo invisible, rechazar inmediatamente
        if (!string.IsNullOrWhiteSpace(dto.Honeypot))
        {
            _logger.LogWarning("[Security] Honeypot activado por bot desde IP: {IP}", clientIp ?? "desconocida");
            return Error.Validation("Security.BotDetected", "Solicitud rechazada por filtros de seguridad.");
        }

        // 2. Filtro Cloudflare Turnstile
        if (_turnstileService != null)
        {
            var isValidToken = await _turnstileService.VerifyTokenAsync(dto.TurnstileToken, clientIp, "reserva", cancellationToken);
            if (!isValidToken)
            {
                _logger.LogWarning("[Security] Verificación de Turnstile fallida desde IP: {IP}", clientIp ?? "desconocida");
                return Error.Validation("Security.TurnstileFailed", "No se pudo validar el captcha de seguridad. Por favor intenta de nuevo.");
            }
        }

        // 3. Validar Datos Obligatorios del Cliente
        if (string.IsNullOrWhiteSpace(dto.NombreCliente) || dto.NombreCliente.Trim().Length < 3)
        {
            return Error.Validation("NombreCliente", "El nombre completo es requerido y debe tener al menos 3 caracteres.");
        }

        var cleanPhone = (dto.TelefonoWhatsapp ?? "").Trim();
        var digitsOnly = new string(cleanPhone.Where(char.IsDigit).ToArray());
        if (digitsOnly.Length < 10)
        {
            return Error.Validation("TelefonoWhatsapp", "El teléfono de WhatsApp debe tener al menos 10 dígitos válidos.");
        }

        // 4. Validar Fecha y Hora
        if (dto.FechaHoraInicioUtc <= DateTime.UtcNow.AddMinutes(15))
        {
            return Error.Validation("FechaHoraInicioUtc", "La cita debe reservarse con al menos 15 minutos de anticipación.");
        }

        if (dto.FechaHoraInicioUtc > DateTime.UtcNow.AddMonths(4))
        {
            return Error.Validation("FechaHoraInicioUtc", "No es posible agendar con más de 4 meses de antelación.");
        }

        // 5. Validar Negocio y Servicio (autoritativo desde Base de Datos)
        var negocio = await _unitOfWork.Negocios.GetAsync(dto.NegocioId);
        if (negocio == null || !negocio.Active)
        {
            return Error.NotFound("Negocio.NotFound", "El negocio seleccionado no existe o no se encuentra activo.");
        }

        var servicio = await _unitOfWork.Servicios.GetAsync(dto.ServicioId);
        if (servicio == null || !servicio.Active || servicio.NegocioId != dto.NegocioId)
        {
            return Error.NotFound("Servicio.NotFound", "El servicio solicitado no es válido para este negocio.");
        }

        // 6. Asignar Profesional
        var profesionales = (await _unitOfWork.Profesionales.GetAllAsync())
            .Where(p => p.NegocioId == dto.NegocioId && p.Active)
            .ToList();

        if (!profesionales.Any())
        {
            return Error.NotFound("Profesionales.NotFound", "No hay especialistas disponibles en este negocio.");
        }

        int targetProfId;
        Domain.Entities.Profesional? assignedProf = null;

        if (dto.ProfesionalId.HasValue && dto.ProfesionalId.Value > 0)
        {
            assignedProf = profesionales.FirstOrDefault(p => p.Id == dto.ProfesionalId.Value);
            if (assignedProf == null)
            {
                return Error.NotFound("Profesional.NotFound", "El especialista seleccionado no existe o no está activo.");
            }
            targetProfId = assignedProf.Id;
        }
        else
        {
            assignedProf = profesionales.First();
            targetProfId = assignedProf.Id;
        }

        // 7. Calcular Duración y Fechas de Inicio/Fin
        var duracionMin = servicio.DuracionMinutos > 0 ? servicio.DuracionMinutos : 45;
        var bufferMin = servicio.BufferMinutosPosterior >= 0 ? servicio.BufferMinutosPosterior : 15;
        var fechaFinUtc = dto.FechaHoraInicioUtc.AddMinutes(duracionMin);
        var fechaFinConBufferUtc = dto.FechaHoraInicioUtc.AddMinutes(duracionMin + bufferMin);

        // 8. Validar Colisión / Traslape de Citas en BD
        var existingCitas = (await _unitOfWork.Citas.GetAllAsync())
            .Where(c => c.ProfesionalId == targetProfId && c.Active && c.Estado != Domain.Enums.EstadoCita.Cancelada)
            .ToList();

        bool hasOverlap = existingCitas.Any(c =>
            dto.FechaHoraInicioUtc < c.FechaHoraFinConBufferUtc && fechaFinConBufferUtc > c.FechaHoraInicioUtc);

        if (hasOverlap)
        {
            return Error.Conflict("Cita.HorarioOcupado", "El horario seleccionado ya no se encuentra disponible. Por favor elige otro horario.");
        }

        // 9. Resolver o Crear Cliente
        var existingClients = (await _unitOfWork.Clientes.GetAllAsync())
            .Where(c => c.NegocioId == dto.NegocioId && c.Active)
            .ToList();

        var clientMatch = existingClients.FirstOrDefault(c =>
            new string((c.TelefonoWhatsapp ?? "").Where(char.IsDigit).ToArray()) == digitsOnly);

        int clienteId;
        if (clientMatch != null)
        {
            clienteId = clientMatch.Id;
        }
        else
        {
            var nuevoCliente = new Cliente
            {
                NegocioId = dto.NegocioId,
                NombreCompleto = dto.NombreCliente.Trim(),
                TelefonoWhatsapp = cleanPhone,
                Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim(),
                NotasGenerales = string.IsNullOrWhiteSpace(dto.Notas) ? null : dto.Notas.Trim(),
                Active = true,
                CreatedBy = "PortalReservaPublico",
                Created = DateTime.UtcNow
            };
            await _unitOfWork.Clientes.InsertAsync(nuevoCliente);
            clienteId = nuevoCliente.Id;
        }

        // 10. Construir Cita Blindada
        var requiereAnticipo = servicio.RequiereAnticipo && servicio.MontoAnticipo > 0;
        var montoAnticipo = requiereAnticipo ? servicio.MontoAnticipo : 0m;
        var estadoInicial = requiereAnticipo ? Domain.Enums.EstadoCita.PendienteValidacionPago : Domain.Enums.EstadoCita.Confirmada;
        var tokenAcceso = Guid.NewGuid();

        var nuevaCita = new Cita
        {
            NegocioId = dto.NegocioId,
            ClienteId = clienteId,
            ProfesionalId = targetProfId,
            ServicioId = servicio.Id,
            FechaHoraInicioUtc = dto.FechaHoraInicioUtc,
            FechaHoraFinUtc = fechaFinUtc,
            FechaHoraFinConBufferUtc = fechaFinConBufferUtc,
            Estado = estadoInicial,
            CostoTotal = servicio.PrecioTotal,
            MontoAnticipoRequerido = montoAnticipo,
            TokenAccesoCliente = tokenAcceso,
            Active = true,
            CreatedBy = "PortalReservaPublico",
            Created = DateTime.UtcNow
        };

        var inserted = await _unitOfWork.Citas.InsertAsync(nuevaCita);
        if (!inserted)
        {
            return Error.Failure("Cita.InsertFailed", "No se pudo registrar la reserva en la base de datos.");
        }

        return new PublicReservaResponseDTO
        {
            Id = nuevaCita.Id,
            TokenAccesoCliente = tokenAcceso,
            FechaHoraInicioUtc = nuevaCita.FechaHoraInicioUtc,
            FechaHoraFinUtc = nuevaCita.FechaHoraFinUtc,
            Estado = nuevaCita.Estado,
            CostoTotal = nuevaCita.CostoTotal,
            RequiereAnticipo = requiereAnticipo,
            MontoAnticipo = montoAnticipo,
            NombreServicio = servicio.Nombre,
            NombreProfesional = assignedProf.NombreCompleto
        };
    }
}
