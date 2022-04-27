using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class ArticleTagRepository : IArticleTagRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ArticleTagRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ArticleTags>> GetArticleTagsAsync()
    {
        var records = await _context.ArticleTags.ToListAsync();
        return _mapper.Map<List<ArticleTags>>(records);
    }

    public async Task<ArticleTags?> GetArticleTagByIdAsync(int articleTagId)
    {
        var record = await _context.ArticleTags.FindAsync(articleTagId);
        return _mapper.Map<ArticleTags>(record);
    }

    public async Task<int> AddArticleTagAsync(ArticleTagModel articleTagModel)
    {
        var articleTag = _mapper.Map<ArticleTags>(articleTagModel);

        await _context.ArticleTags.AddAsync(articleTag);
        await _context.SaveChangesAsync();

        return articleTag.Id;
    }

    public async Task UpdateArticleTagAsync(int articleTagId, ArticleTagModel articleTagModel)
    {
        //articleTagModel.Id = articleTagId;
        var newArticleTag = _mapper.Map<ArticleTags>(articleTagModel);
        newArticleTag.Id = articleTagId;
        _context.ArticleTags.Update(newArticleTag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteArticleTagAsync(int articleTagId)
    {
        var articleTag = await _context.ArticleTags.FindAsync(articleTagId);
        if (articleTag is not null)
        {
            _context.ArticleTags.Remove(articleTag);
            await _context.SaveChangesAsync();
        }
    }
}