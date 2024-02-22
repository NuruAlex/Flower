using FileDataBase.Main;
using FileDataBase.Objects;
using Telegram.Dialogs.DataDialogs;

namespace Telegram.Dialogs
{
    public class AddRecordProcess : IDialogProcess
    {
        private Record _record;
        private IRequestDataDialog _dialog;

        public int Iteration { get; set; } = -1;
        public bool IsLastAction { get; set; } = false;

        public void Start() => _dialog = new GroupRequestDialog() { IsRequiredNew = false };

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
                _dialog = new ItemTitleRequestDialog("Введите название записи:") { IsRequiredNew = true };
            }
            if (Iteration == 1)
            {
                _record = new Record(((ItemTitleRequestDialog)_dialog).Title);
                _dialog = new TextRequestDialog("Введите текст:");
            }
            if (Iteration == 2)
            {
                _record.Text = ((TextRequestDialog)_dialog).Text;
                DataBase.TaskManager.AddObject(_record);
                IsLastAction = true;
                _ = TelegramBot.SendTextMessageAsync("Запись добавлена");
            }
        }

    }
}
