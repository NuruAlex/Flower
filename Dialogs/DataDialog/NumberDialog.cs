using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.DataDialog;

[Serializable]
public class NumberDialog : IDataProcess
{
    public readonly int LowNumber;
    public readonly int HighNumber;
    private readonly bool _withCompration;

    public long Result;

    public TelegramMessage ErrorMessage { get; set; }


    public NumberDialog(int lowNumber, int highNumber)
    {
        LowNumber = lowNumber;
        HighNumber = highNumber;
        _withCompration = true;
    }

    public bool IsCorrectData(Message message)
    {
        if (message.Text == null)
            return false;
        if (!long.TryParse(message.Text, out long result))
            return false;
        if (_withCompration && (result < LowNumber || result > HighNumber))
            return false;
        Result = result;
        return true;
    }
}
