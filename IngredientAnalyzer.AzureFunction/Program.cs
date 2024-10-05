using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IngredientAnalyzer.AzureFunction.ServiceConfiguration.MessageHandler;
using System.Text.Json;
using System.Text.Json.Serialization;
using IngredientAnalyzer.AzureFunction.JsonSerializer.Converter;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        
        services.Configure<JsonSerializerOptions>(options => {
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.Converters.Add(new UnixDateTimeConverter());
        });
        
        services.AddTelegramMessageHandler(context);
    })
    .Build();

host.Run();