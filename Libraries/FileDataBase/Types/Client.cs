using FileDataBase.Main;
using System;

namespace FileDataBase.Types;



[Serializable]
public abstract class TelegramUser : ClicableObject<long>
{
    protected TelegramUser(long chatId, long id, string username = null) : base(chatId)
    {
        Id = id;
        UserName = username;
        RegistrationTime = DateTime.Now;
    }

    public readonly long Id;
    public readonly string UserName;
    public readonly DateTime RegistrationTime;



    public delegate void DeleteUser(TelegramUser user);
    public static event DeleteUser OnDeleteUser;

    public void OnDelete() => OnDeleteUser?.Invoke(this);

}

[Serializable]
public class Client : TelegramUser
{
    public int RemainingMoves { get; set; } = 3;
    public int UnitsOfHapiness { get; set; } = 1000;


    public override void Update() => DataBase.Clients.UpdateDefault(this);

    public Client(long value, long id, string username = null) : base(value, id, username)
    {
    }

}


[Serializable]
public class Admin : TelegramUser
{
    public override void Update() => DataBase.Admins.UpdateDefault(this);
    public Admin(long chatId, string userName) : base(chatId, 0, userName) { }

}