using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence.Seeder;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        try
        {
            // Asegurar que la estructura exista
            await dbContext.Database.EnsureCreatedAsync();

            // 1. Sembrar Usuario Administrador por defecto
            if (!await dbContext.Personas.AnyAsync(p => p.Email == "admin@timely.io"))
            {
                var admin = new Persona
                {
                    Nombres = "Administrador",
                    Apellidos = "Timely",
                    Email = "admin@timely.io",
                    Password = "Admin123!*Password",
                    Telefono = "5215512345678",
                    FechaNacimiento = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc),
                    TypeId = 1,
                    Active = true,
                    CreatedBy = "SystemSeeder",
                    Created = DateTime.UtcNow
                };

                await unitOfWork.Personas.InsertAsync(admin);
            }

            // 2. Sembrar Negocio por defecto
            var negocio = await dbContext.Negocios.FirstOrDefaultAsync();
            if (negocio == null)
            {
                negocio = new Negocio
                {
                    NombreComercial = "Clínica Nórdica Timely",
                    Slug = "clinica-nordica",
                    TelefonoWhatsappOficial = "5215512345678",
                    DatosBancariosSPEI = "STP / CLABE 646180123456789012 / Beneficiario: Clinica Nordica",
                    PoliticaCancelacionHoras = 24,
                    ZonaHorariaId = "America/Mexico_City",
                    HorarioAtencionInicio = new TimeSpan(9, 0, 0),
                    HorarioAtencionFin = new TimeSpan(19, 0, 0),
                    MensajeFueraHorario = "Nuestro horario de atención es de 9:00 a 19:00 hrs. Tu cita se confirmará en cuanto nuestro equipo verifique tu solicitud.",
                    Active = true,
                    CreatedBy = "SystemSeeder",
                    Created = DateTime.UtcNow
                };

                await dbContext.Negocios.AddAsync(negocio);
                await dbContext.SaveChangesAsync();
            }

            // 3. Sembrar Servicios Iniciales
            if (!await dbContext.Servicios.AnyAsync())
            {
                var servicios = new List<Servicio>
                {
                    new()
                    {
                        NegocioId = negocio.Id,
                        Nombre = "Consulta Inicial & Diagnóstico",
                        Descripcion = "Evaluación clínica integral y plan de tratamiento personalizado.",
                        DuracionMinutos = 45,
                        BufferMinutosPosterior = 15,
                        PrecioTotal = 600m,
                        RequiereAnticipo = true,
                        MontoAnticipo = 300m,
                        DiasParaReenganche = 30,
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        NegocioId = negocio.Id,
                        Nombre = "Limpieza Dental & Profilaxis",
                        Descripcion = "Higiene profunda con ultrasonido y pulido con pasta profiláctica fluorada.",
                        DuracionMinutos = 60,
                        BufferMinutosPosterior = 15,
                        PrecioTotal = 850m,
                        RequiereAnticipo = true,
                        MontoAnticipo = 400m,
                        DiasParaReenganche = 60,
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        NegocioId = negocio.Id,
                        Nombre = "Control y Ajuste de Ortodoncia",
                        Descripcion = "Revisión mensual de arcos, ligaduras y evaluación de progreso dental.",
                        DuracionMinutos = 30,
                        BufferMinutosPosterior = 10,
                        PrecioTotal = 1200m,
                        RequiereAnticipo = false,
                        MontoAnticipo = 0m,
                        DiasParaReenganche = 28,
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    }
                };

                await dbContext.Servicios.AddRangeAsync(servicios);
                await dbContext.SaveChangesAsync();
            }

            // 4. Sembrar Profesionales Iniciales
            if (!await dbContext.Profesionales.AnyAsync())
            {
                var profesionales = new List<Profesional>
                {
                    new()
                    {
                        NegocioId = negocio.Id,
                        NombreCompleto = "Dra. Elena Mora",
                        TituloProfesional = "Odontopediatra & Estética",
                        ColorIdentificador = "#2B5741",
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        NegocioId = negocio.Id,
                        NombreCompleto = "Dr. Carlos Ruiz",
                        TituloProfesional = "Cirujano Maxilofacial",
                        ColorIdentificador = "#1E3F35",
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        NegocioId = negocio.Id,
                        NombreCompleto = "Dra. Sofía Castro",
                        TituloProfesional = "Ortodoncista Clínica",
                        ColorIdentificador = "#0F766E",
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    }
                };

                await dbContext.Profesionales.AddRangeAsync(profesionales);
                await dbContext.SaveChangesAsync();
            }

            // 5. Sembrar Clientes Iniciales
            if (!await dbContext.Clientes.AnyAsync())
            {
                var clientes = new List<Cliente>
                {
                    new()
                    {
                        NegocioId = negocio.Id,
                        NombreCompleto = "María Gómez Morales",
                        TelefonoWhatsapp = "5215598765432",
                        Email = "maria.gomez@gmail.com",
                        NotasGenerales = "Paciente recurrente, prefiere citas matutinas.",
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        NegocioId = negocio.Id,
                        NombreCompleto = "Jorge Hernández Luna",
                        TelefonoWhatsapp = "5215587654321",
                        Email = "jorge.hdz@outlook.com",
                        NotasGenerales = "Tratamiento de ortodoncia en curso.",
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        NegocioId = negocio.Id,
                        NombreCompleto = "Lucía Ramírez Torres",
                        TelefonoWhatsapp = "5215576543210",
                        Email = "lucia.rt@gmail.com",
                        NotasGenerales = "Consulta estética inicial.",
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    }
                };

                await dbContext.Clientes.AddRangeAsync(clientes);
                await dbContext.SaveChangesAsync();
            }

            // 6. Sembrar Citas de Muestra para el día de hoy
            if (!await dbContext.Citas.AnyAsync())
            {
                var clientesList = await dbContext.Clientes.ToListAsync();
                var profesionalesList = await dbContext.Profesionales.ToListAsync();
                var serviciosList = await dbContext.Servicios.ToListAsync();

                if (clientesList.Any() && profesionalesList.Any() && serviciosList.Any())
                {
                    var hoy = DateTime.UtcNow.Date;

                    var cita1 = new Cita
                    {
                        NegocioId = negocio.Id,
                        ClienteId = clientesList[0].Id,
                        ProfesionalId = profesionalesList[0].Id,
                        ServicioId = serviciosList[0].Id,
                        FechaHoraInicioUtc = hoy.AddHours(15), // 15:00 UTC (9:00 CDMX)
                        FechaHoraFinUtc = hoy.AddHours(15).AddMinutes(serviciosList[0].DuracionMinutos),
                        FechaHoraFinConBufferUtc = hoy.AddHours(15).AddMinutes(serviciosList[0].DuracionMinutos + serviciosList[0].BufferMinutosPosterior),
                        Estado = Domain.Enums.EstadoCita.Confirmada,
                        CostoTotal = serviciosList[0].PrecioTotal,
                        MontoAnticipoRequerido = serviciosList[0].MontoAnticipo,
                        TokenAccesoCliente = Guid.NewGuid(),
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    };

                    var cita2 = new Cita
                    {
                        NegocioId = negocio.Id,
                        ClienteId = clientesList[1].Id,
                        ProfesionalId = profesionalesList[1].Id,
                        ServicioId = serviciosList[1].Id,
                        FechaHoraInicioUtc = hoy.AddHours(17), // 17:00 UTC (11:00 CDMX)
                        FechaHoraFinUtc = hoy.AddHours(17).AddMinutes(serviciosList[1].DuracionMinutos),
                        FechaHoraFinConBufferUtc = hoy.AddHours(17).AddMinutes(serviciosList[1].DuracionMinutos + serviciosList[1].BufferMinutosPosterior),
                        Estado = Domain.Enums.EstadoCita.PendienteValidacionPago,
                        CostoTotal = serviciosList[1].PrecioTotal,
                        MontoAnticipoRequerido = serviciosList[1].MontoAnticipo,
                        TokenAccesoCliente = Guid.NewGuid(),
                        Active = true,
                        CreatedBy = "SystemSeeder",
                        Created = DateTime.UtcNow
                    };

                    await dbContext.Citas.AddRangeAsync(cita1, cita2);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DatabaseSeeder Error]: {ex.Message}");
        }
    }
}
