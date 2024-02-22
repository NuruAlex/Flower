using FileDataBase.Groups;
using FileDataBase.Main;
using Telegram.Dialogs.DataDialogs;

namespace Telegram.Dialogs
{
    public class AddGroupProcess : IDialogProcess
    {
        public int Iteration { get; set; } = -1;
        private IRequestDataDialog _dialog;

        public bool IsLastAction { get; set; } = false;
        private Group _group;
        public void Start() => _dialog = new GroupRequestDialog() { IsRequiredNew = true };

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
                _group = new Group(((GroupRequestDialog)_dialog).Group.Name);
                _dialog = new TextRequestDialog("Введите описание группы (или отправьте слово \"нет\"):");
            }
            if (Iteration == 1)
            {
                if (((TextRequestDialog)_dialog).Text.ToLower() != "нет")
                    _group.Descriprion = ((TextRequestDialog)_dialog).Text;

                DataBase.GroupManager.AddGroup(_group);
                IsLastAction = true;
                _ = TelegramBot.SendTextMessageAsync("Группа добавлена");
            }
        }

    }
}
