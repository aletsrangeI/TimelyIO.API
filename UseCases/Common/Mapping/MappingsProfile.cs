using AutoMapper;
using Domain.Entities;
using DTO;
using DTO.ContenidoCatalogo;
using DTO.FormField;
using DTO.Persona;

namespace UseCases.Common.Mapping;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Catalogo, CatalogoDTO>().ReverseMap();
        CreateMap<ContenidoCatalogo, ContenidoCatalogoDTO>().ReverseMap();
        CreateMap<FormField, FormFieldDTO>().ReverseMap();
        CreateMap<Persona, PersonaDTO>().ReverseMap();
        CreateMap<Persona, PersonaLoginDTO>().ReverseMap();
        CreateMap<Persona, PersonaRegisterDTO>().ReverseMap();
    }
}