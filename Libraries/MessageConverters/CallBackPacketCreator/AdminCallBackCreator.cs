using FileDataBase.Types;
using Messages.Reply.CallBack;
using System;
using System.Collections.Generic;

namespace CallBackPacketCreator;

[Serializable]
public abstract class AdminCallBackCreator<T> : IAdminCallBackCreator where T : ISerializableObject
{
    public abstract CallBackPacket CreatePacket(T item);
}

[Serializable]
public class AdminPacketCreator : AdminCallBackCreator<Admin>
{
    public override CallBackPacket CreatePacket(Admin item) => new(item.KeyValue, CallBackCode.Admin);
}

[Serializable]
public class ClientPacketCreator : AdminCallBackCreator<Client>
{
    public override CallBackPacket CreatePacket(Client item) => new(item.KeyValue, CallBackCode.Client);
}

[Serializable]
public class PromoCodePacketCreator : AdminCallBackCreator<PromoCode>
{
    public override CallBackPacket CreatePacket(PromoCode item) => new(item.KeyValue, CallBackCode.Client);
}
public class PaymentPacketCreator : AdminCallBackCreator<Payment>
{
    public override CallBackPacket CreatePacket(Payment item) => new(item.ChatId, CallBackCode.Payment, item.KeyValue);
}

public class PresentationtPacketCreator : AdminCallBackCreator<Presentation>
{
    public override CallBackPacket CreatePacket(Presentation item) => new(item.ChatId, CallBackCode.Presentation, item.KeyValue);
}

public class WishPacketCreator : AdminCallBackCreator<Wish>
{
    public override CallBackPacket CreatePacket(Wish item) => new(item.ChatId, CallBackCode.Wish, item.KeyValue);
}

public class UnknownPacketCreator : AdminCallBackCreator<UnknownSerializableObject>
{
    public override CallBackPacket CreatePacket(UnknownSerializableObject item)
    {
        throw new NotImplementedException();
    }
}

public static class AdminCallBackCreatorsArchieve
{
    private static readonly Dictionary<Type, IAdminCallBackCreator> _creators = new()
    {
        { typeof(Client), new ClientPacketCreator() },
        { typeof(Payment), new PaymentPacketCreator() },
        { typeof(Presentation), new PresentationtPacketCreator() },
        { typeof(Admin), new AdminPacketCreator() },
        { typeof(Wish), new WishPacketCreator() },
        { typeof(PromoCode), new PromoCodePacketCreator() },
    };
    public static AdminCallBackCreator<T> GetCreator<T>() where T : ISerializableObject
    {
        if (_creators.TryGetValue(typeof(T), out var creator))
            return creator as AdminCallBackCreator<T>;
        return new UnknownPacketCreator() as AdminCallBackCreator<T>;
    }
}