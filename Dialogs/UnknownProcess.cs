using FileDataBase.Types;
using System;

namespace Dialogs;

internal class UnknownProcess : IStartProcess
{
    public TelegramUser User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Start()
    {
        throw new NotImplementedException();
    }
}
