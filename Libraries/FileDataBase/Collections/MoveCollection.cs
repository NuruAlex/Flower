using FileDataBase.Types;
using System;

namespace FileDataBase.Collections;

[Serializable]
public class MoveCollection : FileObjectWithKeyCollection<Move, int>
{
    public MoveCollection() : base() { }

    public int FindByClient(long chatId) => CountOf(i => i.ChatId == chatId);
    public int FindByClient(Client client) => CountOf(i => i.ChatId == client.KeyValue);
}
