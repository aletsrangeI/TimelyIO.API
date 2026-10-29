using Common;
using Interface.Services;
using Logging;
using WebApi.Services;

namespace WebApi.Modules.Injection;

public static class InjectionExtension
{
    public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddHttpClient<ITurnstileService, TurnstileService>();
        return services;
    }
}