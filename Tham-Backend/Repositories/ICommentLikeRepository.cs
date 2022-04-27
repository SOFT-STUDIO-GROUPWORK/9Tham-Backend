using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface ICommentLikeRepository
{
    Task<List<CommentLikes>> GetCommentLikesAsync();
    Task<CommentLikes?> GetCommentLikeByIdAsync(int commentLikeId);
    Task<int> AddCommentLikeAsync(CommentLikeModel commentLikeModel);
    Task UpdateCommentLikeAsync(int commentLikeId, CommentLikeModel commentLikeModel);
    Task DeleteCommentLikeAsync(int commentLikeId);
}