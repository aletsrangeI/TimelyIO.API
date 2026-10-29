using WatchDog;

namespace WebApi.Modules.Watch;

public static class WatchDogExtensions
{
    public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            services.AddWatchDogServices(opt =>
            {
                opt.SetExternalDbConnString = configuration.GetConnectionString(
                    "Timely_IO"
                );
                opt.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.PostgreSql;
                opt.IsAutoClear = true;
                opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Monthly;
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Advertencia] WatchDog no pudo conectar con la base de datos externa: {ex.Message}");
        }
        return services;
    }
}