using FileDataBase.Main;
using FileDataBase.Types;

namespace Flower.SupportClasses;

public class Creator
{
    public static int GetWishNumber() => DataBase.Wishes.Count + 1;
    public static int GetPresentationNumber() => DataBase.Presentations.Count + 1;
    public static int GetChoiceNumber() => DataBase.Choices.Count + 1;
    public static Payment GetPayment(Client client, PaymentType type)
    {
        int number = DataBase.Payments.Count + 1;

        return type switch
        {
            PaymentType.Bot => new BotPayment(number, client.KeyValue),
            PaymentType.Individual => new IndividualPayment(number, client.KeyValue),
            PaymentType.Group => new GroupPayment(number, client.KeyValue),
            _ => null,
        };
    }
}
