using FileDataBase.Main;
using System;

namespace FileDataBase.Types;

[Serializable]
public class Wish : ClicableObject<int>
{
    public Wish(int value, Client client) : base(value)
    {
        ChatId = client.KeyValue;
        UserName = client.UserName;
        RecordTime = DateTime.Now;
    }

    public readonly DateTime RecordTime;
    public readonly long ChatId;
    public readonly string UserName;

    public bool HasAnswered { get; set; }
    public string Text { get; set; }
    public string ResourceTitle { get; set; }


    public override string ToString() => $"Желание: \"{Text}\", номер желания:  {KeyValue}, ресурс {ResourceTitle},\n дата записи: {RecordTime:G}";

    public override void Update() => DataBase.Wishes.UpdateDefault(this);
}
