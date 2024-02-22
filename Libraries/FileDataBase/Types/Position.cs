using System;

namespace FileDataBase.Types;

[Serializable]
public class Position
{
    public Leaf Leaf { get; set; }
    public int Cell { get; set; }

    public string CellTitle { get; set; }
    public string LeafTitle { get; set; }

    public Position AddCell(int result)
    {
        Cell += result;

        if (Cell > 13)
            MoveLeaf();
        return this;
    }

    public void MoveLeaf()
    {
        Cell -= 13;
        Leaf = NextLeaf();
    }

    public override string ToString()
    {
        string cell = "";
        if (Cell > 0)
            cell = $"клетка: {Cell} ({CellTitle})";

        return $"Лепесток: {LeafTitle}, {cell}";
    }

    public string ToTextString() => ((int)Leaf).ToString() + Cell.ToString();

    private Leaf NextLeaf()
    {
        return Leaf switch
        {
            Leaf.Money => Leaf.Health,
            Leaf.Health => Leaf.Family,
            Leaf.Family => Leaf.Development,
            Leaf.Development => Leaf.Career,
            Leaf.Career => Leaf.Love,
            Leaf.Love => Leaf.Frends,
            Leaf.Frends => Leaf.Hobby,
            Leaf.Hobby => Leaf.Money,
            _ => Leaf.Money,
        };
    }

    public Position Copy() => new()
    {
        Cell = Cell,
        Leaf = Leaf,
    };
}
