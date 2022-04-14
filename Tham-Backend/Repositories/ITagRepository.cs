using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface ITagRepository
{
    Task<List<TagModel>> GetTagsAsync();
    Task<TagModel?> GetTagByIdAsync(int tagId);
    Task<int> AddTagAsync(TagModel tagModel);
    Task UpdateTagAsync(int tagId, TagModel tagModel);
    Task DeleteTagAsync(int tagId);
}