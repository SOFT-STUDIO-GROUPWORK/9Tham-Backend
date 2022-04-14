using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class BloggerRepository : IBloggerRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BloggerRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BloggerModel>> GetBloggersAsync()
    {
        var records = await _context.Bloggers.ToListAsync();
        return _mapper.Map<List<BloggerModel>>(records);
    }

    public async Task<BloggerModel?> GetBloggerByIdAsync(int bloggerId)
    {
        var record = await _context.Bloggers.FindAsync(bloggerId);
        return _mapper.Map<BloggerModel>(record);
    }

    public async Task<int> AddBloggerAsync(BloggerModel bloggerModel)
    {
        var blogger = _mapper.Map<Bloggers>(bloggerModel);

        await _context.Bloggers.AddAsync(blogger);
        await _context.SaveChangesAsync();

        return blogger.Id;
    }

    public async Task UpdateBloggerAsync(int bloggerId, BloggerModel bloggerModel)
    {
        //XXX: Some field should not be able to set (consult first)
        var newBlogger = new Bloggers
        {
            Id = bloggerId,
            FirstName = bloggerModel.FirstName,
            LastName = bloggerModel.LastName,
            NickName = bloggerModel.NickName,
            Email = bloggerModel.Email,
            Role = bloggerModel.Role,
            IsBanned = bloggerModel.IsBanned,
            PasswordHash = bloggerModel.PasswordHash,
            PasswordSalt = bloggerModel.PasswordSalt
        };
        _context.Bloggers.Update(newBlogger);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBloggerAsync(int bloggerId)
    {
        var blogger = new Bloggers
        {
            Id = bloggerId
        };
        _context.Bloggers.Remove(blogger);
        await _context.SaveChangesAsync();
    }

    public async Task<BloggerModel?> GetByEmailAsync(string email)
    {
        var record = await _context.Bloggers.SingleOrDefaultAsync(blogger => blogger.Email == email);

        return _mapper.Map<BloggerModel>(record);
    }
}