using FileDataBase.Main;
using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace FileDataBase.Collections;

[Serializable]
public class ClientCollection : ClicableObjectCollection<Client, long>
{
    public ClientCollection() : base() { }

    public static event Action<Client> OnClientDelete;

    public override void DeleteDefault(Client item)
    {
        if (IsNull || !ContainsKey(item.KeyValue)) return;

        OnClientDelete?.Invoke(item);

        Items.RemoveAll(i => i.KeyValue == item.KeyValue);

        DataBase.Wishes.DeleteAll(i => i.ChatId == item.KeyValue);

        DataBase.Moves.DeleteAll(i => i.ChatId == item.KeyValue);

        DataBase.Payments.DeleteAll(i => i.ChatId == item.KeyValue);

        DataBase.Presentations.DeleteAll(i => i.ChatId == item.KeyValue);

        DataBase.Payments.DeleteAll(i => i.ChatId == item.KeyValue);

        DataBase.Choices.DeleteAll(i => i.ChatId == item.KeyValue);

        Save();
    }


    public override void DeleteAll(Predicate<Client> match)
    {
        List<Client> clients = FindAsList(match);

        foreach (Client client in clients)
        {
            OnClientDelete?.Invoke(client);
            DeleteDefault(client);
        }

        Save();
    }

}



