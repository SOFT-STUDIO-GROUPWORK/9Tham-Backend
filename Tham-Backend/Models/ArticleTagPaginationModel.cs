namespace Tham_Backend.Models;

public class ArticleTagPaginationModel
{
    public List<ArticleTagModel> ArticleTags { get; set; } = new();
    
    public int FirstPage { get; set; }

    public int LastPage { get; set; }

    public int CurrentPage { get; set; }
}