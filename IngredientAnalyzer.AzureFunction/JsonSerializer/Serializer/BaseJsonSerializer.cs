using System.Text.Json;
using IngredientAnalyzer.AzureFunction.Interfaces.JsonSerializer;

namespace IngredientAnalyzer.AzureFunction.JsonSerializer.Serializer;

public class BaseJsonSerializer<TConsumer> : IJsonSerializer<TConsumer>
{
    public JsonSerializerOptions JsonSerializerOptions { get; private set; }

    public BaseJsonSerializer()
    {
        JsonSerializerOptions = new JsonSerializerOptions();
        ConfigureJsonSerializerOptions();
    }

    public T? Deserialize<T>(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
    }

    protected virtual void ConfigureJsonSerializerOptions()
    {}
}