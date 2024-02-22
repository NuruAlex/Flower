using Dialogs.ClientDialogs;
using FileDataBase.Collections;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Handlers;
using Messages.Main;
using Messages.Types;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
namespace Flower.Dialogs.Sub;

[Serializable]
public class IsClientMadeItem<T> : ISubProcess where T : ContainsKeyFileObject<int>
{
    private readonly int _number;
    private readonly string _errorMessage;

    public IsClientMadeItem(int number, string errorMessage)
    {
        _number = number;
        _errorMessage = errorMessage;
    }

    public async Task<int> Start(TelegramUser user, Message message, string suscessMessage)
    {

        FileObjectWithKeyCollection<T, int> collection = DataBaseCollections.GetCollection<T>() as FileObjectWithKeyCollection<T, int>;

        if (collection.Contains(i => i.KeyValue == _number))
        {

            await Sender.SendMessage(new TextMessage(user?.KeyValue)
            {
                Text = $"{_errorMessage} {_number}"
            });

            ProcessHandler.Run(user, new MoveProcess(user as Client));

        }
        else await Sender.SendMessage(new TextMessage(user?.KeyValue)
        {
            Text = suscessMessage
        });
        return -1;
    }
}
