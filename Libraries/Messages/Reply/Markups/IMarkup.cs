using Messages.Reply.Buttons;
using Telegram.Bot.Types.ReplyMarkups;

namespace Messages.Reply.Markups;

public interface IMarkup
{
    IMarkup AddButton(IButton button);
    IMarkup AddRow();
    IReplyMarkup ToMarkUp();
}
