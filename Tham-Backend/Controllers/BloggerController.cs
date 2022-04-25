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
    private readonly IAuthService _authService;

    public BloggerController(IBloggerRepository repository,IUserService userService,IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BloggerResponseModel>>> GetBloggers()
    {
        var bloggers = await _repository.GetBloggersAsync();
        return Ok(bloggers);
    }
    
    [HttpGet("{search}/{page:min(1)}/{perPage:min(1)}")]
    public async Task<ActionResult<BloggerPaginationModel>> SearchBloggers([FromRoute] string search,int page, int perPage)
    {
        var bloggers = await _repository.SearchBloggersPaginated(page,(float)perPage,search);
        return Ok(bloggers);
    }
    
    [HttpGet("{page:min(1)}/{perPage:min(1)}")]
    public async Task<ActionResult<BloggerPaginationModel>> GetBloggers([FromRoute] int page, int perPage)
    {
        var bloggers = await _repository.GetBloggersPaginated(page,(float)perPage);
        return Ok(bloggers);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<BloggerResponseModel>> GetBloggerById([FromRoute] string email)
    {
        var blogger = await _repository.GetBloggerByEmailAsync(email);
        if (blogger is null) return NotFound("Blogger not found!");
        return Ok(blogger);
    }
    
    [HttpGet("myself")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BloggerResponseModel>> GetBloggerByJwt()
    {
        var email =  _userService.GetEmail();
        var blogger = await _repository.GetBloggerByEmailAsync(email);
        if (blogger is null) return NotFound("Blogger not found!");
        return Ok(blogger);
    }
    
    [HttpGet("{email}/articles")]
    public async Task<ActionResult<Articles>> GetBloggerArticles([FromRoute] string email)
    {
        var articles = await _repository.GetBloggerArticles(email);
        if (articles is null) return NotFound("Blogger's articles not found!");
        return Ok(articles);
    }

    [HttpPatch("{email}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateBlogger([FromRoute] string email,EditBloggerDTO request)
    {
        if (_userService.GetRole() == "User")
        {
            if (_userService.GetEmail() != request.Email) return Unauthorized("You are not owner of this account!");
        }
        await _repository.UpdateBloggerAsync(email, request);
        return Ok();
    }
    
    [HttpPatch("changePassword")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> ChangePasswordBlogger(ChangePasswordDTO request)
    {
        if (_userService.GetRole() == "User")
        {
            if (_userService.GetEmail() != request.Email) return Unauthorized("You are not owner of this account!");
        }

        var blogger = (await _repository._GetBloggerByEmailAsync(request.Email));
        if (blogger is null) return NotFound("Blogger not found!");
        if (!_authService.VerifyPasswordHash(request.OldPassword, blogger.PasswordHash, blogger.PasswordSalt))
            return BadRequest("Wrong password.");
        
        await _repository.ChangePasswordAsync(request.Email, request);
        return Ok();
    }

    [HttpDelete("{email}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteBlogger([FromRoute] string email)
    {
        if (_userService.GetRole() == "User")
        {
            if (_userService.GetEmail() != email) return Unauthorized("You are not owner of this account!");
        }
        
        var blogger = await _repository._GetBloggerByEmailAsync(email);
        if (blogger is null) return NotFound("Blogger not found!");
        await _repository.DeleteBloggerAsync(email);
        return Ok();
    }
}