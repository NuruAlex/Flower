using FileDataBase.Collections;
using FileDataBase.Main;
using FileDataBase.Types;
using System;
using System.Collections.Generic;

namespace Flower.Handlers;

[Serializable]
public class ClientPosition : ContainsKeyFileObject<long>
{
    public ClientPosition(long value, Position position) : base(value)
    {
        CurrentPosition = position;
        Positions = new List<Position>() { position };
    }

    public Position CurrentPosition { get; set; }
    public List<Position> Positions { get; set; }

    public override void Update() => PositionHandler.ClientPositions.UpdateDefault(this);

}

public class ClientPositionCollection : FileObjectWithKeyCollection<ClientPosition, long> { }

public static class PositionHandler
{
    public static ClientPositionCollection ClientPositions => new();

    public static void Initialize(string path) => FileArchieve.AddValue(typeof(ClientPosition), path);

    public static void SetCurrentPosition(Client client, Position position)
    {
        if (!ClientPositions.ContainsKey(client.KeyValue))
        {
            ClientPositions.Add(new(client.KeyValue, position));
            return;
        }

        ClientPosition clientPosition = ClientPositions.Find(i => i.KeyValue == client.KeyValue);
        clientPosition.CurrentPosition = position;
        clientPosition.Positions.Add(position);
        clientPosition.Update();
    }
    public static void DeleteClientPositions(Client client) => ClientPositions.DeleteAll(i => i.KeyValue == client?.KeyValue);
    public static Position GetCurrentPosition(Client client)
    {
        ClientPosition clientPosition = ClientPositions.Find(i => i.KeyValue == client.KeyValue);

        return clientPosition?.CurrentPosition;
    }

    public static Leaf GetStartLeaf(Client client)
    {
        ClientPosition clientPosition = ClientPositions.Find(i => i.KeyValue == client.KeyValue);

        if (clientPosition == null)
            return Leaf.Non;

        return clientPosition.Positions[0].Leaf;
    }
    public static bool IsClientChandedLeaf(Client client)
    {
        ClientPosition clientPosition = ClientPositions.Find(i => i.KeyValue == client.KeyValue);

        if (clientPosition == null)
            return false;

        Leaf start = GetStartLeaf(client);

        foreach (Position position in clientPosition.Positions)
            if (position.Leaf != start)
                return true;

        return false;
    }
}
