namespace Tham_Backend.Models;

public class TagPaginationModel
{
    public List<TagModel> Tags { get; set; } = new();
    
    public int FirstPage { get; set; }

    public int LastPage { get; set; }

    public int CurrentPage { get; set; }
}