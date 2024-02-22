using FileDataBase.Types;
using Flower.Bot;
using Flower.SupportClasses;
using System;
using System.Collections.Generic;

namespace Flower.Resources;



public static class ItemArchieve
{
    private static Dictionary<Type, ISerializableObject> _items;

    private static void Update()
    {
        _items = new()
        {
            { typeof(Client),       UserHandler.CallBackUser },
            { typeof(Wish),         CallBackData.Wish },
            { typeof(PromoCode),    CallBackData.PromoCode },
            { typeof(Admin),        UserHandler.CallBackUser },
            { typeof(Payment),      CallBackData.Payment },
            { typeof(Presentation), CallBackData.Presentation },
        };
    }

    public static T GetItem<T>() where T : ISerializableObject
    {
        Update();

        if (_items.TryGetValue(typeof(T), out var item))
        {
            return (T)item;
        }
        return (T)(new UnknownSerializableObject() as ISerializableObject);
    }

}
