using FileDataBase.Types;
using Messages.Types;
using System;
using System.Collections.Generic;

namespace MessageConverters;

public interface ITelegramMessageConverter { }

public abstract class TelegramMessageConverter<T> : ITelegramMessageConverter where T : ISerializableObject
{
    public abstract string AsListItem(T item);

    public abstract TelegramMessage ConvertItem(TelegramUser user, T item);

    public abstract TelegramMessage ConvertCollection(TelegramUser user);
}




public static class Converter
{

    public static event Action<string> OnConvertationError;


    public static TelegramMessage ConvertItem<T>(TelegramUser user, T item) where T : ISerializableObject
    {
        try
        {
            TelegramMessageConverter<T> converter = ConverterArchieve.GetConverter<T>();

            TelegramMessage message = converter.ConvertItem(user, item);

            return message;
        }
        catch (Exception ex)
        {
            OnConvertationError.Invoke(ex.Message + $" TelegramMessageConverter / ConvertToTelegramMessage(), Inner Converter type: {typeof(T).Name}");

            return new UnknownTelegramMessage();
        }
    }

    public static TelegramMessage ConvertCollection<T, k>(TelegramUser user) where T : ClicableObject<k>
    {
        try
        {
            TelegramMessageConverter<T> converter = ConverterArchieve.GetConverter<T>();

            TelegramMessage message = converter.ConvertCollection(user);

            return message;
        }
        catch (Exception ex)
        {
            OnConvertationError.Invoke(ex.Message + $"TelegramMessageConverter / ConvertToTelegramMessage(), Inner Converter type: {typeof(T).Name}");

            return new UnknownTelegramMessage();
        }
    }

    public static string ConvertToListItem<T>(T item) where T : ISerializableObject
    {
        try
        {
            TelegramMessageConverter<T> converter = ConverterArchieve.GetConverter<T>();

            string listItem = converter.AsListItem(item);

            return listItem;
        }
        catch (Exception ex)
        {
            OnConvertationError.Invoke(ex.Message + $"TelegramMessageConverter / ConvertToListItem(), Inner Converter type: {typeof(T).Name}");
            return "";
        }
    }
}

public static class ConverterArchieve
{
    public static Dictionary<Type, ITelegramMessageConverter> _converters = new()
    {
        {typeof(Client), new ClientToMessageConverter() },
        {typeof(Payment), new PaymentToMessageConverter() },
        {typeof(Presentation), new PresentationToMessageConverter() },
        {typeof(Admin), new AdminToMessageConverter() },
        {typeof(Wish), new WishToMessageConverter() },
        {typeof(PromoCode), new PromoCodeToMessageConverter() },
        {typeof(GlobalStatistics), new GlobalStatisticsConveter() },
    };

    public static TelegramMessageConverter<T> GetConverter<T>() where T : ISerializableObject
    {
        if (_converters.TryGetValue(typeof(T), out var converter))
            return converter as TelegramMessageConverter<T>;

        return new UnknownConverter() as TelegramMessageConverter<T>;
    }
}


