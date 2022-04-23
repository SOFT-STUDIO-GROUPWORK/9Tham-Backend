namespace Tham_Backend.Models;

public class ArticlePaginationModel
{
    public List<ArticleModel> Articles { get; set; } = new();
    
    public int FirstPage { get; set; }

    public int LastPage { get; set; }

    public int CurrentPage { get; set; }
}