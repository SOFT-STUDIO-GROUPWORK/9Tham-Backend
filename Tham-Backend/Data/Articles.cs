using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Data;

public class Articles
{
    public int Id { get; set; }
    
    public DateTime Published { get; set; }

    [Required] [StringLength(200)] public string Title { get; set; } = string.Empty;

    [Required] [StringLength(4000)] public string Content { get; set; } = string.Empty;

    public bool Visible { get; set; }
    public int ViewCount { get; set; }

    public int BloggerId { get; set; } //FK
}