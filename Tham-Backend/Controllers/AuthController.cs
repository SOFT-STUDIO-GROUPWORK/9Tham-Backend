using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IBloggerRepository _repository;
    private readonly IUserService _userService;


    public AuthController(IBloggerRepository repository, IUserService userService,
        IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
        _repository = repository;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<string> WhoAmI()
    {
        var email = _userService.GetEmail();
        return Ok(email);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<BloggerModel>> Register(RegisterDTO request)
    {
        var user = await _repository.GetByEmailAsync(request.Email);
        if (user is not null) return Conflict("User already register!");

        _authService.CreatePasswordHash(request.Password, out var passwordHash,
            out var passwordSalt);

        BloggerModel newUser = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            NickName = request.NickName,
            Email = request.NickName,
            Role = request.Role,
            IsBanned = false,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _repository.AddBloggerAsync(newUser);
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginDTO request)
    {
        var user = await _repository.GetByEmailAsync(request.Email);
        if (user is null) return NotFound("User not found!");

        if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return BadRequest("Wrong password.");

        var token = _authService.CreateToken(user);
        return Ok(token);
    }
}