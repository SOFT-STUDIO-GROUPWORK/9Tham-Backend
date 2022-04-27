using Newtonsoft.Json;

namespace Tham_Backend.Data;

public class Likes
{
    public int Id { get; set; } //XXX: Maybe useless (use ArticleId and BloggerId as composite key)

    [JsonIgnore]
    public Articles Article { get; set; }//NavigationReference
    public int ArticleId { get; set; } //FK
    [JsonIgnore]
    public Bloggers Blogger { get; set; }//NavigationReference
    public int? BloggerId { get; set; } //FK
}