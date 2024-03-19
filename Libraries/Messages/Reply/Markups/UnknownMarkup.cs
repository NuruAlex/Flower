using Messages.Reply.Buttons;
using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace Messages.Reply.Markups;

[Serializable]
public class UnknownMarkup : IMarkup
{
    public IMarkup AddButton(IButton button) => this;

    public IMarkup AddRow() => this;

    public IReplyMarkup ToMarkUp() => null;
}
