using FileDataBase.Main;
using System;

namespace FileDataBase.Types;

[Serializable]
public class Payment : ClicableObject<int>
{
    public bool Aproved { get; set; } = false;
    public long ChatId { get; set; }
    public double Price { get; set; }
    public string UserName { get; set; }
    public string PhotoPath { get; set; }
    public override void Update() => DataBase.Payments.UpdateDefault(this);
    public Payment(int value, long chatId) : base(value) => ChatId = chatId;



    public virtual string PaymentTypeToString() => "Оплата в боте";
    public virtual string GetCaptionString() => $"Клиент:  {GetClientString()}, {PaymentTypeToString()}";



    public string GetClientString() => UserName ?? ChatId.ToString();


}

[Serializable]
public class BotPayment : Payment
{
    public BotPayment(int value, long chatId) : base(value, chatId) { }
}


[Serializable]
public class DiscontPayment : Payment
{
    public DiscontPromoCode Promocode { get; set; } = null;

    public DiscontPayment(int value, long chatId) : base(value, chatId) { }

    public void CountDiscont()
    {
        Price -= Price / 100.00 * Promocode.Discont;
    }

    private string GetPromoCodeString()
    {
        if (Promocode != null)
            return $"Промокод: {Promocode.KeyValue}";
        return "Промокод не использовал";
    }

    public override string GetCaptionString() => $"Клиент:  {GetClientString()}, {PaymentTypeToString()},{GetPromoCodeString()}";


}


[Serializable]
public class GroupPayment : DiscontPayment
{
    public GroupPayment(int value, long chatId) : base(value, chatId) { }
    public override string PaymentTypeToString() => "Оплата групповой игры";
}

[Serializable]
public class IndividualPayment : DiscontPayment
{
    public IndividualPayment(int value, long chatId) : base(value, chatId) { }
    public override string PaymentTypeToString() => "Оплата индивидуальной игры";
}

public enum PaymentType
{
    Bot,
    Individual,
    Group,
}



