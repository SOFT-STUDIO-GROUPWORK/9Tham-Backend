using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Models;

public class Article
{
    public int Id { get; set; }
    public DateTime Published { get; set; }

    [Required] [StringLength(200)] public string Title { get; set; }

    [Required] [StringLength(4000)] public string Content { get; set; }

    public bool Visible { get; set; }

    public int BloggerId { get; set; } //FK
}