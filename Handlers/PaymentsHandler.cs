using FileDataBase.Main;
using FileDataBase.Types;

namespace Flower.SupportClasses;


public static class PaymentsHandler
{
    public static bool IsClientPayed(Client client) => DataBase.Payments.Contains
        (i => i != null && i is BotPayment payment &&
        payment.ChatId == client.KeyValue &&
        payment.Aproved) &&
        client.RemainingMoves == -1;

    public static int GetClientPaymentsCount(Client client) => DataBase.Payments.CountOf(i => i.ChatId == client.KeyValue);
}
