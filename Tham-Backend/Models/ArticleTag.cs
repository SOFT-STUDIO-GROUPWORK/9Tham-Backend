namespace Tham_Backend.Models;

public class ArticleTag
{
    public int ArticleTagId { get; set; }
    public int ArticleId { get; set; }//FK
    public int TagId { get; set; }//FK
}