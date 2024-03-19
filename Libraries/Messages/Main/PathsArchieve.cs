using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Messages.Main;

public static class PathsArchieve
{
    private static readonly Dictionary<Type, string> _defaultDownloadPaths = new()
    {
        {typeof(Video),$"Resources\\Media\\Videos\\" },
        {typeof(Voice),$"Resources\\Media\\Voices\\" },
        {typeof(PhotoSize),$"Resources\\Media\\Photos\\" },
        {typeof(VideoNote),$"Resources\\Media\\VideoNotes\\" },
        {typeof(Audio),$"Resources\\Media\\Audios\\" },
    };


    private static readonly Dictionary<Type, string> _extensions = new()
    {
        {typeof(Video),".mp4" },
        {typeof(Voice),".ogg" },
        {typeof(PhotoSize),".jpg" },
        {typeof(VideoNote),".mp4" },
        {typeof(Audio),".mp3" },
    };

    public static string GetPathByType(Type type)
    {
        if (_defaultDownloadPaths.TryGetValue(type, out var path))
            return path;
        return "Unknown";
    }
    public static string GetExtensionByType(Type type)
    {
        if (_extensions.TryGetValue(type, out var extension))
            return extension;
        return "Unknown";
    }
}
