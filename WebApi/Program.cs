using Persistence;
using Scalar.AspNetCore;
using UseCases;
using WatchDog;
using WebApi.Modules.Authentication;
using WebApi.Modules.Feature;
using WebApi.Modules.Injection;
using WebApi.Modules.Watch;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFeature(builder.Configuration);
builder.Services.AddInjection(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddWatchDog(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("TimelyIO.Service.WebApi")
        .WithTheme(ScalarTheme.Alternate)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

if (app.Environment.IsDevelopment())
{
    // Puedes agregar más configuraciones aquí si es necesario
}

app.UseWatchDogExceptionLogger();
app.UseHttpsRedirection();
app.UseCors("policyTimelyIO");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseWatchDog(conf =>
{
    conf.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUsername"];
    conf.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
});


app.UseDeveloperExceptionPage();
app.Run();

public partial class Program
{
};