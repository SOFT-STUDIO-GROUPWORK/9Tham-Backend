using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Models;

public class ArticleModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool Visible { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Description { get; set; } = string.Empty;

    public int BloggerId { get; set; } //FK
}