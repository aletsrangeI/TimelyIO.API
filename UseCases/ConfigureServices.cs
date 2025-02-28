using System.Reflection;
using Interface.UseCases;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Catalogos;
using UseCases.ContenidoCatalogos;
using UseCases.FormFields;
using UseCases.Personas;
using Validator;

namespace UseCases;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // dependency injection
        services.AddScoped<ICatalogoApplication, CatalogoApplication>();
        services.AddScoped<IContenidoCatalogoApplication, ContenidoCatalogoApplication>();
        services.AddScoped<IFormFieldApplication, FormFieldApplication>();
        services.AddScoped<IPersonaApplication, PersonaApplication>();


        // validators
        services.AddTransient<CatalogoDTOValidator>();
        services.AddTransient<ContenidoCatalogoDTOValidator>();
        services.AddTransient<FormFieldDTOValidator>();
        services.AddTransient<PersonaDTOValidator>();
        services.AddTransient<PersonaLoginDTOValidator>();
        services.AddTransient<PersonaRegisterDTOValidator>();

        return services;
    }
}