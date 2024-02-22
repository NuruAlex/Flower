using FileDataBase.Collections;
using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace FileDataBase.Main;

public static class DataBaseCollections
{
    private static Dictionary<Type, ISerializableObjectCollection> _collections = new()
    {
        { typeof (PromoCode),        new PromoCodeCollection()},
        { typeof (Admin),            new AdminCollection() },
        { typeof (Client),           new ClientCollection() },
        { typeof (Payment),          new PaymentCollection() },
        { typeof (Wish),             new WishCollection()},
        { typeof (Presentation),     new PresentationCollection()},
        { typeof (Move),             new MoveCollection() },
        { typeof (CardChoice),       new CardChoiceCollection() },
    };

    public static void AddCollection(Type type, ISerializableObjectCollection collection)
    {
        if (!_collections.ContainsKey(type))
            _collections.Add(type, collection);
    }

    public static void AddCollections(Dictionary<Type, ISerializableObjectCollection> collections) => _collections = collections;


    public static ISerializableObjectCollection GetCollection<T>() where T : ISerializableObject
    {
        if (_collections.TryGetValue(typeof(T), out var collection))
            return collection;

        return new UnknownCollection();
    }
}
