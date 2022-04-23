﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;
using Tham_Backend.Services;

namespace Tham_Backend.Repositories;

public class BloggerRepository : IBloggerRepository
{
    private readonly IAuthService _authService;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BloggerRepository(DataContext context, IMapper mapper,IAuthService authService)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<List<BloggerModel>> GetBloggersAsync()
    {
        var records = await _context.Bloggers.ToListAsync();
        return _mapper.Map<List<BloggerModel>>(records);
    }

    public async Task<BloggerModel?> GetBloggerByEmailAsync(string email)
    {
        var record = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Email==email);
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

    public async Task UpdateBloggerAsync(string email, EditBloggerDTO editBloggerDto)
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
        
        var blogger = await _context.Bloggers.FirstOrDefaultAsync(x=>x.Email == email);

        _authService.CreatePasswordHash(editBloggerDto.Password, out var passwordHash,
            out var passwordSalt);
        
        if (blogger is not null)
        {
            blogger.FirstName = editBloggerDto.FirstName;
            blogger.LastName = editBloggerDto.LastName;
            blogger.NickName = editBloggerDto.NickName;
            blogger.Email = editBloggerDto.Email;
            blogger.Role = editBloggerDto.Role;
            blogger.IsBanned = editBloggerDto.IsBanned;
            blogger.PasswordHash = passwordHash;
            blogger.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBloggerAsync(string email)
    {
        var blogger = new Bloggers
        {
            Email = email
        };
        _context.Bloggers.Remove(blogger);
        await _context.SaveChangesAsync();
    }
}