namespace Tham_Backend.Data;

public class Likes
{
    public int Id { get; set; } //XXX: Maybe useless (use ArticleId and BloggerId as composite key)
    public int ArticleId { get; set; } //FK
    public int BloggerId { get; set; } //FK
}