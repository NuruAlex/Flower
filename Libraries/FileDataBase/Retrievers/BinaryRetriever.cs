using FileDataBase.Types;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileDataBase.Retrievers;

public class BinaryRetriever : IRetriever
{
    private readonly BinaryFormatter _formatter = new();

    public string Extension => ".bin";

    public List<T> LoadFromFile<T>(string path) where T : ISerializableObject
    {
        using FileStream stream = new(path, FileMode.OpenOrCreate);

        stream.Position = 0;

        return _formatter.Deserialize(stream) as List<T> ?? new();
    }
    public void Save<T>(List<T> data, string path) where T : ISerializableObject
    {
        using FileStream stream = new(path, FileMode.OpenOrCreate);

        stream.Position = 0;

        _formatter.Serialize(stream, data);
    }
}