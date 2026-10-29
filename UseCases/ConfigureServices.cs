using Interface.UseCases;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Catalogos;
using UseCases.Citas;
using UseCases.Clientes;
using UseCases.ContenidoCatalogos;
using UseCases.ExpedientesClientes;
using UseCases.FormFields;
using UseCases.Negocios;
using UseCases.Personas;
using UseCases.Profesionales;
using UseCases.Servicios;
using Validator;

namespace UseCases;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // mapper
        services.AddSingleton<UseCases.Common.Mapping.IAppMapper, UseCases.Common.Mapping.AppMapperBridge>();

        // dependency injection
        services.AddScoped<ICatalogoApplication, CatalogoApplication>();
        services.AddScoped<IContenidoCatalogoApplication, ContenidoCatalogoApplication>();
        services.AddScoped<IFormFieldApplication, FormFieldApplication>();
        services.AddScoped<IPersonaApplication, PersonaApplication>();

        services.AddScoped<INegocioApplication, NegocioApplication>();
        services.AddScoped<IProfesionalApplication, ProfesionalApplication>();
        services.AddScoped<IServicioApplication, ServicioApplication>();
        services.AddScoped<IClienteApplication, ClienteApplication>();
        services.AddScoped<ICitaApplication, CitaApplication>();
        services.AddScoped<IExpedienteClienteApplication, ExpedienteClienteApplication>();

        // validators
        services.AddTransient<CatalogoDTOValidator>();
        services.AddTransient<ContenidoCatalogoDTOValidator>();
        services.AddTransient<FormFieldDTOValidator>();
        services.AddTransient<PersonaDTOValidator>();
        services.AddTransient<PersonaLoginDTOValidator>();
        services.AddTransient<PersonaRegisterDTOValidator>();

        services.AddTransient<NegocioDTOValidator>();
        services.AddTransient<ProfesionalDTOValidator>();
        services.AddTransient<ServicioDTOValidator>();
        services.AddTransient<ClienteDTOValidator>();
        services.AddTransient<CitaDTOValidator>();
        services.AddTransient<ExpedienteClienteDTOValidator>();

        return services;
    }
}