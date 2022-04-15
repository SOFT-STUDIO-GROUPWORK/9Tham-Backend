using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface ICommentRepository
{
    Task<List<CommentModel>> GetCommentsAsync();
    Task<int> AddCommentAsync(CommentModel commentModel);
    Task UpdateCommentAsync(int commentId, CommentModel commentModel);
    Task DeleteCommentAsync(int commentId);
}