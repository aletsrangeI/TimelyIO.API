using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TimelyIO.API.UnitTests.Domain;

public class CitaAntiOverlapTests
{
    [Fact]
    public void ObtenerIntervaloConBuffer_CalculaIntervaloExactoConBuffer()
    {
        // Arrange
        var inicio = new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc);
        var fin = new DateTime(2026, 9, 21, 10, 45, 0, DateTimeKind.Utc);
        var finConBuffer = new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc); // 15 min buffer

        var cita = new Cita
        {
            Id = 1,
            NegocioId = 1,
            ClienteId = 10,
            ProfesionalId = 5,
            ServicioId = 2,
            FechaHoraInicioUtc = inicio,
            FechaHoraFinUtc = fin,
            FechaHoraFinConBufferUtc = finConBuffer
        };

        // Act
        var intervalo = cita.ObtenerIntervaloConBuffer();

        // Assert
        intervalo.InicioUtc.Should().Be(inicio);
        intervalo.FinUtc.Should().Be(finConBuffer);
        intervalo.DuracionMinutos.Should().Be(60);
    }

    [Fact]
    public void DosCitas_ConBufferSolapado_DetectanConflicto()
    {
        // Arrange: Cita A termina 10:45 con buffer hasta 11:00. Cita B quiere iniciar 10:50.
        var citaA = new Cita
        {
            FechaHoraInicioUtc = new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc),
            FechaHoraFinUtc = new DateTime(2026, 9, 21, 10, 45, 0, DateTimeKind.Utc),
            FechaHoraFinConBufferUtc = new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc)
        };

        var citaB = new Cita
        {
            FechaHoraInicioUtc = new DateTime(2026, 9, 21, 10, 50, 0, DateTimeKind.Utc),
            FechaHoraFinUtc = new DateTime(2026, 9, 21, 11, 35, 0, DateTimeKind.Utc),
            FechaHoraFinConBufferUtc = new DateTime(2026, 9, 21, 11, 50, 0, DateTimeKind.Utc)
        };

        // Act & Assert
        citaA.ObtenerIntervaloConBuffer().SeTraslapaCon(citaB.ObtenerIntervaloConBuffer()).Should().BeTrue();
    }

    [Fact]
    public void DosCitas_RespetandoElBufferExacto_NoSeTraslapan()
    {
        // Arrange: Cita A termina 10:45 con buffer hasta 11:00. Cita B arranca exactamente 11:00.
        var citaA = new Cita
        {
            FechaHoraInicioUtc = new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc),
            FechaHoraFinUtc = new DateTime(2026, 9, 21, 10, 45, 0, DateTimeKind.Utc),
            FechaHoraFinConBufferUtc = new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc)
        };

        var citaB = new Cita
        {
            FechaHoraInicioUtc = new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc),
            FechaHoraFinUtc = new DateTime(2026, 9, 21, 11, 45, 0, DateTimeKind.Utc),
            FechaHoraFinConBufferUtc = new DateTime(2026, 9, 21, 12, 0, 0, DateTimeKind.Utc)
        };

        // Act & Assert
        citaA.ObtenerIntervaloConBuffer().SeTraslapaCon(citaB.ObtenerIntervaloConBuffer()).Should().BeFalse();
    }
}
