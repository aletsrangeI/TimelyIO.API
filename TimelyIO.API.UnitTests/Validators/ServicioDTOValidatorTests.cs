using DTO.Servicio;
using FluentAssertions;
using Validator;
using Xunit;

namespace TimelyIO.API.UnitTests.Validators;

public class ServicioDTOValidatorTests
{
    private readonly ServicioDTOValidator _validator = new();

    [Fact]
    public void Validar_ServicioValidoSinAnticipo_NoTieneErrores()
    {
        // Arrange
        var dto = new ServicioDTO
        {
            Id = 1,
            NegocioId = 1,
            Nombre = "Consulta General",
            DuracionMinutos = 45,
            BufferMinutosPosterior = 15,
            PrecioTotal = 800,
            RequiereAnticipo = false,
            MontoAnticipo = 0
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validar_ServicioConAnticipoValido_NoTieneErrores()
    {
        // Arrange: Anticipo de $400 para un total de $800
        var dto = new ServicioDTO
        {
            Id = 1,
            NegocioId = 1,
            Nombre = "Tratamiento Mayor",
            DuracionMinutos = 60,
            BufferMinutosPosterior = 15,
            PrecioTotal = 800,
            RequiereAnticipo = true,
            MontoAnticipo = 400
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validar_ServicioRequiereAnticipoPeroMontoEsCero_TieneError()
    {
        // Arrange
        var dto = new ServicioDTO
        {
            NegocioId = 1,
            Nombre = "Afinación Mayor",
            DuracionMinutos = 120,
            BufferMinutosPosterior = 30,
            PrecioTotal = 2500,
            RequiereAnticipo = true,
            MontoAnticipo = 0 // Invalido porque requiere anticipo
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ServicioDTO.MontoAnticipo) && e.ErrorMessage.Contains("mayor a 0"));
    }

    [Fact]
    public void Validar_MontoAnticipoMayorQuePrecioTotal_TieneError()
    {
        // Arrange: Anticipo de $1,200 en servicio de $1,000
        var dto = new ServicioDTO
        {
            NegocioId = 1,
            Nombre = "Servicio Especial",
            DuracionMinutos = 45,
            BufferMinutosPosterior = 10,
            PrecioTotal = 1000,
            RequiereAnticipo = true,
            MontoAnticipo = 1200
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ServicioDTO.MontoAnticipo) && e.ErrorMessage.Contains("no puede ser mayor que el precio total"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void Validar_DuracionMenorOIgualACero_TieneError(int duracionInvalida)
    {
        // Arrange
        var dto = new ServicioDTO
        {
            NegocioId = 1,
            Nombre = "Servicio Invalido",
            DuracionMinutos = duracionInvalida,
            BufferMinutosPosterior = 15,
            PrecioTotal = 500
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ServicioDTO.DuracionMinutos));
    }

    [Fact]
    public void Validar_BufferMinutosNegativo_TieneError()
    {
        // Arrange
        var dto = new ServicioDTO
        {
            NegocioId = 1,
            Nombre = "Servicio Con Buffer Invalido",
            DuracionMinutos = 30,
            BufferMinutosPosterior = -5, // Invalido
            PrecioTotal = 500
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ServicioDTO.BufferMinutosPosterior));
    }
}
