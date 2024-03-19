using System;

namespace Messages.Reply.CallBack;

[Serializable]
public class CallBackPacket : ICallBackPacket
{
    public int Number { get; set; } = -1;
    public long ChatId { get; set; } = -1;
    public CallBackCode Code { get; set; } = CallBackCode.Ingnore;
    public string SendData { get; set; }
    private readonly string _callBack;
    public CallBackPacket(string callBackData) => _callBack = callBackData;

    public CallBackPacket(long chatId, CallBackCode code, int number)
    {
        ChatId = chatId;
        Code = code;
        Number = number;
    }
    public CallBackPacket(long chatId, CallBackCode code, int number, string sendData)
    {
        ChatId = chatId;
        Code = code;
        Number = number;
        SendData = sendData;
    }
    public CallBackPacket(CallBackCode code) => Code = code;

    public CallBackPacket(long chatId, CallBackCode code)
    {
        ChatId = chatId;
        Code = code;
    }

    public CallBackPacket(string textData, CallBackCode code)
    {
        SendData = textData;
        Code = code;
    }

    public string Pack() => $"{ChatId}.{(int)Code}.{Number}.{SendData}";

    public void UnPack()
    {
        string[] split = _callBack.Split('.');

        ChatId = long.Parse(split[0]);
        Code = (CallBackCode)Enum.Parse(typeof(CallBackCode), split[1]);
        Number = int.Parse(split[2]);
        SendData = split[3];
    }
}
