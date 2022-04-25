namespace Tham_Backend.Models;

public class TagPaginationModel
{
    public List<Tags> Tags { get; set; } = new();
    
    public int FirstPage { get; set; }

    public int LastPage { get; set; }

    public int CurrentPage { get; set; }
}