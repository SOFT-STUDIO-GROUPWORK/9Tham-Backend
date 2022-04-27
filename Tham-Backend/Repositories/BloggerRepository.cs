using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;
using Tham_Backend.Services;

namespace Tham_Backend.Repositories;

public class BloggerRepository : IBloggerRepository
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BloggerRepository(DataContext context, IMapper mapper,IAuthService authService,IUserService userService)
    {
        _context = context;
        _authService = authService;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<List<BloggerResponseModel>> GetBloggersAsync()
    {
        var records = await _context.Bloggers.ToListAsync();
        return _mapper.Map<List<BloggerResponseModel>>(records);
    }
    
    public async Task<BloggerResponseModel?> GetBloggerByIdAsync(int id)
    {
        var record = await _context.Bloggers.FindAsync(id);
        return _mapper.Map<BloggerResponseModel>(record);
    }
    
    public async Task<List<Articles>?> GetBloggerArticles(string email)
    {
        var record = await _context.Bloggers.Include(b=>b.Articles).FirstOrDefaultAsync(x=>x.Email==email);
        var articles = record?.Articles.ToList();
        if (articles is null) return null;
        //XXX: prevent returning Blogger data (bad practice here but it work)
        foreach(var article in articles)
        {
            article.Blogger = null;
        }
        return articles;

    }

    public async Task<BloggerResponseModel?> GetBloggerByEmailAsync(string email)
    {
        var record = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Email==email);
        return _mapper.Map<BloggerResponseModel>(record);
    }
    
    public async Task<BloggerModel?> _GetBloggerByEmailAsync(string email)
    {
        var record = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Email==email);
        return _mapper.Map<BloggerModel>(record);
    }
    
    public async Task<BloggerPaginationModel> GetBloggersPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.Bloggers.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var bloggers = await _context.Bloggers.Skip((page - 1) * (int) perPage).Take((int)perPage).ToListAsync();
        var response = new BloggerPaginationModel()
        {
            Bloggers = _mapper.Map<List<BloggerResponseModel>>(bloggers),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
    }

    public async Task<BloggerPaginationModel> SearchBloggersPaginated(int page, float perPage, string search)
    {
        var qureyWhere = await _context.Bloggers.Where(e => e.Email.Contains(search) || e.NickName.Contains(search) || e.FirstName.Contains(search)|| e.LastName.Contains(search)).ToListAsync();
        var pageCount = Math.Ceiling(qureyWhere.Count / perPage);
        if (pageCount == 0) pageCount = 1;

        var bloggers = qureyWhere.Skip((page - 1) * (int) perPage).Take((int)perPage);
        var response = new BloggerPaginationModel()
        {
            Bloggers = _mapper.Map<List<BloggerResponseModel>>(bloggers),
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

    public async Task UpdateBloggerAsync(string email, EditBloggerDTO editBloggerDto)
    {
        var blogger = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Email == email);

        if (blogger is not null)
        {
            blogger.Email = editBloggerDto.Email;
            blogger.FirstName = editBloggerDto.FirstName;
            blogger.LastName = editBloggerDto.LastName;
            blogger.NickName = editBloggerDto.NickName;
            blogger.Role = editBloggerDto.Role;
            blogger.IsBanned = editBloggerDto.IsBanned;
            blogger.ImageUrl = editBloggerDto.ImageUrl;
            blogger.BannerUrl = editBloggerDto.BannerUrl;
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task ChangePasswordAsync(string email, ChangePasswordDTO changePasswordDTO)
    {
        var blogger = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Email == email);

        _authService.CreatePasswordHash(changePasswordDTO.Password, out var passwordHash,
            out var passwordSalt);
        
        if (blogger is not null)
        {
            blogger.PasswordHash = passwordHash;
            blogger.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBloggerAsync(string email)
    {
        var blogger = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Email == email);
        if (blogger is not null)
        {
            _context.Bloggers.Remove(blogger);
            await _context.SaveChangesAsync();
        }
    }
}