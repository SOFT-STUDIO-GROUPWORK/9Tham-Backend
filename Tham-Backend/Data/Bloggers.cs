using System.ComponentModel.DataAnnotations;
using Tham_Backend.Models;

namespace Tham_Backend.Data;

public class Bloggers
{
    public int Id { get; set; }

    [Required] [StringLength(200)] public virtual string FirstName { get; set; }

    [Required] [StringLength(200)] public virtual string LastName { get; set; }

    [Required] [StringLength(150)] public virtual string NickName { get; set; }

    [Required] [StringLength(256), EmailAddress] public virtual string Email { get; set; }

    public UserRoles Role { get; set; }

    public bool IsBanned { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }
}