namespace Tham_Backend.Models;

public class LikeModel
{
    //public int Id { get; set; }
    public int ArticleId { get; set; } //FK
    public int BloggerId { get; set; } //FK
}