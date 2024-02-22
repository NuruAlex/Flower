using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Dialogs;

namespace TheFlowerOfHapinnes.Telegram.Dialogs.AdminDialogs
{
    internal class AnswerPresentationDialog : IDialogProcess
    {
        public int Iteration { get; set; }

        public void NextAction(Message parameter)
        {

        }

        public async Task Start()
        {
            await TelegramBot.AddAdminButtons("", new KeyboardButton[] {})
        }
    }
}
