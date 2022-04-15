using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface ILikeRepository
{
    Task<List<LikeModel>> GetLikesAsync();
    Task<int> AddLikeAsync(LikeModel likeModel);
    Task UpdateLikeAsync(int likeId, LikeModel likeModel);
    Task DeleteLikeAsync(int likeId);
}