using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Models;

public class Comment
{
    public int Id { get; set; }

    public DateTime Published { get; set; }

    [Required] [StringLength(1000)] public string Content { get; set; }

    public bool Visible { get; set; }

    public int BloggerId { get; set; } //FK

    public int ArticleId { get; set; } //FK
}