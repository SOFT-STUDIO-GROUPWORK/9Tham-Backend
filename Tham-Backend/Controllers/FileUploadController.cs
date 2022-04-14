using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class FileUploadController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;

    public FileUploadController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost("upload")]
    [AllowAnonymous]
    public async Task<ActionResult<UploadResponseModel>> Register(UploadDTO request)
    {
        var result = _cloudinaryService.UploadImage("./Buffer/" + request.NameWithExtension, request.NameWithExtension);
        if (result.StatusCode != (HttpStatusCode) 200) return BadRequest("Fail to upload.");
        var response = new UploadResponseModel
        {
            Width = result.Width,
            Height = result.Height,
            ResouceType = result.ResourceType,
            Url = result.Url,
            Format = result.Format
        };
        return Ok(response);
    }
}