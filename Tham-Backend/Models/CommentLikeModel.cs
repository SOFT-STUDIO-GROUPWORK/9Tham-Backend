namespace Tham_Backend.Models;

public class CommentLikeModel
{
    public int CommentId { get; set; } //FK
    public int BloggerId { get; set; } //FK
}