namespace Tham_Backend.Models;

public class BloggerPaginationModel
{
    public List<BloggerModel> Bloggers { get; set; } = new();
    
    public int FirstPage { get; set; }

    public int LastPage { get; set; }

    public int CurrentPage { get; set; }
}