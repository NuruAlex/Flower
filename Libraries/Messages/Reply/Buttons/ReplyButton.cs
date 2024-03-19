namespace Messages.Reply.Buttons;

[System.Serializable]
public class ReplyButton : IButton
{
    public string Text { get; set; }
    public ReplyButton(string text) => Text = text;
}
