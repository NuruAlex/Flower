namespace Messages.Main;

public static class PathCreator
{
    public static string CreatePath(string fileId, System.Type type) => GetDownloadPath(type) + fileId + GetExtension(type);
    public static string GetDownloadPath(System.Type type) => PathsArchieve.GetPathByType(type);
    public static string GetExtension(System.Type type) => PathsArchieve.GetExtensionByType(type);
}

