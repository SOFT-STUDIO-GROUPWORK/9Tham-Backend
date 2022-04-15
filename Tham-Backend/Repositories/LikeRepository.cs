using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class LikeRepository: ILikeRepository
{
    public Task<List<LikeModel>> GetLikesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddLikeAsync(LikeModel likeModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLikeAsync(int likeId, LikeModel likeModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLikeAsync(int likeId)
    {
        throw new NotImplementedException();
    }
}