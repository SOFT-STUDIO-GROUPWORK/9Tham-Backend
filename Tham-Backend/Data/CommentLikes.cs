using Newtonsoft.Json;

namespace Tham_Backend.Data;

public class CommentLikes
{
    public int Id { get; set; }

    [JsonIgnore]
    public Comments Comment { get; set; }//NavigationReference
    public int CommentId { get; set; } //FK
    [JsonIgnore]
    public Bloggers Blogger { get; set; }//NavigationReference
    public int? BloggerId { get; set; } //FK
}