using Dialogs.DataDialog;
using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Bot;
using Flower.Dialogs.Sub;
using Flower.Handlers;
using Flower.Resources;
using Flower.SupportClasses;
using MessageConverters;
using Messages.Main;
using Messages.Types;
using System;
using Telegram.Bot.Types;

namespace Dialogs.ClientDialogs;

[Serializable]
public class PresentationProcess : IOneActProcess
{
    private Presentation _pres;
    private readonly int _number;

    private readonly IDataProcess _dialog;
    public TelegramUser User { get; set; }

    public PresentationProcess()
    {
        User = UserHandler.CallBackUser;
        _number = CallBackData.Packet.Number;
        _dialog = new VideoTextDialog(User.KeyValue);
    }

    public async void NextAction(Message message)
    {
        if (!_dialog.IsCorrectData(message))
        {
            await Sender.SendMessage(_dialog.ErrorMessage);
            return;
        }

        if (message.VideoNote != null)
        {
            VideoNoteMessage photo = await Builder.CreateTelegramMessage(message) as VideoNoteMessage;

            _pres = new VideoPresentation(_number, User as Client, photo.Media.Path);

            await Sender.SendMessage(User?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.VideoPresentationBonus));
            (User as Client).UnitsOfHapiness += 500;
            User.Update();
        }
        else
            _pres = new Presentation(_number, User as Client, message.Text);

        await Sender.SendMailing(Converter.ConvertItem(UserHandler.CurrentUser, _pres), DataBase.Admins.GetAllKeys());
        await Sender.SendMessage(User?.KeyValue, MessageArchieve.GetResourceMessage(ResourceMessageType.PresentationSent));

        DataBase.Presentations.Add(_pres);
        DataBase.Moves.Add(new PresentationMove(User as Client, PositionHandler.GetCurrentPosition(User as Client), _pres));

        ProcessHandler.Run(User, new MoveProcess(User as Client));
        User.Update();
    }

    public async void Start()
    {
        if (User == null || User as Client == null)
            return;

        IsClientMadeItem<Presentation> dialog = new(_number, $"Вы уже записали самопрезентацию номер: ");

        await dialog.Start(User, null, "Ждем вашу самопрезентацию (Видео или текст):");
    }
}
