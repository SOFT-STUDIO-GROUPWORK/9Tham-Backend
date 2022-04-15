namespace Tham_Backend.Data;

public class Likes
{
    public int Id { get; set; }
    public int ArticleId { get; set; } //FK
    public int BloggerId { get; set; } //FK
}