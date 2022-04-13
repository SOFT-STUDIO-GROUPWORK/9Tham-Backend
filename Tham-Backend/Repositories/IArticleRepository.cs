using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IArticleRepository
{
    Task<List<ArticleModel>> GetArticlesAsync();
    Task<ArticleResponseModel> GetPaginatedArticles(int page);
    Task<ArticleModel?> GetArticleByIdAsync(int articleId);
    Task<int> AddBookAsync(ArticleModel articleModel);
    Task UpdateArticleAsync(int articleId, ArticleModel articleModel);
    Task DeleteArticleAsync(int articleId);
}