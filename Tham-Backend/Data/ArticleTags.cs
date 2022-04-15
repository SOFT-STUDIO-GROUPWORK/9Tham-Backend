namespace Tham_Backend.Data;

public class ArticleTags
{
    public int Id { get; set; } //XXX: Maybe useless (use ArticleId and BloggerId as composite key)
    public int ArticleId { get; set; } //FK
    public int TagId { get; set; } //FK
}