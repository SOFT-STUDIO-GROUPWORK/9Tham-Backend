using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Data;

public class Announcements
{
    public int Id { get; set; }
    [Required] public string ImageUrl { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}