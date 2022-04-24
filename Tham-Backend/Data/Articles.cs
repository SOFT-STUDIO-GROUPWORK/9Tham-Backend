using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Tham_Backend.Data;

public class Articles
{
    public int Id { get; set; }
    
    public DateTime Published { get; set; }

    [Required] [StringLength(200)] public string Title { get; set; } = string.Empty;

    [Required] [StringLength(10000)] public string Content { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool Visible { get; set; }
    public int ViewCount { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;
    
    public Bloggers Blogger { get; set; }//NavigationReference (since BloggerId got no 's' after Blogger => Name must exactly match)
    public int BloggerId { get; set; } //FK
    
    public List<Comments> Comments { get; set; }//for FK on Comments table
    public List<ArticleTags> ArticleTags { get; set; }//for FK on ArticleTags table
    public List<Likes> Likes { get; set; }//for FK on Likes table
}