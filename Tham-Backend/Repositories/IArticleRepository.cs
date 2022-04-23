using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IArticleRepository
{
    Task<List<Articles>> GetArticlesAsync();
    Task<ArticlePaginationModel> GetArticlesPaginated(int page, float perPage);
    Task<Articles?> GetArticleByIdAsync(int articleId);
    Task<int> AddArticleAsync(ArticleModel articleModel);
    Task UpdateArticleAsync(int articleId, ArticleModel articleModel);
    Task DeleteArticleAsync(int articleId);
}