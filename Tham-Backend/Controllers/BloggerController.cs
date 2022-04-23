using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloggerController : ControllerBase
{
    private readonly IBloggerRepository _repository;
    private readonly IUserService _userService;

    public BloggerController(IBloggerRepository repository,IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BloggerResponseModel>>> GetBloggers()
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
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<string>> UpdateBlogger(EditBloggerDTO request)
    {
        if (_userService.GetRole() == "User")
        {
            if (_userService.GetEmail() != request.Email) return Unauthorized("You are not owner of this account!");
        }
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