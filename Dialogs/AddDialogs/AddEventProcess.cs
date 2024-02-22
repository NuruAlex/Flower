using FileDataBase.Main;
using FileDataBase.Objects;
using Telegram.Dialogs.DataDialogs;

namespace Telegram.Dialogs
{
    public class AddEventProcess : IDialogProcess
    {
        private Event _event;


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
                _dialog = new ItemTitleRequestDialog("Введите название события:") { IsRequiredNew = true };
            }

            if (Iteration == 1)
            {
                _event = new Event(((ItemTitleRequestDialog)_dialog).Title);
                _dialog = new DateRequestDialog("Введите дату события:");
            }

            if (Iteration == 2)
            {
                _event.Date = ((DateRequestDialog)_dialog).Date;
                _dialog = new DateRequestDialog("Введите дату напоминания:");
            }

            if (Iteration == 3)
            {
                _event.RemindDate = ((DateRequestDialog)_dialog).Date;

                IsLastAction = true;
                DataBase.TaskManager.AddObject(_event);
                _ = TelegramBot.SendTextMessageAsync("Cобытие добавлено");
            }
        }

    }
}
