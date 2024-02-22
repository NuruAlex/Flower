using FileDataBase.Main;
using System;

namespace FileDataBase.Types;

[Serializable]
public class Presentation : ClicableObject<int>
{
    private bool _hasAnswered = false;
    public string Text { get; set; }
    public long ChatId { get; set; }
    public string UserName { get; set; }

    public bool HasAnswered
    {
        get => _hasAnswered;
        set
        {
            _hasAnswered = value;
            Update();
        }
    }

    public Presentation(int value, Client client, string text) : base(value)
    {
        ChatId = client.KeyValue;
        UserName = client.UserName;
        Text = text;
    }


    public override void Update() => DataBase.Presentations.UpdateDefault(this);

    public override string ToString() => $"Самопрезентация №{KeyValue}:";

}


[Serializable]
public class VideoPresentation : Presentation
{
    public string Path { get; set; } = "";

    public VideoPresentation(int value, Client client, string path) : base(value, client, "") => Path = path;

    public override string ToString() => base.ToString() + "Имеется видео";

}
