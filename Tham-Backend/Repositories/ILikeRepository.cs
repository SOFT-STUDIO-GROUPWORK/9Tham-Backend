using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface ILikeRepository
{
    Task<List<LikeModel>> GetLikesAsync();
    Task<LikeModel?> GetLikeByIdAsync(int likeId);
    Task<int> AddLikeAsync(LikeModel likeModel);
    Task UpdateLikeAsync(int likeId, LikeModel likeModel);
    Task DeleteLikeAsync(int likeId);
}