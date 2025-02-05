using WatchDog;

namespace WebApi.Modules.Watch;

public static class WatchDogExtensions
{
    public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
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
        return services;
    }
}