using Tham_Backend.Models;

namespace Tham_Backend.Services;

public interface IAuthService
{
    public string CreateToken(BloggerModel user);
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}