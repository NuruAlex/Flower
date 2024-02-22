using FileDataBase.Main;
using System;

namespace FileDataBase.Types;


[Serializable]
public class Move : ContainsKeyFileObject<int>
{
    public readonly long ChatId;
    public readonly Position Position;

    public Move(Client client, Position position) : base(DataBase.Moves.Count + 1)
    {
        ChatId = client.KeyValue;
        Position = position;
    }

    public override void Update() => DataBase.Moves.UpdateDefault(this);


    public override string ToString() => $"Ход: {KeyValue} {Position}";
}

[Serializable]
public class SelectingCardMove : Move
{
    public CardChoice CardChoice { get; set; }
    public SelectingCardMove(Client client, Position position, CardChoice choice) : base(client, position) => CardChoice = choice;

    public override string ToString() => base.ToString() + CardChoice.ToString();

}

[Serializable]
public class WishMove : Move
{
    public Wish Wish { get; set; }
    public WishMove(Client client, Position position, Wish wish) : base(client, position) => Wish = wish;

    public override string ToString() => base.ToString() + Wish.ToString();
}

[Serializable]
public class PresentationMove : Move
{
    public Presentation Pressentation { get; set; }

    public PresentationMove(Client client, Position position, Presentation presentation) : base(client, position) => Pressentation = presentation;

    public override string ToString() => base.ToString() + Pressentation.ToString();

}
