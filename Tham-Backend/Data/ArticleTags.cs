namespace Tham_Backend.Data;

public class ArticleTags
{
    public int Id { get; set; } //XXX: Maybe useless (use ArticleId and BloggerId as composite key)

    public Articles Article { get; set; }//NavigationReference
    public int ArticleId { get; set; } //FK

    public Tags Tag { get; set; }//NavigationReference
    public int TagId { get; set; } //FK
}