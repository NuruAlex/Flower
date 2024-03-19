using Messages.Reply.Buttons;
using Messages.Reply.CallBack;
using Messages.Reply.Rows;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Messages.Reply.Markups;

[Serializable]
public class InlineMarkup : IMarkup
{
    private int _index = 0;

    private readonly List<List<InlineButton>> _inlineButtons = new()
    {
        new()
    };


    public InlineMarkup()
    {

    }
    public InlineMarkup(params IMarkupItem[] items)
    {
        foreach (IMarkupItem item in items)
        {
            if (item is InlineButton button)
                AddButton(button);
            if (item is InlineRow)
                AddRow();
        }
    }
    public IMarkup AddButton(string title, ICallBackPacket callback) => AddButton(new InlineButton(title, callback));


    public IMarkup AddButton(IButton button)
    {
        _inlineButtons[_index].Add(button as InlineButton);
        return this;
    }

    public IMarkup AddRow()
    {
        _inlineButtons.Add(new List<InlineButton>());
        _index = _inlineButtons.Count - 1;
        return this;
    }

    public IReplyMarkup ToMarkUp()
    {
        int index = 0;

        List<List<InlineKeyboardButton>> buttons = new();

        foreach (List<InlineButton> row in _inlineButtons)
        {
            buttons.Add(new());

            foreach (InlineButton button in row)
                buttons[index].Add(new InlineKeyboardButton(button.Text) { CallbackData = button.CallBack });

            index++;
        }

        return new InlineKeyboardMarkup(buttons);
    }
}
