namespace Tham_Backend.Models;

public class ArticlePaginationModel
{
    public List<Articles> Articles { get; set; } = new();
    
    public int FirstPage { get; set; }

    public int LastPage { get; set; }

    public int CurrentPage { get; set; }
    
    public int TotalArticles { get; set; }
}