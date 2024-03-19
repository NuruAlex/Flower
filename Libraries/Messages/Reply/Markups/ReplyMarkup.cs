using Messages.Reply.Buttons;
using Messages.Reply.Rows;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Messages.Reply.Markups;

[System.Serializable]
public class ReplyMarkup : IMarkup
{
    private int _index = 0;

    private readonly List<List<ReplyButton>> _buttons = new()
    {
        new()
    };

    public IMarkup AddButton(string text) => AddButton(new ReplyButton(text));

    public ReplyMarkup()
    {

    }

    public ReplyMarkup(params IMarkupItem[] items)
    {
        foreach (IMarkupItem item in items)
        {
            if (item is ReplyButton button)
                AddButton(button.Text);
            if (item is ReplyRow)
                AddRow();
        }
    }
    public IMarkup AddButton(IButton button)
    {
        _buttons[_index].Add(button as ReplyButton);
        return this;
    }

    public IMarkup AddRow()
    {
        _buttons.Add(new List<ReplyButton>());
        _index = _buttons.Count - 1;
        return this;
    }

    public IReplyMarkup ToMarkUp()
    {
        int index = 0;

        List<List<KeyboardButton>> buttons = new();

        foreach (List<ReplyButton> row in _buttons)
        {
            buttons.Add(new());

            foreach (ReplyButton button in row)
                buttons[index].Add(new(button.Text));

            index++;
        }

        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true
        };
    }
}
