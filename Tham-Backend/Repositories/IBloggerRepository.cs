﻿using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IBloggerRepository
{
    Task<List<BloggerResponseModel>> GetBloggersAsync();
    Task<BloggerPaginationModel> GetBloggersPaginated(int page, float perPage);
    Task<BloggerResponseModel?> GetBloggerByEmailAsync(string email);
    Task<BloggerModel?> _GetBloggerByEmailAsync(string email);
    Task<int> AddBloggerAsync(BloggerModel bloggerModel);
    Task UpdateBloggerAsync(string email, EditBloggerDTO editBloggerDto);
    Task DeleteBloggerAsync(string email);
}