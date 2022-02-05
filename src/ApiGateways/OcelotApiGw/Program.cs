
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle()); ;

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
})
.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", false, true);
});



var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Welcome!");


app.UseOcelot();

app.Run();
