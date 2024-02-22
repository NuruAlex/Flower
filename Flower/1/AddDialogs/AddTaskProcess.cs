using FileDataBase.Main;
using FileDataBase.Objects;
using Telegram.Dialogs.DataDialogs;

namespace Telegram.Dialogs
{
    public class AddTaskProcess : IDialogProcess
    {
        private Task _task;
        private IRequestDataDialog _dialog;
        public int Iteration { get; set; } = -1;
        public bool IsLastAction { get; set; } = false;
        
        public void Start()=> _dialog = new GroupRequestDialog() { IsRequiredNew = false };

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
                _dialog = new ItemTitleRequestDialog("Введите название задачи:") { IsRequiredNew = true };
            }
            if (Iteration == 1)
            {
                _task = new Task(((ItemTitleRequestDialog)_dialog).Title);
                _dialog = new TextRequestDialog("В чем суть вашей задачи?");
            }
            if (Iteration == 2)
            {
                _task.Essence = ((TextRequestDialog)_dialog).Text;
                _dialog = new DateRequestDialog("К какому сроку нужно выполнить задачу?");
            }
            if (Iteration == 3)
            {
                _task.DeadLine = ((DateRequestDialog)_dialog).Date;
                _dialog = new TextRequestDialog("Напишите план действий:");
            }
            if (Iteration == 4)
            {
                _task.Plan = ((TextRequestDialog)_dialog).Text;
                _dialog = new DateRequestDialog("Введите дату напоминания:");
            }
            if (Iteration == 5)
            {
                _task.RemindDate = ((DateRequestDialog)_dialog).Date;
                IsLastAction = true;
                DataBase.TaskManager.AddObject(_task);
                _ = TelegramBot.SendTextMessageAsync("Событие добавлено");
            }
        }

    }
}
