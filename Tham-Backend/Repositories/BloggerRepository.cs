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
    
    public async Task<BloggerPaginationModel> GetBloggersPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.Bloggers.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var bloggers = await _context.Bloggers.Skip((page - 1) * (int) perPage).Take(page).ToListAsync();
        var response = new BloggerPaginationModel()
        {
            Bloggers = _mapper.Map<List<BloggerModel>>(bloggers),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
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
        /*
        var newBlogger = new Bloggers
        {
            Id = bloggerId,//XXX: Don't change
            FirstName = bloggerModel.FirstName,
            LastName = bloggerModel.LastName,
            NickName = bloggerModel.NickName,
            Email = bloggerModel.Email,
            Role = bloggerModel.Role,
            IsBanned = bloggerModel.IsBanned,
            PasswordHash = bloggerModel.PasswordHash,//XXX: Don't change
            PasswordSalt = bloggerModel.PasswordSalt//XXX: Don't change
        };
        _context.Bloggers.Update(newBlogger);
        */
        
        var blogger = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Id == bloggerId);
        if (blogger is not null)
        {
            blogger.FirstName = bloggerModel.FirstName;
            blogger.LastName = bloggerModel.LastName;
            blogger.NickName = bloggerModel.NickName;
            blogger.Email = bloggerModel.Email;
            blogger.Role = bloggerModel.Role;
            blogger.IsBanned = bloggerModel.IsBanned;
            await _context.SaveChangesAsync();
        }
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