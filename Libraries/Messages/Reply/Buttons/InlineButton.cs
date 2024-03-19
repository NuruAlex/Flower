using Messages.Reply.CallBack;

namespace Messages.Reply.Buttons;

/// <summary>
/// Inline button is a button below the telegram message
/// </summary>
[System.Serializable]
public class InlineButton : IButton
{
    public string Text { get; set; }
    public string CallBack { get; set; }

    public InlineButton(string text, string callBack)
    {
        Text = text;
        CallBack = callBack;
    }
    public InlineButton(string text, ICallBackPacket callBackData)
    {
        Text = text;
        CallBack = callBackData.Pack();
    }
}




