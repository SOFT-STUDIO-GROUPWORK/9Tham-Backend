using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class ArticleTagRepository : IArticleTagRepository
{
    public Task<List<ArticleTagModel>> GetArticleTagsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddArticleTagAsync(ArticleTagModel articleTagModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArticleTagAsync(int articleTagId, ArticleTagModel articleTagModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArticleTagAsync(int articleTagId)
    {
        throw new NotImplementedException();
    }
}