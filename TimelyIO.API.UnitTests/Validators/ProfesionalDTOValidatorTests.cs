using DTO.Profesional;
using FluentAssertions;
using Validator;
using Xunit;

namespace TimelyIO.API.UnitTests.Validators;

public class ProfesionalDTOValidatorTests
{
    private readonly ProfesionalDTOValidator _validator = new();

    [Fact]
    public void Validar_ProfesionalConDatosValidos_NoTieneErrores()
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            Id = 1,
            NegocioId = 1,
            NombreCompleto = "Dra. Elena Mora",
            TituloProfesional = "Odontopediatra",
            ColorIdentificador = "#2B5741",
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
    [InlineData(-50)]
    public void Validar_NegocioIdInvalido_TieneError(int negocioIdInvalido)
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = negocioIdInvalido,
            NombreCompleto = "Dr. Carlos Ruiz",
            TituloProfesional = "Fisioterapeuta",
            ColorIdentificador = "#1E3F35"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ProfesionalDTO.NegocioId));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validar_NombreCompletoVacio_TieneError(string? nombre)
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = 1,
            NombreCompleto = nombre!,
            TituloProfesional = "Nutrióloga",
            ColorIdentificador = "#0F766E"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ProfesionalDTO.NombreCompleto));
    }

    [Fact]
    public void Validar_NombreCompletoExcede150Caracteres_TieneError()
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = 1,
            NombreCompleto = new string('A', 151),
            TituloProfesional = "Especialista",
            ColorIdentificador = "#1E3F35"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ProfesionalDTO.NombreCompleto) && e.ErrorMessage.Contains("150 caracteres"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validar_TituloProfesionalVacio_TieneError(string? titulo)
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = 1,
            NombreCompleto = "Dra. Sofía Castro",
            TituloProfesional = titulo!,
            ColorIdentificador = "#701A75"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ProfesionalDTO.TituloProfesional));
    }

    [Fact]
    public void Validar_TituloProfesionalExcede100Caracteres_TieneError()
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = 1,
            NombreCompleto = "Dra. Sofía Castro",
            TituloProfesional = new string('B', 101),
            ColorIdentificador = "#701A75"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ProfesionalDTO.TituloProfesional) && e.ErrorMessage.Contains("100 caracteres"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validar_ColorIdentificadorVacio_TieneError(string? color)
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = 1,
            NombreCompleto = "Dr. Pedro Gómez",
            TituloProfesional = "Barbero",
            ColorIdentificador = color!
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ProfesionalDTO.ColorIdentificador));
    }

    [Theory]
    [InlineData("azul")]
    [InlineData("#123")]
    [InlineData("#12345")]
    [InlineData("#1234567")]
    [InlineData("1E3F35")]
    [InlineData("#GGGGGG")]
    [InlineData("#ZZZ123")]
    [InlineData("# 1E3F3")]
    public void Validar_ColorIdentificadorFormatoInvalido_TieneError(string colorInvalido)
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = 1,
            NombreCompleto = "Dr. Pedro Gómez",
            TituloProfesional = "Barbero",
            ColorIdentificador = colorInvalido
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(ProfesionalDTO.ColorIdentificador) && e.ErrorMessage.Contains("Hexadecimal"));
    }

    [Theory]
    [InlineData("#1E3F35")]
    [InlineData("#2B5741")]
    [InlineData("#FFFFFF")]
    [InlineData("#000000")]
    [InlineData("#abcdef")]
    [InlineData("#ABCDEF")]
    [InlineData("#123456")]
    public void Validar_ColorIdentificadorFormatoValido_EsAceptado(string colorValido)
    {
        // Arrange
        var dto = new ProfesionalDTO
        {
            NegocioId = 1,
            NombreCompleto = "Dr. Pedro Gómez",
            TituloProfesional = "Barbero",
            ColorIdentificador = colorValido
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotContain(e => e.PropertyName == nameof(ProfesionalDTO.ColorIdentificador));
    }
}
