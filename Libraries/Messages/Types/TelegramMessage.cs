using Messages.Reply.Markups;
using Messages.Types.Media;
using System;
using Telegram.Bot.Types.Enums;
namespace Messages.Types;

[Serializable]
public abstract class TelegramMessage
{
    private IMarkup _markup;

    public TelegramMessage(long? chatId) => ChatId = chatId ?? 0;
    public TelegramMessage() { }

    public long ChatId { get; set; }

    public IMarkup Markup
    {
        get => _markup ??= new UnknownMarkup();
        set => _markup = value;
    }

    public abstract MessageType Type { get; }
}

[Serializable]

public class ContactMessage : TelegramMessage
{
    public ContactMessage() { }
    public ContactMessage(long? chatId) : base(chatId) { }
    public TelegramContacts Contants { get; set; }
    public override MessageType Type => MessageType.Contact;
}

[Serializable]
public class TextMessage : TelegramMessage
{
    public TextMessage() { }
    public TextMessage(long? chatId) : base(chatId) { }
    public string Text { get; set; }
    public override MessageType Type => MessageType.Text;
}

[Serializable]
public abstract class MediaMessage<T> : TelegramMessage where T : TelegramMedia
{
    public T Media { get; set; }
    public MediaMessage() : base() { }
    public MediaMessage(long? chatId) : base(chatId) { }

}

[Serializable]
public class AudioMessage : MediaMessage<TelegramAudio>
{
    public AudioMessage() { }
    public AudioMessage(long? chatId) : base(chatId) { }

    public override MessageType Type => MessageType.Audio;
}

[Serializable]
public class VideoMessage : MediaMessage<TelegramVideo>
{
    public VideoMessage() { }
    public VideoMessage(long? chatId) : base(chatId) { }

    public override MessageType Type => MessageType.Video;
}

[Serializable]
public class PhotoMessage : MediaMessage<TelegramPhoto>
{
    public PhotoMessage() { }
    public PhotoMessage(long? chatId) : base(chatId) { }

    public override MessageType Type => MessageType.Photo;
}

[Serializable]
public class VideoNoteMessage : MediaMessage<TelegramVideoNote>
{
    public VideoNoteMessage() { }
    public VideoNoteMessage(long? chatId) : base(chatId) { }

    public override MessageType Type => MessageType.VideoNote;
}



[Serializable]
public class VoiceMessage : MediaMessage<TelegramVoice>
{
    public VoiceMessage() : base() { }
    public VoiceMessage(long? chatId) : base(chatId) { }

    public override MessageType Type => MessageType.Voice;
}


[Serializable]
public class UnknownTelegramMessage : TelegramMessage
{
    public UnknownTelegramMessage() : base() { }

    public override MessageType Type => MessageType.Unknown;
}



