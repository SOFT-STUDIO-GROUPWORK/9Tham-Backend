using CloudinaryDotNet.Actions;

namespace Tham_Backend.Services;

public interface ICloudinaryService
{
    public ImageUploadResult UploadImage(string path, string publicId);
    public DeletionResult DeleteImage(string url);
}