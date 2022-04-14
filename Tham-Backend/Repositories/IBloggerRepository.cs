using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IBloggerRepository
{
    Task<List<BloggerModel>> GetBloggersAsync();
    Task<BloggerModel?> GetBloggerByIdAsync(int bloggerId);
    Task<int> AddBloggerAsync(BloggerModel bloggerModel);
    Task UpdateBloggerAsync(int bloggerId, BloggerModel bloggerModel);
    Task DeleteBloggerAsync(int bloggerId);
}