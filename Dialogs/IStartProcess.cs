using FileDataBase.Types;
using Messages.Types;
using Telegram.Bot.Types;

namespace Dialogs;

public interface IStartProcess
{
    TelegramUser User { get; set; }
    void Start();
}


public interface IOneActProcess : IStartProcess
{
    void NextAction(Message message);
}

public interface IMultiActProcess : IOneActProcess
{
    int Iteration { get; set; }
}

public interface IDataProcess
{
    TelegramMessage ErrorMessage { get; set; }
    bool IsCorrectData(Message message);
}

