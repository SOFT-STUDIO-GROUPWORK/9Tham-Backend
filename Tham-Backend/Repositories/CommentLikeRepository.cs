using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class CommentLikeRepository : ICommentLikeRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    
    public CommentLikeRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CommentLikes>> GetCommentLikesAsync()
    {
        return await _context.CommentLikes.ToListAsync();
    }

    public async Task<CommentLikes?> GetCommentLikeByIdAsync(int commentLikeId)
    {
        return await _context.CommentLikes.FindAsync(commentLikeId);
    }

    public async Task<int> AddCommentLikeAsync(CommentLikeModel commentLikeModel)
    {
        var commentLike = _mapper.Map<CommentLikes>(commentLikeModel);

        await _context.CommentLikes.AddAsync(commentLike);
        await _context.SaveChangesAsync();

        return commentLike.Id;
    }

    public async Task UpdateCommentLikeAsync(int commentLikeId, CommentLikeModel commentLikeModel)
    {
        var newCommentLike = _mapper.Map<CommentLikes>(commentLikeId);
        newCommentLike.Id = commentLikeId;
        _context.CommentLikes.Update(newCommentLike);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentLikeAsync(int commentLikeId)
    {
        var commentLike = await _context.CommentLikes.FindAsync(commentLikeId);
        if (commentLike is not null)
        {
            _context.CommentLikes.Remove(commentLike);
            await _context.SaveChangesAsync();
        }
    }
}