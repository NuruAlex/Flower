using FileDataBase.Main;
using System;

namespace FileDataBase.Types;

[Serializable]
public class CardChoice : ContainsKeyFileObject<int>
{
    public long ChatId { get; set; }
    public string TypeOfChoice { get; set; }
    public int CardNumber { get; set; } = 0;

    public CardChoice(int number, Client client) : base(number) => ChatId = client.KeyValue;

    public override string ToString() => $"Выбор вида карты: {TypeOfChoice}, номер карточки:{CardNumber} ";
    public override void Update() => DataBase.Choices.UpdateDefault(this);
}
