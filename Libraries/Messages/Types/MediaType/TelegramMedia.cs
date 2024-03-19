namespace Messages.Types.Media;

[System.Serializable]
public abstract class TelegramMedia
{
    public string UriOrFileId { get; set; }
    public string Path { get; set; }
}
