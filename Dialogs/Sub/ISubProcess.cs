using FileDataBase.Types;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Flower.Dialogs.Sub;

public interface ISubProcess
{
    public Task<int> Start(TelegramUser user, Message message, string suscessMessage);
}
