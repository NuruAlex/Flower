using Dialogs;
using Dialogs.ClientDialogs;
using FileDataBase.Main;
using FileDataBase.Types;
using Messages.Reply.CallBack;
using System;
using Telegram.Bot.Types;

namespace Flower.Handlers;

public static class ProcessHandler
{
    public static ExecutingProcessCollection Processec => new();

    public static void Initialize(string path) => FileArchieve.AddValue(typeof(ExecutingProcess), path);


    public static event Action<string> OnDialogError;

    public static void Run(TelegramUser user, IStartProcess startProcess)
    {
        try
        {
            if (Processec.ContainsKey(user.KeyValue))
            {
                ExecutingProcess executingProcess = Processec.Find(i => i.KeyValue == user.KeyValue);
                executingProcess.StartProcess = startProcess;
                executingProcess.Start();
                executingProcess.Update();
                return;
            }

            ExecutingProcess process = new(user.KeyValue, startProcess);

            process.Start();
            Processec.Add(process);


        }
        catch (Exception ex)
        {
            OnDialogError.Invoke(ex.Message + $" ProcessExecutor / Run chat id:{user.KeyValue}, process type: {startProcess.GetType().Name}");
        }
    }

    public static void RunDefault()
    {
        new CheckClientSubscriptionProcess().Start();
        new ClientIsWentAroundProcess().Start();
        new ClientIsNeedToPayProcess().Start();
    }

    public static IStartProcess GetProcess(long chatId)
    {
        ExecutingProcess process = Processec.FindKey(chatId);

        return process?.StartProcess ?? null;
    }

    public static bool NextAction(long chatId, Message message)
    {
        ExecutingProcess proc = Processec.FindKey(chatId);

        try
        {

            proc?.NextAction(message);
            proc?.Update();

            return proc?.StartProcess != null && proc?.StartProcess is IOneActProcess;
        }
        catch (Exception ex)
        {
            OnDialogError.Invoke(ex.Message + $" ProcessExecutor / Run chat id:{chatId}, process type: {proc?.StartProcess.GetType().Name}");
            return false;
        }
    }
    public static bool NextAction(long chatId, Message message, CallBackPacket packet)
    {
        ExecutingProcess proc = Processec.FindKey(chatId);

        try
        {
            if (proc.StartProcess is PromocodeOrPayDialog dialog)
            {
                dialog.NextAction(message, packet);
            }
            proc?.Update();

            return proc?.StartProcess != null && proc?.StartProcess is IOneActProcess;
        }
        catch (Exception ex)
        {
            OnDialogError.Invoke(ex.Message + $" ProcessExecutor / Run chat id:{chatId}, process type: {proc?.StartProcess.GetType().Name}");
            return false;
        }
    }

    public static void StopProcess(TelegramUser user)
    {
        if (Processec.ContainsKey(user.KeyValue))
            Processec.DeleteAll(i => i.KeyValue == user.KeyValue);
    }

}
