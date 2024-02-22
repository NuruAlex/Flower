using FileDataBase.Types;
using Flower.Resources;
using Messages.Main;
using Messages.Types;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Flower.Handlers;

public static class GameMovingHandler
{
    public static void OnBonusCell(Client client)
    {
        int cell = PositionHandler.GetCurrentPosition(client).Cell;

        if (cell == 6 || cell == 10)
            client.UnitsOfHapiness += 500;
        if (cell == 13)
            client.UnitsOfHapiness += 1000;

        client.Update();
    }
    public static async void MoveClient(Client client, int offset)
    {
        if (client == null || PositionHandler.GetCurrentPosition(client) == null)
            return;

        Position position = await OnChangedLeaf(client.KeyValue,
            PositionHandler.GetCurrentPosition(client), offset);


        position.LeafTitle = TelegramTexts.LeafsTitle[(int)position.Leaf];

        position.CellTitle = TelegramTexts.CellsTitles[position.Cell];

        PositionHandler.SetCurrentPosition(client, position);

        client.Update();
    }

    public static async Task<Position> OnChangedLeaf(long chatId, Position position, int offset)
    {
        Leaf previous = position.Leaf;

        Position next = position.Copy().AddCell(offset);

        if (previous != next.Leaf)
        {
            await Sender.SendMessage(new TextMessage(chatId)
                {
                    Text = $"Ты завершил лепесток {TelegramTexts.LeafsTitle[(int)previous]}. И переходишь на лепесток {TelegramTexts.LeafsTitle[(int)position.Leaf]}."
            });
        }
        return next;
    }
}
