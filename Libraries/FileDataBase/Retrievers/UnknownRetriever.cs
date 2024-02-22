using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace FileDataBase.Retrievers;

public class UnknownRetriever : IRetriever
{
    public string Extension => throw new NotImplementedException();

    public List<T> LoadFromFile<T>() where T : ISerializableObject
    {
        throw new NotImplementedException();
    }

    public List<T> LoadFromFile<T>(string path) where T : ISerializableObject
    {
        throw new NotImplementedException();
    }

    public void Save<T>(List<T> data) where T : ISerializableObject
    {
        throw new NotImplementedException();
    }

    public void Save<T>(List<T> data, string path) where T : ISerializableObject
    {
        throw new NotImplementedException();
    }
}
