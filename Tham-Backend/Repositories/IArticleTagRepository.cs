using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IArticleTagRepository
{
    Task<List<ArticleTags>> GetArticleTagsAsync();
    Task<ArticleTags?> GetArticleTagByIdAsync(int articleTagId);
    Task<int> AddArticleTagAsync(ArticleTagModel articleTagModel);
    Task UpdateArticleTagAsync(int articleTagId, ArticleTagModel articleTagModel);
    Task DeleteArticleTagAsync(int articleTagId);
}