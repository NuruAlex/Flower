using FileDataBase.Main;
using FileDataBase.Retrievers;
using Flower.Handlers;
using Messages.Main;
using System;

namespace Flower;



internal class Program
{
    private static void Main()
    {
        SignalHandler.Start();

        TelegramBot.Start();


        DataBase.SetRetriever(new BinaryRetriever());

        SenderClient.SetBotToken("6894117707:AAFWp7dZcMrwUr1d74UR_xq4Q1eeEn90YBg");



        /*  Client client = new Client(2029866924, 2029866924, "niki");
          *//*DataBase.Clients.Add(client);*//*
          UserHandler.CurrentUser = client;

          ProcessExecutor.Run(client, new SelectLeafProcess());
          ProcessExecutor.Run(client, new MoveProcess(client));*/

        Console.ReadKey();
    }
}
