using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IngredientAnalyzer.AzureFunction.ServiceConfiguration;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) => {
        services.AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()

            .AddTelegramMessageHandler(context)
            .AddJsonSerializers();
    })
    .Build();

host.Run();