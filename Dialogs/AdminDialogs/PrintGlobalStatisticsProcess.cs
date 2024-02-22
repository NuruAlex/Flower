using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Handlers;
using Flower.SupportClasses;
using MessageConverters;
using Messages.Main;
using System;

namespace Dialogs.AdminDialogs;



[Serializable]
public class PrintGlobalStatisticsProcess : IStartProcess
{
    private GlobalStatistics statistics;

    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public async void Start()
    {
        if (DataBase.Clients.Count == 0)
        {
            await Sender.SendMessage(User?.KeyValue, "Количество клиентов 0");
            return;
        }

        statistics = new GlobalStatistics(DateTime.Now, PromocodeHandler.ClientPromocodes.Count);

        DataBase.Statistics.Add(statistics);
        await Sender.SendMessage(Converter.ConvertItem(UserHandler.CurrentUser, statistics));
    }
}
