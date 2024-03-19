namespace Messages.Types.Media;

[System.Serializable]
public class TelegramPhoto : TelegramMedia
{
    public string Caption { get; set; } = string.Empty;
}