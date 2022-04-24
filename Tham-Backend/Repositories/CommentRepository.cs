using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class CommentRepository:ICommentRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CommentRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<Comments>> GetCommentsAsync()
    {
        var records = await _context.Comments.ToListAsync();
        return _mapper.Map<List<Comments>>(records);
    }

    public async Task<Comments?> GetCommentByIdAsync(int commentId)
    {
        var record = await _context.Comments.FindAsync(commentId);
        return _mapper.Map<Comments>(record);
    }

    public async Task<int> AddCommentAsync(CommentModel commentModel)
    {
        var comment = _mapper.Map<Comments>(commentModel);
        comment.Published = DateTime.Now;

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return comment.Id;
    }

    public async Task UpdateCommentAsync(int commentId, CommentModel commentModel)
    {
        //commentModel.Id = commentId;
        var newComment = _mapper.Map<Comments>(commentModel);
        newComment.Id = commentId;
        _context.Comments.Update(newComment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        var comment = new Comments
        {
            Id = commentId
        };
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }
}