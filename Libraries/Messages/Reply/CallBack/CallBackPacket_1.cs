namespace Messages.Reply.CallBack;


/// <summary>
/// call back data for inline buttons
/// </summary>
public interface ICallBackPacket
{
    string Pack();

    void UnPack();
}
