using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Data;

public class Comments
{
    public int Id { get; set; }

    public DateTime Published { get; set; }

    [Required] [StringLength(1000)] public string Content { get; set; } = string.Empty;

    public bool Visible { get; set; }

    public int BloggerId { get; set; } //FK

    public int ArticleId { get; set; } //FK
}