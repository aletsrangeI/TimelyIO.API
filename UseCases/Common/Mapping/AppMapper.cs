using Domain.Entities;
using DTO;
using DTO.Cita;
using DTO.Cliente;
using DTO.ContenidoCatalogo;
using DTO.ExpedienteCliente;
using DTO.FormField;
using DTO.Negocio;
using DTO.Persona;
using DTO.Profesional;
using DTO.Servicio;
using Riok.Mapperly.Abstractions;

namespace UseCases.Common.Mapping;

[Mapper]
public static partial class AppMapper
{
    // Catalogo
    public static partial CatalogoDTO ToDto(this Catalogo entity);
    public static partial Catalogo ToEntity(this CatalogoDTO dto);
    public static partial IEnumerable<CatalogoDTO> ToDtoList(this IEnumerable<Catalogo> entities);

    // ContenidoCatalogo
    public static partial ContenidoCatalogoDTO ToDto(this ContenidoCatalogo entity);
    public static partial ContenidoCatalogo ToEntity(this ContenidoCatalogoDTO dto);
    public static partial IEnumerable<ContenidoCatalogoDTO> ToDtoList(this IEnumerable<ContenidoCatalogo> entities);

    // FormField
    public static partial FormFieldDTO ToDto(this FormField entity);
    public static partial FormField ToEntity(this FormFieldDTO dto);
    public static partial IEnumerable<FormFieldDTO> ToDtoList(this IEnumerable<FormField> entities);

    // Persona
    public static partial PersonaDTO ToDto(this Persona entity);
    public static partial Persona ToEntity(this PersonaDTO dto);
    public static partial IEnumerable<PersonaDTO> ToDtoList(this IEnumerable<Persona> entities);

    public static partial PersonaLoginDTO ToLoginDto(this Persona entity);
    public static partial Persona ToEntity(this PersonaLoginDTO dto);

    public static partial PersonaRegisterDTO ToRegisterDto(this Persona entity);
    public static partial Persona ToEntity(this PersonaRegisterDTO dto);

    // Negocio
    public static partial NegocioDTO ToDto(this Negocio entity);
    public static partial Negocio ToEntity(this NegocioDTO dto);
    public static partial IEnumerable<NegocioDTO> ToDtoList(this IEnumerable<Negocio> entities);

    // Profesional
    public static partial ProfesionalDTO ToDto(this Profesional entity);
    public static partial Profesional ToEntity(this ProfesionalDTO dto);
    public static partial IEnumerable<ProfesionalDTO> ToDtoList(this IEnumerable<Profesional> entities);

    // Servicio
    public static partial ServicioDTO ToDto(this Servicio entity);
    public static partial Servicio ToEntity(this ServicioDTO dto);
    public static partial IEnumerable<ServicioDTO> ToDtoList(this IEnumerable<Servicio> entities);

    // Cliente
    public static partial ClienteDTO ToDto(this Cliente entity);
    public static partial Cliente ToEntity(this ClienteDTO dto);
    public static partial IEnumerable<ClienteDTO> ToDtoList(this IEnumerable<Cliente> entities);

    // Cita
    public static partial CitaDTO ToDto(this Cita entity);
    public static partial Cita ToEntity(this CitaDTO dto);
    public static partial IEnumerable<CitaDTO> ToDtoList(this IEnumerable<Cita> entities);

    // ExpedienteCliente
    public static partial ExpedienteClienteDTO ToDto(this ExpedienteCliente entity);
    public static partial ExpedienteCliente ToEntity(this ExpedienteClienteDTO dto);
    public static partial IEnumerable<ExpedienteClienteDTO> ToDtoList(this IEnumerable<ExpedienteCliente> entities);
}
