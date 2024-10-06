namespace IngredientAnalyzer.AzureFunction.Interfaces;

public interface IJsonSerializer<TConsumer>{
    T? Deserialize<T>(string json);
}