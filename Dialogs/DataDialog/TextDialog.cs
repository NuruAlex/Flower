using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.DataDialog;

[Serializable]
public class TextDialog : IDataProcess
{
    private readonly string _textToCompare;
    public TelegramMessage ErrorMessage { get; set; } = new TextMessage()
    {
        Text = "Кажется произошла ошибка, команда не распознана, ожидалось \"Бросить кубик\""
    };




    public TextDialog(string textTocompare) => _textToCompare = textTocompare;


    public bool IsCorrectData(Message message)
    {
        if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            return false;

        if (_textToCompare.ToLower() == "any")
            return true;

        return message.Text.ToLower() == _textToCompare.ToLower();
    }

}
