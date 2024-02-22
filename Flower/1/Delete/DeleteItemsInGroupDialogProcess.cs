using FileDataBase.Main;
using Telegram.Dialogs.DataDialogs;

namespace Telegram.Dialogs.Delete
{
    public class DeleteItemsInGroupDialogProcess : IDialogProcess
    {
        public int Iteration { get; set; } = -1;
        public bool IsLastAction { get; set; } = false;

        private IRequestDataDialog _dialog;

        public void NextAction(string parameter)
        {
            _dialog.TryLoadData(parameter);

            if (!_dialog.IsCorrentData())
                return;

            DataBase.TaskManager = new TaskManager(((GroupRequestDialog)_dialog).Group);
            DataBase.TaskManager.ClearAll();
            _ = TelegramBot.SendTextMessageAsync("Элементы удалены");
            IsLastAction = true;
        }

        public void Start() => _dialog = new GroupRequestDialog() { IsRequiredNew = false };
    }
}
