using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IBloggerRepository
{
    Task<List<BloggerResponseModel>> GetBloggersAsync();
    Task<BloggerPaginationModel> GetBloggersPaginated(int page, float perPage);
    Task<BloggerPaginationModel> SearchBloggersPaginated(int page, float perPage, string search);
    public Task<List<Articles>?> GetBloggerArticles(string email);
    Task<BloggerResponseModel?> GetBloggerByEmailAsync(string email);
    Task<BloggerModel?> _GetBloggerByEmailAsync(string email);
    Task<int> AddBloggerAsync(BloggerModel bloggerModel);
    Task UpdateBloggerAsync(string email, EditBloggerDTO editBloggerDto);
    Task ChangePasswordAsync(string email, ChangePasswordDTO changePasswordDto);
    Task DeleteBloggerAsync(string email);
}