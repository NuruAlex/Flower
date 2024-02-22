using FileDataBase.Collections;
using FileDataBase.Types;
using Flower.Handlers;
using System;
using Telegram.Bot.Types;

namespace Dialogs;

[Serializable]
public class ExecutingProcess : ContainsKeyFileObject<long>
{
    public IStartProcess StartProcess { get; set; }

    public ExecutingProcess(long value, IStartProcess process) : base(value) => StartProcess = process;
    public void Start() => StartProcess.Start();
    public void NextAction(Message message) => (StartProcess as IOneActProcess)?.NextAction(message);


    public override void Update()
    {
        ProcessHandler.Processec.UpdateDefault(this);
    }
}

[Serializable]
public class ExecutingProcessCollection : FileObjectWithKeyCollection<ExecutingProcess, long> { }


