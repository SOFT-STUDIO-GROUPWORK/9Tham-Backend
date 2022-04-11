namespace Tham_Backend.Models;

public class Comment
{
    public int CommentId { get; set; }
    public string Content { get; set; }
    public bool Visible { get; set; }

    public int BloggerId { get; set; }//FK
    public int ArticleId { get; set; }//FK
}