using Messages.Reply.Buttons;
using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace Messages.Reply.Markups;

[Serializable]
public class RemoveMarkup : IMarkup
{
    public IMarkup AddButton(IButton button) => this;

    public IMarkup AddRow() => this;

    public IReplyMarkup ToMarkUp() => new ReplyKeyboardRemove();

}