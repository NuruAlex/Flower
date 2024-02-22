using FileDataBase.Main;
using FileDataBase.Types;

namespace FileDataBase.Collections;

public class UserCollection : ClicableObjectCollection<TelegramUser, long>
{
    public override void LoadDataAsync()
    {
        Items = new System.Collections.Generic.List<TelegramUser>();
        foreach (Client client in DataBase.Clients.Items)
            Items.Add(client);
        foreach (Admin admin in DataBase.Admins.Items)
            Items.Add(admin);
    }
    public override void Add(TelegramUser item)
    {
        if (item is Admin admin)
            DataBase.Admins.Add(admin);
        if (item is Client client)
            DataBase.Clients.Add(client);
    }
}
