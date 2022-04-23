using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class LikeRepository: ILikeRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public LikeRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<Likes>> GetLikesAsync()
    {
        var records = await _context.Likes.ToListAsync();
        return _mapper.Map<List<Likes>>(records);
    }

    public async Task<Likes?> GetLikeByIdAsync(int likeId)
    {
        var record = await _context.Likes.FindAsync(likeId);
        return _mapper.Map<Likes>(record);
    }

    public async Task<int> AddLikeAsync(LikeModel likeModel)
    {
        var like = _mapper.Map<Likes>(likeModel);

        await _context.Likes.AddAsync(like);
        await _context.SaveChangesAsync();

        return like.Id;
    }

    public async Task UpdateLikeAsync(int likeId, LikeModel likeModel)
    {
        //likeModel.Id = likeId;
        var newLike = _mapper.Map<Likes>(likeModel);
        newLike.Id = likeId;
        _context.Likes.Update(newLike);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLikeAsync(int likeId)
    {
        var like = new Likes
        {
            Id = likeId
        };
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
    }
}