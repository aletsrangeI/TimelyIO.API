using Common.Services;
using FluentAssertions;
using Xunit;

namespace TimelyIO.API.UnitTests.Services;

public class WhatsAppTemplateBuilderTests
{
    [Theory]
    [InlineData("5512345678", "525512345678")]
    [InlineData("+52 55 1234 5678", "525512345678")]
    [InlineData("+52 (55) 1234-5678", "525512345678")]
    [InlineData("5215512345678", "5215512345678")]
    public void SanitizarTelefono_NormalizaFormatoCorrectamente(string entrada, string esperado)
    {
        // Act
        var resultado = WhatsAppTemplateBuilder.SanitizarTelefono(entrada);

        // Assert
        resultado.Should().Be(esperado);
    }

    [Fact]
    public void MensajeConfirmacionInmediata_ContieneVariablesYEnlace()
    {
        // Arrange
        var cliente = "Sofía Valenzuela";
        var servicio = "Limpieza Dental & Profilaxis";
        var profesional = "Dra. Elena Mora";
        var negocio = "Clínica Dental Alborada";
        var fecha = new DateTime(2026, 10, 15, 10, 0, 0);
        var token = Guid.NewGuid();

        // Act
        var mensaje = WhatsAppTemplateBuilder.MensajeConfirmacionInmediata(
            cliente, servicio, profesional, negocio, fecha, token, "https://timely.io");

        // Assert
        mensaje.Should().Contain(cliente);
        mensaje.Should().Contain(servicio);
        mensaje.Should().Contain(profesional);
        mensaje.Should().Contain(negocio);
        mensaje.Should().Contain(token.ToString());
        mensaje.Should().Contain("https://timely.io/mi-cita/");
    }

    [Fact]
    public void MensajeRecordatorio24h_ContieneSolicitudDeConfirmacion()
    {
        // Arrange
        var cliente = "Juan Pérez";
        var servicio = "Consulta General";
        var negocio = "Centro Médico";
        var fecha = new DateTime(2026, 10, 16, 11, 30, 0);
        var token = Guid.NewGuid();

        // Act
        var mensaje = WhatsAppTemplateBuilder.MensajeRecordatorio24h(
            cliente, servicio, negocio, fecha, token);

        // Assert
        mensaje.Should().Contain(cliente);
        mensaje.Should().Contain(servicio);
        mensaje.Should().Contain(token.ToString());
        mensaje.Should().Contain("Recordatorio de Cita");
    }

    [Fact]
    public void GenerarUrlWhatsApp_GeneraLinkValidoParaWaMe()
    {
        // Arrange
        var tel = "5512345678";
        var texto = "Hola Sofía, tu cita está lista.";

        // Act
        var url = WhatsAppTemplateBuilder.GenerarUrlWhatsApp(tel, texto);

        // Assert
        url.Should().StartWith("https://wa.me/525512345678?text=");
        url.Should().Contain("Hola%20Sof");
    }
}
