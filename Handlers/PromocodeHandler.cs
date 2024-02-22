using FileDataBase.Collections;
using FileDataBase.Main;
using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace Flower.Handlers;

[Serializable]
public class ClientPromocode : ContainsKeyFileObject<long>
{

    /// <summary>
    /// </summary>
    /// <param name="value">is client Chat id</param>
    /// <param name="current">current client promocode</param>
    public ClientPromocode(long value, PromoCode current) : base(value)
    {
        CurrentPromocode = current;
        PromoCodes = new() { current };
    }

    public PromoCode CurrentPromocode { get; set; }

    public List<PromoCode> PromoCodes { get; set; }

    public override void Update()
    {
        PromocodeHandler.ClientPromocodes.UpdateDefault(this);
    }
}
[Serializable]
public class ClientPromocodeCollection : FileObjectWithKeyCollection<ClientPromocode, long> { }

public static class PromocodeHandler
{
    public static ClientPromocodeCollection ClientPromocodes => new();

    public static void Initialize(string path) => FileArchieve.AddValue(typeof(ClientPromocode), path);

    public static void SetClientCurrentPromocode(Client client, PromoCode promoCode)
    {
        if (!ClientPromocodes.ContainsKey(client.KeyValue))
        {
            ClientPromocode item = new(client.KeyValue, promoCode);
            ClientPromocodes.Add(item);
            return;
        }

        ClientPromocode clientPromocode = ClientPromocodes.FindKey(client.KeyValue);
        clientPromocode.CurrentPromocode = promoCode;
        clientPromocode.PromoCodes.Add(promoCode);
        clientPromocode.Update();
    }
    public static void DeleteClientPromocodes(Client client) => ClientPromocodes.DeleteAll(i => i.KeyValue == client?.KeyValue);

    public static PromoCode GetClientCurrentPromocode(Client client)
    {
        ClientPromocode clientPromocode = ClientPromocodes.FindKey(client.KeyValue);
        return clientPromocode?.CurrentPromocode;
    }

}
