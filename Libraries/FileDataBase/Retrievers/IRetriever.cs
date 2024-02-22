using FileDataBase.Types;
using System.Collections.Generic;

namespace FileDataBase.Retrievers;

public interface IRetriever
{
    string Extension { get; }

    List<T> LoadFromFile<T>(string path) where T : ISerializableObject;

    void Save<T>(List<T> data, string path) where T : ISerializableObject;
}