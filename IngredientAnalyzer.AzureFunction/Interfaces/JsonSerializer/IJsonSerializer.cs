namespace IngredientAnalyzer.AzureFunction.Interfaces.JsonSerializer;

public interface IJsonSerializer<TConsumer>{
    T? Deserialize<T>(string json);
}