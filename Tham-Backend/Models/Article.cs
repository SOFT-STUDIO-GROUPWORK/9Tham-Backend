namespace Tham_Backend.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool Visible { get; set; }
    public int BloggerId { get; set; } //FK
}