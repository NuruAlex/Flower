using FileDataBase.Main;
using FileDataBase.Types;
using Flower.SupportClasses;
using Messages.Reply.CallBack;

namespace Flower.Bot;

public static class CallBackData
{
    public static Wish Wish { get; set; }
    public static Presentation Presentation { get; set; }
    public static PromoCode PromoCode { get; set; }
    public static Payment Payment { get; set; }
    public static CallBackPacket Packet { get; set; }

    private static void Update()
    {
        Packet.UnPack();

        if (Packet.SendData != null && Packet.SendData != "")
            PromoCode = DataBase.PromoCodes.FindKey(Packet.SendData);

        if (UserHandler.CallBackUser != null && Packet.Number != -1)
        {
            Wish = DataBase.Wishes.FindKey(Packet.Number);
            Presentation = DataBase.Presentations.FindKey(Packet.Number);
            Payment = DataBase.Payments.FindKey(Packet.Number);
        }
    }

    public static void LoadData(CallBackPacket packet)
    {
        Packet = packet;
        Update();
    }
}
