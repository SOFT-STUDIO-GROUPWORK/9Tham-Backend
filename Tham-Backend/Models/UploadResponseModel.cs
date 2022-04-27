namespace Tham_Backend.Models;

public class UploadResponseModel
{
    public int Width { get; set; }

    public int Height { get; set; }

    public string ResouceType { get; set; }

    public Uri Url { get; set; }

    public string Format { get; set; }
}