using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BloggerController : ControllerBase
{
    private readonly IBloggerRepository _repository;

    public BloggerController(IBloggerRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<BloggerModel>>> GetBloggers()
    {
        var bloggers = await _repository.GetBloggersAsync();
        return Ok(bloggers);
    }
    
    [HttpGet("{page:min(1)}/{perPage:min(1)}")]
    public async Task<ActionResult<List<BloggerPaginationModel>>> GetBloggers([FromRoute] int page, int perPage)
    {
        var bloggers = await _repository.GetBloggersPaginated(page,(float)perPage);
        return Ok(bloggers);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<BloggerModel>> GetBloggerById([FromRoute] string email)
    {
        var blogger = await _repository.GetBloggerByEmailAsync(email);
        if (blogger is null) return NotFound("Blogger not found!");
        return Ok(blogger);
    }

    [HttpPut("{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<string>> UpdateBlogger(EditBloggerDTO request)
    {
        await _repository.UpdateBloggerAsync(request.Email, request);
        return Ok();
    }

    [HttpDelete("{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBlogger([FromRoute] string email)
    {
        await _repository.DeleteBloggerAsync(email);
        return Ok();
    }
}