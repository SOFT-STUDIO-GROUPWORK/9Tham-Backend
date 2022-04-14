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
            PublicId = "olympic_flag"
        };
        var uploadResult = _cloudinary.Upload(uploadParams);
        return uploadResult;
    }
}