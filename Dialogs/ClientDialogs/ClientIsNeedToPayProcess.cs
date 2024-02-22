using FileDataBase.Types;
using Flower.Handlers;
using Flower.SupportClasses;
using System;

namespace Dialogs.ClientDialogs;

[Serializable]
public class ClientIsNeedToPayProcess : IStartProcess
{
    public TelegramUser User { get => UserHandler.CurrentUser; set { } }

    public void Start()
    {

        IStartProcess process = ProcessHandler.GetProcess(User.KeyValue);

        if (process?.GetType() != typeof(MoveProcess))
            return;

        if (process.GetType() == typeof(PromocodeOrPayDialog))
            return;

        if (PaymentsHandler.IsClientPayed(User as Client))
            return;
        int clientRemainingMoves = MoveHandler.GetClientRemainingMovesCount(User as Client);

        if (clientRemainingMoves > 0)
            return;
        if (clientRemainingMoves == 0 && PromocodeHandler.GetClientCurrentPromocode(User as Client) != null)
            return;

        ProcessHandler.Run(User, new PromocodeOrPayDialog());

    }
}
