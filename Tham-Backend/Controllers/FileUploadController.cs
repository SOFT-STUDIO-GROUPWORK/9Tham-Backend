using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles="Admin")]
public class FileUploadController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;

    public FileUploadController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }


    [HttpPost("picture")]
    public async Task<ActionResult<UploadResponseModel>> Register([FromForm] UploadDTO objectFile)
    {
        try
        {
            if (objectFile.Files.Length > 0)
            {
                var path = "./Buffer/";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                using (var fileStream = System.IO.File.Create(path + objectFile.Files.FileName))
                {
                    objectFile.Files.CopyTo(fileStream);
                    fileStream.Flush();
                }

                var result = _cloudinaryService.UploadImage("./Buffer/" + objectFile.Files.FileName,
                    objectFile.Files.FileName);
                if (result.StatusCode != (HttpStatusCode) 200)
                    return BadRequest("Fail to upload, Cloudinary service not available.");
                var response = new UploadResponseModel
                {
                    Width = result.Width,
                    Height = result.Height,
                    ResouceType = result.ResourceType,
                    Url = result.Url,
                    Format = result.Format
                };

                if (System.IO.File.Exists("./Buffer/" + objectFile.Files.FileName))
                    System.IO.File.Delete("./Buffer/" + objectFile.Files.FileName);

                return Ok(response);
            }

            return BadRequest("Fail to upload, Not upload.");
        }
        catch (Exception exception)
        {
            return BadRequest("Fail to upload, Transfer file error.");
        }
    }
}