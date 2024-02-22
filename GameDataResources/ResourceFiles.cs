using System.Collections.Generic;

namespace Flower.Resources;

public static class ResourceFiles
{


    private static readonly string _mainDirectory = "Resources\\GameData\\";
    private static readonly string     _cardsMain  = _mainDirectory + "Cards\\";
    private static readonly string         _moneyPath = _cardsMain + "Money";
    private static readonly string         _picturesPath = _cardsMain + "Pictures";
    private static readonly string         _answersPath = _cardsMain + "Answers";
    private static readonly string     _wishesMain = _mainDirectory + "WishImages\\";
    private static readonly string     _clientHelp = _mainDirectory + "Help\\";

    public static readonly string     Field      = _mainDirectory + "Field.bmp"; 

    public static readonly Dictionary<string, string> CardsPaths = new()
    {
        { "про деньги" ,          _moneyPath    },
        { "метафорические карты", _picturesPath },
        { "ответы внутри себя",   _answersPath  },
    };

    public static string GetCardsPathByChoiceType(string choice)
    {
        if (CardsPaths.TryGetValue(choice, out var path))
            return path;
        return null;

    }

    public static readonly Dictionary<string, int> CardsCount = new()
    {
        { "про деньги" ,          50 },
        { "метафорические карты", 40 },
        { "ответы внутри себя",   95 },
    };

    public static readonly Dictionary<int, string> Resources = new()
    {
        { 1, _wishesMain + "Abundance.bmp" },
        { 2, _wishesMain + "TrustInTheUniverse.bmp" },
        { 3, _wishesMain + "Harmony.bmp" },
        { 4, _wishesMain + "LoveYourSelf.bmp" },
        { 5, _wishesMain + "Manifestation.bmp" },
        { 6, _wishesMain + "DisclosureOfTalents.bmp" },
    };
    public static readonly Dictionary<int, string> ResourcesTitle = new()
    {
        {1, "Изобилие" },
        {2, "Доверие вселенной" },
        {3, "Гармония" },
        {4, "Любовь к себе" },
        {5, "Проявленность" },
        {6, "Раскрытие талантов" },
    };
}
