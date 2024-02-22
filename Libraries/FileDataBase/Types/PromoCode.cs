using FileDataBase.Main;
using System;

namespace FileDataBase.Types;

[Serializable]
public abstract class PromoCode : ClicableObject<string>
{
    public int NumberOfUsing { get; set; }

    public PromoCode(string text) : base(text) => NumberOfUsing = 0;
    public override void Update() => DataBase.PromoCodes.UpdateDefault(this);
}

[Serializable]
public class DefaultPromoCode : PromoCode
{
    public DefaultPromoCode(string text) : base(text)
    {
    }

}

[Serializable]
public class SpecialPromoCode : PromoCode
{
    public int AddictionalyMoves { get; set; }

    public SpecialPromoCode(string text) : base(text) { }
}

[Serializable]
public class DiscontPromoCode : PromoCode
{
    public int Discont { get; set; }

    public DiscontPromoCode(string text) : base(text) { }



}
