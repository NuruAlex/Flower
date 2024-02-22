using FileDataBase.Types;
using System.Collections.Generic;

namespace Flower.Resources;

public static class Flower
{
    private static readonly Dictionary<int, Leaf> _flower = new()
    {
        {1, Leaf.Money},
        {2, Leaf.Health },
        {3, Leaf.Family },
        {4, Leaf.Development },
        {5, Leaf.Career },
        {6, Leaf.Love },
        {7, Leaf.Frends },
        {8, Leaf.Hobby }
    };

    public static Leaf GetLeaf(int number)
    {
        if (_flower.TryGetValue(number, out Leaf leaf)) return leaf;
        return Leaf.Non;
    }
    public static Position GetPosition(int leaf, int cell) => new()
    {
        Leaf = GetLeaf(leaf),
        Cell = cell,
        CellTitle = TelegramTexts.CellsTitles[cell],
        LeafTitle = TelegramTexts.LeafsTitle[(int)GetLeaf(leaf)],
    };

}
