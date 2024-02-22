using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace FileDataBase.Collections;

[Serializable]
public class AdminCollection : ClicableObjectCollection<Admin, long>
{
    public AdminCollection() : base() { }

    public static event Action<Admin> OnAdminDelete;

    public void AddDefault() => Add(new Admin(5082579517, "lekhaNuru"));

    public override void DeleteAll(Predicate<Admin> match)
    {
        List<Admin> admins = FindAsList(match);

        foreach (Admin admin in admins)
        {
            OnAdminDelete?.Invoke(admin);
            DeleteDefault(admin);
        }

    }
}
