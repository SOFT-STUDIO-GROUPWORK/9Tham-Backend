using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class CommentRepository:ICommentRepository
{
    public Task<List<CommentModel>> GetCommentsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddCommentAsync(CommentModel commentModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCommentAsync(int commentId, CommentModel commentModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCommentAsync(int commentId)
    {
        throw new NotImplementedException();
    }
}