using Messages.Main;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Messages.Types;

[System.Serializable]
public class Downloader
{
    private static ITelegramBotClient Bot => SenderClient.Bot;

    public async Task<string> DowloadIfNotExist(FileBase fileBaseObject)
    {
        string fileId = fileBaseObject.FileId;
        string destinationFilePath = PathCreator.CreatePath(fileId, fileBaseObject.GetType());
        return await DowloadIfNotExist(fileBaseObject, destinationFilePath);
    }

    public async Task<string> DowloadIfNotExist(FileBase fileBaseObject, string downloadPath)
    {
        if (fileBaseObject == null)
            return "";

        string fileId = fileBaseObject.FileId;

        if (!System.IO.File.Exists(downloadPath))
            await DownLoadFileAsync(fileId, downloadPath);

        return downloadPath;
    }
    public static async Task<File> GetFileAsync(string fileId) => await Bot.GetFileAsync(fileId);

    public static async Task DownLoadFileAsync(string fileId, string toPath)
    {
        var fileInfo = await GetFileAsync(fileId);
        var filePath = fileInfo.FilePath ?? "";

        if (filePath == "") return;

        using System.IO.Stream fileStream = System.IO.File.Create(toPath);
        await Bot.DownloadFileAsync(filePath, fileStream);
    }
}


