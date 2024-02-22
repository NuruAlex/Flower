using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Flower.SupportClasses;

public static class AvailableMessageTypes
{
    public static List<MessageType> AnswerTypes() => new()
    {
        MessageType.Audio,
        MessageType.Video,
        MessageType.Text,
        MessageType.VideoNote,
        MessageType.Voice
    };
    public static List<MessageType> VideoOrTextType() => new()
    {
        MessageType.VideoNote,
        MessageType.Text
    };
}
