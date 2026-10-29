namespace Interface.External;

/// <summary>
/// Contrato agnóstico y desacoplado para el envío de notificaciones interactivas por WhatsApp.
/// Permite alternar entre Meta Cloud API, Twilio o un Mock local sin modificar la lógica de negocio.
/// </summary>
public interface IWhatsappNotificationService
{
    /// <summary>
    /// Envía un recordatorio interactivo con botones nativos de Confirmar y Reprogramar.
    /// </summary>
    Task<bool> EnviarRecordatorioInteractivoAsync(
        string telefonoDestino,
        string nombreCliente,
        string nombreNegocio,
        string nombreServicio,
        DateTime fechaHoraLocal,
        string urlPortalCita,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envía la confirmación inmediata de reserva con el resumen y enlace al portal.
    /// </summary>
    Task<bool> EnviarConfirmacionReservaAsync(
        string telefonoDestino,
        string nombreCliente,
        string nombreNegocio,
        string nombreServicio,
        DateTime fechaHoraLocal,
        string urlPortalCita,
        CancellationToken cancellationToken = default);
}
