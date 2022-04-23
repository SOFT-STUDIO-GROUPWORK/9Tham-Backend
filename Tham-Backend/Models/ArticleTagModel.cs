namespace Tham_Backend.Models;

public class ArticleTagModel
{
    //public int Id { get; set; }
    public int ArticleId { get; set; } //FK
    public int TagId { get; set; } //FK
}