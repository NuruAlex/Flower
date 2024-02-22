using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Resources;
using Flower.SupportClasses;
using MessageConverters;
using Messages.Main;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class PayDialog : IOneActProcess
{
    private readonly Payment _payment;
    private readonly bool _withPromoCode;
    private bool _hasPhoto = false;
    private DiscontPromoCodeDialog _dialog = null;


    public PayDialog(PaymentType paymentType, bool withPromo)
    {
        User = UserHandler.CurrentUser;
        _payment = Creator.GetPayment(User as Client, paymentType);
        _withPromoCode = withPromo;

        DataBase.Payments.Add(_payment);
    }


    public TelegramUser User { get; set; }

    public async void ExecuteDialog(Message message)
    {
        if (!_dialog.IsCorrectData(message))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }
        else if (_payment is DiscontPayment payment && payment.Promocode == null)
        {
            payment.Promocode = _dialog.PromoCode;
            payment.CountDiscont();

            await Sender.SendMessage(new TextMessage(User?.KeyValue)
            {
                Text = $"Вы получаете скидку {payment.Promocode.Discont}%"
            });

            await Sender.SendMessage(new ContactMessage(User?.KeyValue)
            {
                Contants = TelegramContacts.GetDefault(_payment.Price)
            });

            return;
        }
    }

    public async void NextAction(Message parameter)
    {
        if (_dialog != null && _dialog.PromoCode != null)
            ExecuteDialog(parameter);

        if (_payment.Aproved) return;

        if (_hasPhoto)
        {
            await Sender.SendMessage(User?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.WaitAprovePayment));
            return;
        }
        else if (parameter.Photo == null)
        {
            await Sender.SendMessage(User?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.NeedToPaymentPhoto));
            return;
        }
        else if (!_hasPhoto && parameter.Photo.Length > 0)
        {

            _payment.PhotoPath = await new Downloader().DowloadIfNotExist(parameter.Photo[parameter.Photo.Length - 1]);


            await Sender.SendMessage(Converter.ConvertItem(UserHandler.CurrentUser, _payment));

            _hasPhoto = true;
            _payment.Update();
            await Sender.SendMessage(User?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.WaitFirstAprove));
        }
    }

    public async void Start()
    {
        if (_withPromoCode)
        {
            _dialog = new DiscontPromoCodeDialog(User as Client)
            {
                ErrorMessage = new TextMessage(User?.KeyValue)
                {
                    Text = "Кажется произошла ошибка"
                }
            };
        }
        else
        {
            await Sender.SendMessage(new ContactMessage(User?.KeyValue)
            {
                Contants = TelegramContacts.GetDefault(_payment.Price)
            });
        }
    }
}
