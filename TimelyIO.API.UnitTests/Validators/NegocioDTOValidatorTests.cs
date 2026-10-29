using DTO.Negocio;
using FluentAssertions;
using Validator;
using Xunit;

namespace TimelyIO.API.UnitTests.Validators;

public class NegocioDTOValidatorTests
{
    private readonly NegocioDTOValidator _validator = new();

    [Fact]
    public void Validar_NegocioConDatosValidos_NoTieneErrores()
    {
        // Arrange
        var dto = new NegocioDTO
        {
            Id = 1,
            NombreComercial = "Clínica Dental Alborada",
            Slug = "clinica-dental-alborada",
            TelefonoWhatsappOficial = "5215512345678",
            PoliticaCancelacionHoras = 24,
            ZonaHorariaId = "America/Mexico_City"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validar_NombreComercialVacio_TieneError(string? nombre)
    {
        // Arrange
        var dto = new NegocioDTO
        {
            NombreComercial = nombre!,
            Slug = "clinica-alborada",
            TelefonoWhatsappOficial = "5215512345678"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(NegocioDTO.NombreComercial));
    }

    [Theory]
    [InlineData("Clinica Con Mayusculas")]
    [InlineData("slug_con_guiones_bajos")]
    [InlineData("slug con espacios")]
    [InlineData("slug@con!simbolos")]
    public void Validar_SlugFormatoInvalido_TieneError(string slugInvalido)
    {
        // Arrange
        var dto = new NegocioDTO
        {
            NombreComercial = "Clinica Test",
            Slug = slugInvalido,
            TelefonoWhatsappOficial = "5215512345678"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(NegocioDTO.Slug) && e.ErrorMessage.Contains("solo puede contener"));
    }

    [Theory]
    [InlineData("clinica-alborada")]
    [InlineData("taller-mecanico-2026")]
    [InlineData("barberia123")]
    public void Validar_SlugFormatoValido_EsAceptado(string slugValido)
    {
        // Arrange
        var dto = new NegocioDTO
        {
            NombreComercial = "Negocio Test",
            Slug = slugValido,
            TelefonoWhatsappOficial = "5215512345678"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotContain(e => e.PropertyName == nameof(NegocioDTO.Slug));
    }

    [Fact]
    public void Validar_TelefonoWhatsappVacio_TieneError()
    {
        // Arrange
        var dto = new NegocioDTO
        {
            NombreComercial = "Negocio Test",
            Slug = "negocio-test",
            TelefonoWhatsappOficial = ""
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(NegocioDTO.TelefonoWhatsappOficial));
    }
}
