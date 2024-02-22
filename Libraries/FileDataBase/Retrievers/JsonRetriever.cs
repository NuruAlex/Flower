using FileDataBase.Types;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FileDataBase.Retrievers;

public class JsonRetriever : IRetriever
{
    public string Extension => ".json";

    private readonly JsonSerializerSettings _serializeSettings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
    };

    public List<T> LoadFromFile<T>(string path) where T : ISerializableObject => JsonConvert.DeserializeObject<List<T>>(
            value: File.ReadAllText(path),
            settings: _serializeSettings) ?? new();

    public void Save<T>(List<T> data, string path) where T : ISerializableObject => File.WriteAllText(
            path: path,
            contents: JsonConvert.SerializeObject(data, _serializeSettings));
}