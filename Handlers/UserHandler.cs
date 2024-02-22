using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Reply.CallBack;

namespace Flower.SupportClasses;

public static class UserHandler
{
    public static TelegramUser CurrentUser { get; set; }
    public static TelegramUser CallBackUser { get; set; }

    public static bool IsAdmin() => CurrentUser != null && CurrentUser as Admin != null;
    public static bool IsClient() => CurrentUser != null && CurrentUser as Client != null;

    public static void SetCallBackUser(CallBackPacket packet)
    {
        packet.UnPack();

        CallBackUser = DataBase.Users.FindKey(packet.ChatId);

    }

    public static void SetCurrentUser(long chatId)
    {
        CurrentUser = DataBase.Users.FindKey(chatId);
    }
    public static void SetCurrentUser(long chatId, long Id, string userName)
    {
        CurrentUser = DataBase.Users.FindKey(chatId);

        if (CurrentUser != null)
            return;

        CurrentUser = new Client(chatId, Id, userName);
        DataBase.Clients.Add(CurrentUser as Client);
    }

}
