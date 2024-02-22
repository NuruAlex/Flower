using System;

namespace FileDataBase.Types;

public interface ISerializableObject
{
    void Update();
}

[Serializable]
public class UnknownSerializableObject : ISerializableObject
{
    public void Update()
    {
        throw new NotImplementedException();
    }
}



[Serializable]
public abstract class ContainsKeyFileObject<T> : ISerializableObject//T is KeyType
{
    public T KeyValue => key.Value;

    [Serializable]
    protected class Key
    {
        public readonly T Value;
        public Key(T value) => Value = value;
    }

    protected readonly Key key;

    public ContainsKeyFileObject(T value) => key = new Key(value);


    public abstract void Update();
}

[Serializable]
public abstract class ClicableObject<T> : ContainsKeyFileObject<T>
{
    public ClicableObject(T value) : base(value) { }
}
