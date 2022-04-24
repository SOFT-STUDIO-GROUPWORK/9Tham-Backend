using System.Text;
using System.Text.RegularExpressions;
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
    
    public DeletionResult DeleteImage(string url)
    {
        var publicId = Regex.Match(url, @"\/([A-Z]+).");
        var publicIdGroup = publicId.Groups[1].Value;
        var deletionParams = new DeletionParams(publicIdGroup){
                ResourceType = ResourceType.Image};
        var deletionResult  = _cloudinary.Destroy(deletionParams);
        
        return deletionResult ;
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