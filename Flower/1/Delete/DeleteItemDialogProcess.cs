using FileDataBase.Main;
using Telegram.Dialogs.DataDialogs;

namespace Telegram.Dialogs.Delete
{
    public class DeleteItemDialogProcess : IDialogProcess
    {
        public int Iteration { get; set; } = -1;
        public bool IsLastAction { get; set; } = false;

        private IRequestDataDialog _dialog;

        public void NextAction(string parameter)
        {
            Iteration++;
            _dialog.TryLoadData(parameter);

            if (!_dialog.IsCorrentData())
            {
                Iteration--;
                return;
            }
            if (Iteration == 0)
            {
                DataBase.TaskManager = new TaskManager(((GroupRequestDialog)_dialog).Group);
                _dialog = new ItemTitleRequestDialog("Введите название элемента: ");
            }
            if(Iteration == 1)
            {
                DataBase.TaskManager.DeleteObject(((ItemTitleRequestDialog)_dialog).Title);
                IsLastAction = true;
                _ = TelegramBot.SendTextMessageAsync("Элемент удален");
            }
        }

        public void Start() => _dialog = new GroupRequestDialog { IsRequiredNew = false };
    }
}
