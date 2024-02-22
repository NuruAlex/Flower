namespace Telegram.Dialogs
{
    public interface IDialogProcess
    {
        int Iteration { get; set; }
        bool IsLastAction { get; set; }
        void NextAction(string parameter);
        void Start();
    }
}
