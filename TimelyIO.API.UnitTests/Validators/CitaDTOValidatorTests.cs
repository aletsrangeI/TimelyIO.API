using DTO.Cita;
using Domain.Enums;
using FluentAssertions;
using Validator;
using Xunit;

namespace TimelyIO.API.UnitTests.Validators;

public class CitaDTOValidatorTests
{
    private readonly CitaDTOValidator _validator = new();

    [Fact]
    public void Validar_CitaConDatosValidos_NoTieneErrores()
    {
        // Arrange
        var inicio = DateTime.UtcNow.AddHours(2);
        var fin = inicio.AddMinutes(45);
        var finConBuffer = fin.AddMinutes(15);

        var dto = new CitaDTO
        {
            Id = 1,
            NegocioId = 1,
            ClienteId = 10,
            ProfesionalId = 2,
            ServicioId = 3,
            FechaHoraInicioUtc = inicio,
            FechaHoraFinUtc = fin,
            FechaHoraFinConBufferUtc = finConBuffer,
            Estado = EstadoCita.Confirmada,
            CostoTotal = 600m,
            MontoAnticipoRequerido = 300m,
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
    public void Validar_NegocioIdInvalido_TieneError(int idInvalido)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.NegocioId = idInvalido;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.NegocioId));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validar_ClienteIdInvalido_TieneError(int idInvalido)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.ClienteId = idInvalido;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.ClienteId));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validar_ProfesionalIdInvalido_TieneError(int idInvalido)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.ProfesionalId = idInvalido;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.ProfesionalId));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validar_ServicioIdInvalido_TieneError(int idInvalido)
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.ServicioId = idInvalido;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.ServicioId));
    }

    [Fact]
    public void Validar_FechaFinMenorOIgualAFechaInicio_TieneError()
    {
        // Arrange
        var fecha = DateTime.UtcNow.AddHours(1);
        var dto = CrearDtoBase();
        dto.FechaHoraInicioUtc = fecha;
        dto.FechaHoraFinUtc = fecha; // Igual a inicio
        dto.FechaHoraFinConBufferUtc = fecha.AddMinutes(15);

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.FechaHoraFinUtc));
    }

    [Fact]
    public void Validar_FechaFinConBufferMenorAFechaFin_TieneError()
    {
        // Arrange
        var inicio = DateTime.UtcNow.AddHours(1);
        var fin = inicio.AddMinutes(45);
        var finConBuffer = inicio.AddMinutes(30); // Menor que fin

        var dto = CrearDtoBase();
        dto.FechaHoraInicioUtc = inicio;
        dto.FechaHoraFinUtc = fin;
        dto.FechaHoraFinConBufferUtc = finConBuffer;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.FechaHoraFinConBufferUtc));
    }

    [Fact]
    public void Validar_FechaFinConBufferIgualAFechaFin_EsAceptado()
    {
        // Arrange: Servicio con 0 min buffer
        var inicio = DateTime.UtcNow.AddHours(1);
        var fin = inicio.AddMinutes(45);

        var dto = CrearDtoBase();
        dto.FechaHoraInicioUtc = inicio;
        dto.FechaHoraFinUtc = fin;
        dto.FechaHoraFinConBufferUtc = fin; // Exactamente igual (buffer = 0)

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotContain(e => e.PropertyName == nameof(CitaDTO.FechaHoraFinConBufferUtc));
    }

    [Fact]
    public void Validar_CostoTotalNegativo_TieneError()
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.CostoTotal = -50m;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.CostoTotal));
    }

    [Fact]
    public void Validar_AnticipoMayorACostoTotal_TieneError()
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.CostoTotal = 500m;
        dto.MontoAnticipoRequerido = 600m; // Excede precio total

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.MontoAnticipoRequerido));
    }

    [Fact]
    public void Validar_AnticipoNegativo_TieneError()
    {
        // Arrange
        var dto = CrearDtoBase();
        dto.CostoTotal = 500m;
        dto.MontoAnticipoRequerido = -100m;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CitaDTO.MontoAnticipoRequerido));
    }

    private static CitaDTO CrearDtoBase()
    {
        var inicio = DateTime.UtcNow.AddHours(2);
        var fin = inicio.AddMinutes(45);
        return new CitaDTO
        {
            NegocioId = 1,
            ClienteId = 1,
            ProfesionalId = 1,
            ServicioId = 1,
            FechaHoraInicioUtc = inicio,
            FechaHoraFinUtc = fin,
            FechaHoraFinConBufferUtc = fin.AddMinutes(15),
            CostoTotal = 500m,
            MontoAnticipoRequerido = 250m,
            Active = true
        };
    }
}
