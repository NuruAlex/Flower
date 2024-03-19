namespace Messages.Types;

[System.Serializable]
public class TelegramContacts
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public double Price { get; set; }

    public static TelegramContacts GetDefault(double price) => new()
    {
        FirstName = "Ольга",
        LastName = "Валерьевна",
        PhoneNumber = "+79067799578",
        Price = price
    };
}
