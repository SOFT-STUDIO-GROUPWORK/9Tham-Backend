using Microsoft.AspNetCore.Mvc;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    // GET: api/articles
    [HttpGet("/")]
    public IActionResult Test()
    {
        return Ok("None");
    }
}