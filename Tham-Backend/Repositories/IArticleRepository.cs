using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IArticleRepository
{
    Task<List<ArticleModel>> GetArticlesAsync();
    Task<ArticleResponseModel> GetPaginatedArticles(int page, float perPage);
    Task<ArticleModel?> GetArticleByIdAsync(int articleId);
    Task<int> AddArticleAsync(ArticleModel articleModel);
    Task UpdateArticleAsync(int articleId, ArticleModel articleModel);
    Task DeleteArticleAsync(int articleId);
}