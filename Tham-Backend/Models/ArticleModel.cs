using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Models;

public class ArticleModel
{
    //public int Id { get; set; }
    
    //public DateTime Published { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public bool Visible { get; set; }
    public string ThumbnailUrl { get; set; }
    //public int ViewCount { get; set; }

    public int BloggerId { get; set; } //FK
    //public BloggerModel Blogger { get; set; }//use to send blogger data to client
}