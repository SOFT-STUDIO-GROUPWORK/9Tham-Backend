using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    [Authorize(Roles = "Admin,User")]
    public ActionResult<string> WhoAmI()
    {
        var email = _userService.GetEmail();
        return Ok(email);
    }

    [HttpPost("register/admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<string>> RegisterAdmin(RegisterDTO request)
    {
        var user = await _repository.GetBloggerByEmailAsync(request.Email);
        if (user is not null) return Conflict("Admin already register!");

        _authService.CreatePasswordHash(request.Password, out var passwordHash,
            out var passwordSalt);

        BloggerModel newUser = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            NickName = request.NickName,
            Email = request.Email,
            Role = UserRoles.Admin,
            IsBanned = false,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _repository.AddBloggerAsync(newUser);
        return Ok(request.Email);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<string>> RegisterUser(RegisterDTO request)
    {
        var user = await _repository.GetBloggerByEmailAsync(request.Email);
        if (user is not null) return Conflict("User already register!");

        _authService.CreatePasswordHash(request.Password, out var passwordHash,
            out var passwordSalt);

        BloggerModel newUser = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            NickName = request.NickName,
            Email = request.Email,
            Role = UserRoles.User,
            IsBanned = false,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _repository.AddBloggerAsync(newUser);
        return Ok(request.Email);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDTO request)
    {
        var user = await _repository._GetBloggerByEmailAsync(request.Email);
        
        // Check super admin
        var superAdminEmail = "supertham@outlook.com";
        var superAdminPassword = "supertham123";

        if (superAdminEmail == request.Email && superAdminPassword == request.Password && user is null)
        {
            _authService.CreatePasswordHash(superAdminPassword, out var passwordHash,
                out var passwordSalt);

            BloggerModel newUser = new()
            {
                FirstName = "Super",
                LastName = "Admin",
                NickName = "SA",
                Email = superAdminEmail,
                Role = UserRoles.Admin,
                IsBanned = false,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _repository.AddBloggerAsync(newUser);
            user = newUser;
        }
        
        if (user is null) return NotFound("User not found!");
        if (user.IsBanned) return Unauthorized("You are banned");

        if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return BadRequest("Wrong password.");

        var token = _authService.CreateToken(user);
        return Ok(token);
    }
}