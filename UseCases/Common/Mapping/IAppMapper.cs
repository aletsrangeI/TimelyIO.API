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

namespace UseCases.Common.Mapping;

public interface IAppMapper
{
    TDestination Map<TDestination>(object source);
}

public class AppMapperBridge : IAppMapper
{
    public TDestination Map<TDestination>(object source)
    {
        if (source == null)
            return default!;

        // Catalogo
        if (source is Catalogo cat && typeof(TDestination) == typeof(CatalogoDTO))
            return (TDestination)(object)cat.ToDto();
        if (source is CatalogoDTO catDto && typeof(TDestination) == typeof(Catalogo))
            return (TDestination)(object)catDto.ToEntity();
        if (source is IEnumerable<Catalogo> catList && typeof(TDestination) == typeof(IEnumerable<CatalogoDTO>))
            return (TDestination)(object)catList.ToDtoList();

        // ContenidoCatalogo
        if (source is ContenidoCatalogo cont && typeof(TDestination) == typeof(ContenidoCatalogoDTO))
            return (TDestination)(object)cont.ToDto();
        if (source is ContenidoCatalogoDTO contDto && typeof(TDestination) == typeof(ContenidoCatalogo))
            return (TDestination)(object)contDto.ToEntity();
        if (source is IEnumerable<ContenidoCatalogo> contList && typeof(TDestination) == typeof(IEnumerable<ContenidoCatalogoDTO>))
            return (TDestination)(object)contList.ToDtoList();

        // FormField
        if (source is FormField ff && typeof(TDestination) == typeof(FormFieldDTO))
            return (TDestination)(object)ff.ToDto();
        if (source is FormFieldDTO ffDto && typeof(TDestination) == typeof(FormField))
            return (TDestination)(object)ffDto.ToEntity();
        if (source is IEnumerable<FormField> ffList && typeof(TDestination) == typeof(IEnumerable<FormFieldDTO>))
            return (TDestination)(object)ffList.ToDtoList();

        // Persona
        if (source is Persona p && typeof(TDestination) == typeof(PersonaDTO))
            return (TDestination)(object)p.ToDto();
        if (source is PersonaDTO pDto && typeof(TDestination) == typeof(Persona))
            return (TDestination)(object)pDto.ToEntity();
        if (source is IEnumerable<Persona> pList && typeof(TDestination) == typeof(IEnumerable<PersonaDTO>))
            return (TDestination)(object)pList.ToDtoList();

        if (source is Persona pl && typeof(TDestination) == typeof(PersonaLoginDTO))
            return (TDestination)(object)pl.ToLoginDto();
        if (source is PersonaLoginDTO plDto && typeof(TDestination) == typeof(Persona))
            return (TDestination)(object)plDto.ToEntity();

        if (source is Persona pr && typeof(TDestination) == typeof(PersonaRegisterDTO))
            return (TDestination)(object)pr.ToRegisterDto();
        if (source is PersonaRegisterDTO prDto && typeof(TDestination) == typeof(Persona))
            return (TDestination)(object)prDto.ToEntity();

        // Negocio
        if (source is Negocio neg && typeof(TDestination) == typeof(NegocioDTO))
            return (TDestination)(object)neg.ToDto();
        if (source is NegocioDTO negDto && typeof(TDestination) == typeof(Negocio))
            return (TDestination)(object)negDto.ToEntity();
        if (source is IEnumerable<Negocio> negList && typeof(TDestination) == typeof(IEnumerable<NegocioDTO>))
            return (TDestination)(object)negList.ToDtoList();

        // Profesional
        if (source is Profesional prof && typeof(TDestination) == typeof(ProfesionalDTO))
            return (TDestination)(object)prof.ToDto();
        if (source is ProfesionalDTO profDto && typeof(TDestination) == typeof(Profesional))
            return (TDestination)(object)profDto.ToEntity();
        if (source is IEnumerable<Profesional> profList && typeof(TDestination) == typeof(IEnumerable<ProfesionalDTO>))
            return (TDestination)(object)profList.ToDtoList();

        // Servicio
        if (source is Servicio srv && typeof(TDestination) == typeof(ServicioDTO))
            return (TDestination)(object)srv.ToDto();
        if (source is ServicioDTO srvDto && typeof(TDestination) == typeof(Servicio))
            return (TDestination)(object)srvDto.ToEntity();
        if (source is IEnumerable<Servicio> srvList && typeof(TDestination) == typeof(IEnumerable<ServicioDTO>))
            return (TDestination)(object)srvList.ToDtoList();

        // Cliente
        if (source is Cliente cli && typeof(TDestination) == typeof(ClienteDTO))
            return (TDestination)(object)cli.ToDto();
        if (source is ClienteDTO cliDto && typeof(TDestination) == typeof(Cliente))
            return (TDestination)(object)cliDto.ToEntity();
        if (source is IEnumerable<Cliente> cliList && typeof(TDestination) == typeof(IEnumerable<ClienteDTO>))
            return (TDestination)(object)cliList.ToDtoList();

        // Cita
        if (source is Cita cit && typeof(TDestination) == typeof(CitaDTO))
            return (TDestination)(object)cit.ToDto();
        if (source is CitaDTO citDto && typeof(TDestination) == typeof(Cita))
            return (TDestination)(object)citDto.ToEntity();
        if (source is IEnumerable<Cita> citList && typeof(TDestination) == typeof(IEnumerable<CitaDTO>))
            return (TDestination)(object)citList.ToDtoList();

        // ExpedienteCliente
        if (source is ExpedienteCliente exp && typeof(TDestination) == typeof(ExpedienteClienteDTO))
            return (TDestination)(object)exp.ToDto();
        if (source is ExpedienteClienteDTO expDto && typeof(TDestination) == typeof(ExpedienteCliente))
            return (TDestination)(object)expDto.ToEntity();
        if (source is IEnumerable<ExpedienteCliente> expList && typeof(TDestination) == typeof(IEnumerable<ExpedienteClienteDTO>))
            return (TDestination)(object)expList.ToDtoList();

        throw new InvalidOperationException($"No mapping defined from {source.GetType().Name} to {typeof(TDestination).Name}");
    }
}
