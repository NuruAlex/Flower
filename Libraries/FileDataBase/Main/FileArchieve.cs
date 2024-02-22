using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace FileDataBase.Main;

public static class FileArchieve
{
    private static readonly Dictionary<Type, string> _pathByType = new()
    {
        { typeof (PromoCode),        "Resources\\TelegramFiles\\Promocodes\\Promocodes"},
        { typeof (Admin),            "Resources\\TelegramFiles\\Admins" },
        { typeof (GlobalStatistics), "Resources\\TelegramFiles\\Statistics" },
        { typeof (Client),           "Resources\\TelegramFiles\\Data\\Clients" },
        { typeof (Payment),          "Resources\\TelegramFiles\\Data\\Payments" },
        { typeof (Wish),             "Resources\\TelegramFiles\\Data\\Wishes"},
        { typeof (Presentation),     "Resources\\TelegramFiles\\Data\\Press" },
        { typeof (Move),             "Resources\\TelegramFiles\\Data\\Moves" },
        { typeof (CardChoice),       "Resources\\TelegramFiles\\Data\\Choices"  },
    };
    public static void AddValue(Type type, string path)
    {
        if (!_pathByType.ContainsKey(type) && !_pathByType.ContainsValue(path))
            _pathByType.Add(type, path);
    }

    public static string PathByType(Type type) => _pathByType[type];
}
