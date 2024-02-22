using FileDataBase.Main;
using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace FileDataBase.Collections;

public interface ISerializableObjectCollection
{
    public void LoadDataAsync();
    public void Save();
}

public class UnknownCollection : ISerializableObjectCollection
{
    public void LoadDataAsync()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class FileObjectWithKeyCollection<T, k> : ISerializableObjectCollection where T : ContainsKeyFileObject<k>
{
    private List<T> _items;
    public List<T> Items { get => _items; set => _items = value; }
    public int Count => _items.Count;
    public bool IsNull => _items == null;


    public FileObjectWithKeyCollection() => LoadDataAsync();

    public virtual void LoadDataAsync()
    {
        try
        {
            _items = DataBase.LoadFromFile<T>();
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectCollection<T>/ LoadDataAsync() / Type: {typeof(T).Name}");
        }
    }

    public virtual void Save()
    {
        try
        {
            DataBase.Save(_items);
            LoadDataAsync();
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectCollection<T> / Save()/ Type: {typeof(T).Name}");
        }
    }

    public virtual void Add(T item)
    {
        if (item == null || IsNull || ContainsKey(item)) return;

        try
        {
            Items.Add(item);
            Save();
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectWithKeyCollection<T> / Add()/ Type: {typeof(T).Name}");
        }
    }

    public virtual void DeleteAll(Predicate<T> match)
    {
        try
        {
            _items.RemoveAll(match);
            Save();
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectCollection<T> / DeleteAll()/ Type: {typeof(T).Name}");
        }
    }

    public virtual void DeleteDefault(T item) => DeleteAll(i => i.KeyValue.Equals(item.KeyValue));

    public void Update(T obj, Predicate<T> match)
    {
        if (IsNull || obj == null || !Contains(match)) return;

        try
        {
            _items[_items.FindIndex(match)] = obj;
            Save();
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectCollection<T> / Update()/ Type: {typeof(T).Name}");
        }
    }

    public void UpdateDefault(T item) => Update(item, i => i.KeyValue.Equals(item.KeyValue));


    public T Find(Predicate<T> match)
    {
        try
        {
            return _items.Find(match);
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectCollection<T> / Find()/ Type: {typeof(T).Name}");
        }
        return null;
    }

    public T FindKey(k keyValue)
    {

        try
        {
            return Items.Find(i => i.KeyValue.Equals(keyValue));
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectWithKeyCollection<T> / FindDefault()/ Type: {typeof(T).Name}");
        }
        return null;
    }

    public List<T> FindAsList(Predicate<T> match)
    {
        if (IsNull || _items.Count == 0) return new();

        try
        {
            return _items.FindAll(match);
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectCollection<T> / FindAsList()/ Type: {typeof(T).Name}");
            return new();
        }
    }

    public List<k> GetKeys(Predicate<T> match)
    {
        List<k> keys = new();

        FindAsList(match).ForEach(i => { keys.Add(i.KeyValue); });

        return keys;
    }

    public List<k> GetAllKeys()
    {
        List<k> keys = new();

        Items.ForEach(i => { keys.Add(i.KeyValue); });

        return keys;
    }

    public int CountOf(Predicate<T> match)
    {
        try
        {
            return _items.FindAll(match).Count;
        }
        catch (Exception ex)
        {
            DataBase.OnCollectionException(ex, $"FileObjectCollection<T> / CountOf()/ Type: {typeof(T).Name}");
        }
        return 0;
    }


    public bool Contains(Predicate<T> match) => Find(match) != null;
    public bool ContainsKey(k key) => FindKey(key) != null;
    public bool ContainsKey(T item) => ContainsKey(item.KeyValue);
}

[Serializable]
public class ClicableObjectCollection<T, k> : FileObjectWithKeyCollection<T, k> where T : ClicableObject<k>
{
    public ClicableObjectCollection() : base() { }
}
