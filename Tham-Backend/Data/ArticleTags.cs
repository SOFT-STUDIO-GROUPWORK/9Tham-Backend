namespace Tham_Backend.Data;

public class ArticleTags
{
    public int Id { get; set; }
    public int ArticleId { get; set; } //FK
    public int TagId { get; set; } //FK
}