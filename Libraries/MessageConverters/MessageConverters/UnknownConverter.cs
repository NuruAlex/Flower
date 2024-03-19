using FileDataBase.Types;
using Messages.Types;
using System;

namespace MessageConverters;

[Serializable]
public class UnknownConverter : TelegramMessageConverter<UnknownSerializableObject>
{
    public override string AsListItem(UnknownSerializableObject item)
    {
        throw new NotImplementedException();
    }

    public override TelegramMessage ConvertCollection(TelegramUser user)
    {
        throw new NotImplementedException();
    }

    public override TelegramMessage ConvertItem(TelegramUser user, UnknownSerializableObject item)
    {
        throw new NotImplementedException();
    }
}
