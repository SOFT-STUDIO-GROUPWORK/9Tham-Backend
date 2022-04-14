namespace Tham_Backend.Models;

public class BloggerModel
{
    public int Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }

    public virtual string NickName { get; set; }

    public virtual string Email { get; set; }

    public UserRoles Role { get; set; }

    public bool IsBanned { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }
}