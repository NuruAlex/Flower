using FileDataBase.Collections;
using FileDataBase.Retrievers;
using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace FileDataBase.Main;


public static class DataBase
{
    private static readonly object _lock = new();
    private static IRetriever _retriever = new UnknownRetriever();


    public static event Action<string> OnRetrieverError;
    public static event Action<string> OnCollectionError;

    public static UserCollection Users => new();
    public static MoveCollection Moves => new();
    public static WishCollection Wishes => new();
    public static AdminCollection Admins => new();
    public static ClientCollection Clients => new();
    public static PaymentCollection Payments => new();
    public static CardChoiceCollection Choices => new();
    public static PromoCodeCollection PromoCodes => new();
    public static StatisticsCollection Statistics => new();
    public static PresentationCollection Presentations => new();



    public static void SetRetriever(IRetriever retriever) => _retriever = retriever;
    public static List<T> LoadFromFile<T>() where T : ISerializableObject => LoadFromFile<T>(FileArchieve.PathByType(typeof(T)) + _retriever.Extension) ?? new();
    public static void Save<T>(List<T> data) where T : ISerializableObject => Save(data, FileArchieve.PathByType(typeof(T)) + _retriever.Extension);

    public static List<T> LoadFromFile<T>(string path) where T : ISerializableObject
    {
        lock (_lock)
        {
            try
            {
                return _retriever?.LoadFromFile<T>(path) ?? new();
            }
            catch (Exception ex)
            {
                OnRetrieverError.Invoke(ex.Message + $" DataBase / LoadFromFile<T>() / Type: {_retriever?.GetType().Name} / T:  {typeof(T).Name} ");
            }
        }
        return new();
    }


    /// <summary>
    /// Serialize list of data to path if you setted <see cref="IRetriever"/> object
    /// </summary>
    /// <typeparam name="T">type of data to serialize, make sure your data is serializable</typeparam>
    /// <param name="data">list of data</param>
    /// <param name="path">path to serialize</param>
    public static void Save<T>(List<T> data, string path) where T : ISerializableObject
    {
        lock (_lock)
        {
            try
            {
                _retriever?.Save(data, path);
            }
            catch (Exception ex)
            {
                OnRetrieverError.Invoke(ex.Message + $"DataBase / Save<T>() / Type: {_retriever?.GetType().Name} / T:  {typeof(T).Name} ");
                _retriever?.Save(data, path);
            }
            finally
            {
                new JsonRetriever().Save(data, path.Replace(".bin", ".json"));
            }
        }
    }

    public static void OnCollectionException(Exception exception, string message) => OnCollectionError?.Invoke(exception.Message + " " + message);
}
