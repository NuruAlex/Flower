using FileDataBase.Main;
using System;
using System.Collections.Generic;

namespace FileDataBase.Types;

[Serializable]
public class GlobalStatistics : ContainsKeyFileObject<DateTime>
{
    public int ClientsAmount { get; set; }

    public int UsedPromocodes { get; set; }

    public Dictionary<PromoCode, int> NumberOfUsing { get; set; } = new();

    public double AverageNumberOfMoves { get; set; }

    public int AmountOfPayedClients { get; set; }

    public int AmountOfBotPayment { get; set; }

    public int AmountOfGroupPayment { get; set; }

    public int AmountOfIndividualPayment { get; set; }

    public GlobalStatistics(DateTime value, int usedPromoCodesCount) : base(value) => Load(usedPromoCodesCount);
    public override void Update() => DataBase.Statistics.UpdateDefault(this);

    private void Load(int usedPromoCodesCount)
    {

        List<Client> clients = DataBase.Clients.Items;
        ClientsAmount = clients.Count;

        UsedPromocodes = usedPromoCodesCount;

        foreach (PromoCode code in DataBase.PromoCodes.Items)
            NumberOfUsing.Add(code, code.NumberOfUsing);

        double m = 0;
        DataBase.Clients.Items.ForEach(i => { m += DataBase.Moves.FindByClient(i); });
        AverageNumberOfMoves = m / ClientsAmount;

        AmountOfPayedClients = DataBase.Clients.CountOf(i => DataBase.Payments.CountOf(i => i.ChatId == i.KeyValue) > 0);
        AmountOfBotPayment = DataBase.Payments.CountOf(i => i is not null && i is BotPayment);
        AmountOfGroupPayment = DataBase.Payments.CountOf(i => i is not null && i is GroupPayment);
        AmountOfIndividualPayment = DataBase.Payments.CountOf(i => i is not null && i is IndividualPayment);

    }


}
