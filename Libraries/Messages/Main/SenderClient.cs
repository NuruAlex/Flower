using Telegram.Bot;

namespace Messages.Main;

public static class SenderClient
{
    private static string _token;
    private static ITelegramBotClient _bot;

    /// <summary>
    ///  method that set token to messaging
    /// </summary>
    /// <param name="token">ITelegramBotClient token</param>
    public static void SetBotToken(string token) => _token = token;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="chatId">is chat for sending errors</param>

    public static ITelegramBotClient Bot => _bot ??= new TelegramBotClient(_token);

}



