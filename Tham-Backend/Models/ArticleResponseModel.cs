namespace Tham_Backend.Models;

public class ArticleResponseModel
{
    public List<ArticleModel> Articles { get; set; } = new();

    public int Pages { get; set; }

    public int CurrentPage { get; set; }
}