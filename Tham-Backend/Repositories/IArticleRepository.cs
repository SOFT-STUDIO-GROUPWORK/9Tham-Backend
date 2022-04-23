using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IArticleRepository
{
    Task<List<Articles>> GetArticlesAsync();
    Task<ArticlePaginationModel> GetArticlesPaginated(int page, float perPage);
    Task<ArticlePaginationModel> SearchArticlesPaginated(int page, float perPage, string search);
    Task<Articles?> GetArticleByIdAsync(int articleId);
    Task<int> AddArticleAsync(Articles articleModel);
    Task UpdateArticleAsync(int articleId, Articles articleModel);
    Task DeleteArticleAsync(int articleId);
}