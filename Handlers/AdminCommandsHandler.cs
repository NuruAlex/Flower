using Flower.Resources;

namespace Flower.Handlers;

public class UsedCommand
{
    public string Text { get; set; }
    public int MessageId { get; set; }
}

public static class AdminCommandsHandler
{
    public static UsedCommand Last { get; set; }
    public static bool IsCommand(string text) => text != null && CommandArchieve.GetAdminCommandsList().Contains(text);

    public static bool IsAdminUsedCommand(string text)
    {
        if (Last?.Text == text)
            return true;
        return false;
    }
    public static void SetUsedCommand(string text, int messageId)
    {
        if (!IsCommand(text)) return;

        Last = new()
        {
            Text = text,
            MessageId = messageId
        };
    }
}
