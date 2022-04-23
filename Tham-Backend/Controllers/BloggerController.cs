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

    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<BloggerModel>> GetBloggerById([FromRoute] int id)
    {
        var blogger = await _repository.GetBloggerByIdAsync(id);
        if (blogger is null) return NotFound("Blogger not found!");
        return Ok(blogger);
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBlogger([FromRoute] int id, [FromBody] BloggerModel bloggerModel)
    {
        await _repository.UpdateBloggerAsync(id, bloggerModel);
        return Ok();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBlogger([FromRoute] int id)
    {
        await _repository.DeleteBloggerAsync(id);
        return Ok();
    }
}