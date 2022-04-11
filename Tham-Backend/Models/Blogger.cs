namespace Tham_Backend.Models;

public class Blogger
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public UserRoles Role { get; set; }
    
    public bool IsBanned { get; set; }
}