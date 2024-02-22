using FileDataBase.Main;
using FileDataBase.Types;
using Flower.Handlers;

namespace Flower.SupportClasses;

public static class MoveHandler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <returns>amount of moves</returns>
    public static int GetClientRemainingMovesCount(Client client) => client.RemainingMoves - DataBase.Moves.FindByClient(client.KeyValue);

    public static bool IsClientWentAround(Client client) => PositionHandler.GetCurrentPosition(client) != null &&
         PositionHandler.GetCurrentPosition(client).Leaf == PositionHandler.GetStartLeaf(client) &&
         PositionHandler.IsClientChandedLeaf(client);
}
