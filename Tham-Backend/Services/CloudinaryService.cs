using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Tham_Backend.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Account _account = new(
        "dgsl4gxtr",
        "357648493853985",
        "xnQdZdLODxhGnFN1XU6-YSzzeKw");

    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {
        _cloudinary = new Cloudinary(_account);
    }

    public ImageUploadResult UploadImage(string path, string publicId)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(path),
            PublicId = RandomName()
        };
        var uploadResult = _cloudinary.Upload(uploadParams);
        return uploadResult;
    }

    private string RandomName()
    {
        var str = new StringBuilder();
        char c;
        var random = new Random((int) DateTime.Now.Ticks);
        for (var i = 0; i < 32; i++)
        {
            c = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            str.Append(c);
        }

        return str.ToString();
    }
}