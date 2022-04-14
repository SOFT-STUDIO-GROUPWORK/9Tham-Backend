using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
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

    [HttpGet("{id}")]
    public async Task<ActionResult<BloggerModel>> GetBloggerById([FromRoute]int id)
    {
        var blogger = await _repository.GetBloggerByIdAsync(id);
        if (blogger is null)
        {
            return NotFound("Blogger not found!");
        }
        return Ok(blogger);
    }

    [HttpPost]
    public async Task<IActionResult> AddBlogger([FromBody] BloggerModel bloggerModel)
    {
        var bloggerId = await _repository.AddBloggerAsync(bloggerModel);
        return CreatedAtAction(nameof(GetBloggers), new { id = bloggerId }, bloggerId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlogger([FromRoute]int id, [FromBody] BloggerModel bloggerModel)
    {
        await _repository.UpdateBloggerAsync(id, bloggerModel);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogger([FromRoute]int id)
    {
        await _repository.DeleteBloggerAsync(id);
        return Ok();
    }
}