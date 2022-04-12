namespace Tham_Backend.Models;

public class ArticleResponse
{
    public List<Article> Articles { get; set; } = new();

    public int Pages { get; set; }

    public int CurrentPage { get; set; }
}