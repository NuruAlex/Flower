using FileDataBase.Collections;
using FileDataBase.Main;
using FileDataBase.Types;
using MessageConverters;
using Messages.Main;

namespace Flower.Handlers;

public static class SignalHandler
{
    public static void Start()
    {
        PromocodeHandler.Initialize("Resources\\TelegramFiles\\SupportInfo\\ClientPromoCodes");
        ProcessHandler.Initialize("Resources\\TelegramFiles\\SupportInfo\\Processes");
        PositionHandler.Initialize("Resources\\TelegramFiles\\SupportInfo\\ClientPositions");

        MessageHandler.StartHandle();
        CallBackHandler.StartHandle();

        Builder.OnBuildError += Sender_OnSendError;

        ClientCollection.OnClientDelete += Clients_OnClientDelete;
        AdminCollection.OnAdminDelete += Admins_OnAdminDelete;

        Sender.OnSendError += Sender_OnSendError;
        DataBase.OnRetrieverError += Sender_OnSendError;
        ProcessHandler.OnDialogError += Sender_OnSendError;
        Converter.OnConvertationError += Sender_OnSendError;
    }

    private static void Admins_OnAdminDelete(Admin obj) => ProcessHandler.StopProcess(obj);


    private static void Clients_OnClientDelete(Client obj)
    {
        ProcessHandler.StopProcess(obj);
        PromocodeHandler.DeleteClientPromocodes(obj);
        PositionHandler.DeleteClientPositions(obj);
    }


    private static async void Sender_OnSendError(string message) => await Sender.SendMessage(5082579517, message);
}
