using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Tham_Backend.Data;

public class Comments
{
    public int Id { get; set; }

    public DateTime Published { get; set; }

    [Required] [StringLength(1000)] public string Content { get; set; } = string.Empty;

    public bool Visible { get; set; }
    
    public Bloggers Blogger { get; set; }//NavigationReference
    public int? BloggerId { get; set; } //FK (must be nullable to prevent multiple cascade paths)

    [JsonIgnore]
    public Articles Article { get; set; }//NavigationReference
    public int ArticleId { get; set; } //FK
}