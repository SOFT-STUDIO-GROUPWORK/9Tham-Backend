using Tham_Backend.Models;

namespace Tham_Backend;

public class RegisterDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public UserRoles Role { get; set; }
}