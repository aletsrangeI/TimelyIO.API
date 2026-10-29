using DTO.Cliente;
using FluentAssertions;
using Validator;
using Xunit;

namespace TimelyIO.API.UnitTests.Validators;

public class ClienteDTOValidatorTests
{
    private readonly ClienteDTOValidator _validator = new();

    [Fact]
    public void Validar_ClienteValido_NoTieneErrores()
    {
        // Arrange
        var dto = new ClienteDTO
        {
            Id = 1,
            NegocioId = 1,
            NombreCompleto = "Alejandro Morales",
            TelefonoWhatsapp = "+525512345678",
            Email = "alecs@ejemplo.com",
            NotasGenerales = "Cliente preferente",
            Active = true
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validar_NegocioIdInvalido_TieneError(int negocioId)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.NegocioId = negocioId;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ClienteDTO.NegocioId));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validar_NombreCompletoVacio_TieneError(string? nombre)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.NombreCompleto = nombre!;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ClienteDTO.NombreCompleto));
    }

    [Fact]
    public void Validar_NombreCompletoExcede150Caracteres_TieneError()
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.NombreCompleto = new string('A', 151);

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ClienteDTO.NombreCompleto));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validar_TelefonoWhatsappVacio_TieneError(string? tel)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.TelefonoWhatsapp = tel!;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ClienteDTO.TelefonoWhatsapp));
    }

    [Fact]
    public void Validar_TelefonoWhatsappExcede20Caracteres_TieneError()
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.TelefonoWhatsapp = new string('9', 21);

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ClienteDTO.TelefonoWhatsapp));
    }

    [Theory]
    [InlineData("correo-invalido")]
    [InlineData("correo@")]
    [InlineData("@dominio.com")]
    public void Validar_EmailFormatoInvalido_TieneError(string emailInvalido)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.Email = emailInvalido;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ClienteDTO.Email));
    }

    [Fact]
    public void Validar_EmailOpcionalNuloOVacio_NoTieneError()
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.Email = null;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    private static ClienteDTO CrearDtoBase()
    {
        return new ClienteDTO
        {
            NegocioId = 1,
            NombreCompleto = "María López",
            TelefonoWhatsapp = "5512345678",
            Active = true
        };
    }
}
