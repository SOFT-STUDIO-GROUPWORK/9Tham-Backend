using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IArticleRepository
{
    Task<List<Articles>> GetArticlesAsync();
    Task<ArticlePaginationModel> GetArticlesPaginated(int page, float perPage);
    Task<ArticlePaginationModel> GetReverseArticlesPaginated(int page, float perPage);
    Task<ArticlePaginationModel> SearchArticlesPaginated(int page, float perPage, string search);
    Task<ArticlePaginationModel> SearchReverseArticlesPaginated(int page, float perPage, string search);
    Task<Articles?> GetArticleByIdAsync(int articleId);
    Task<int> AddArticleAsync(ArticleModel articleModel);
    Task UpdateArticleAsync(int articleId, ArticleModel articleModel);
    Task DeleteArticleAsync(int articleId);
}