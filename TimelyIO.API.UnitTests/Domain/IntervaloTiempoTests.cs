using Domain.Common;
using FluentAssertions;
using Xunit;

namespace TimelyIO.API.UnitTests.Domain;

public class IntervaloTiempoTests
{
    [Fact]
    public void Constructor_FechaFinMenorOIgualAInicio_LanzaArgumentException()
    {
        // Arrange
        var inicio = new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc);
        var fin = inicio; // Igual

        // Act
        Action accion = () => new IntervaloTiempo(inicio, fin);

        // Assert
        accion.Should().Throw<ArgumentException>()
            .WithMessage("*posterior a la fecha de inicio*");
    }

    [Fact]
    public void SeTraslapaCon_IntervalosTraslapados_RetornaTrue()
    {
        // Arrange: Cita 1 de 10:00 a 11:00, Cita 2 de 10:30 a 11:30
        var cita1 = new IntervaloTiempo(
            new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc));

        var cita2 = new IntervaloTiempo(
            new DateTime(2026, 9, 21, 10, 30, 0, DateTimeKind.Utc),
            new DateTime(2026, 9, 21, 11, 30, 0, DateTimeKind.Utc));

        // Act & Assert
        cita1.SeTraslapaCon(cita2).Should().BeTrue();
        cita2.SeTraslapaCon(cita1).Should().BeTrue();
    }

    [Fact]
    public void SeTraslapaCon_IntervalosConsecutivosSinTraslape_RetornaFalse()
    {
        // Arrange: Cita 1 de 10:00 a 11:00, Cita 2 de 11:00 a 12:00 (justo al terminar)
        var cita1 = new IntervaloTiempo(
            new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc));

        var cita2 = new IntervaloTiempo(
            new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 9, 21, 12, 0, 0, DateTimeKind.Utc));

        // Act & Assert
        cita1.SeTraslapaCon(cita2).Should().BeFalse();
        cita2.SeTraslapaCon(cita1).Should().BeFalse();
    }

    [Fact]
    public void SeTraslapaCon_IntervalosCompletamenteSeparados_RetornaFalse()
    {
        // Arrange: Cita 1 de 10:00 a 11:00, Cita 2 de 14:00 a 15:00
        var cita1 = new IntervaloTiempo(
            new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 9, 21, 11, 0, 0, DateTimeKind.Utc));

        var cita2 = new IntervaloTiempo(
            new DateTime(2026, 9, 21, 14, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 9, 21, 15, 0, 0, DateTimeKind.Utc));

        // Act & Assert
        cita1.SeTraslapaCon(cita2).Should().BeFalse();
        cita2.SeTraslapaCon(cita1).Should().BeFalse();
    }
}
