using FileDataBase.Main;
using Telegram.Dialogs.DataDialogs;

namespace Telegram.Dialogs.Delete
{
    public class DeleteGroupDialogProcess : IDialogProcess
    {
        public int Iteration { get; set; } = -1;
        public bool IsLastAction { get; set; } = false;

        private IRequestDataDialog _dialog;

        public void NextAction(string parameter)
        {
            _dialog.TryLoadData(parameter);

            if (!_dialog.IsCorrentData())
            {
                Iteration--;
                return;
            }
            DataBase.GroupManager.RemoveGroup(((GroupRequestDialog)_dialog).Group);
            _ = TelegramBot.SendTextMessageAsync("Группа удалена");
            IsLastAction = true;
        }

        public void Start() => _dialog = new GroupRequestDialog { IsRequiredNew = false };
    }
}
