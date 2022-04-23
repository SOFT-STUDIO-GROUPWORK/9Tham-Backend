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
    public async Task<List<ArticleTagModel>> GetArticleTagsAsync()
    {
        var records = await _context.ArticleTags.ToListAsync();
        return _mapper.Map<List<ArticleTagModel>>(records);
    }
    
    public async Task<ArticleTagPaginationModel> GetArticleTagsPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.ArticleTags.Count() / perPage);
        if (pageCount == 0) pageCount = 1;
        

        var articles = await _context.ArticleTags.Skip((page - 1) * (int) perPage).Take((int)perPage).ToListAsync();
        var response = new ArticleTagPaginationModel()
        {
            ArticleTags = _mapper.Map<List<ArticleTagModel>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
    }

    public async Task<ArticleTagModel?> GetArticleTagByIdAsync(int articleTagId)
    {
        var record = await _context.ArticleTags.FindAsync(articleTagId);
        return _mapper.Map<ArticleTagModel>(record);
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
        var articleTag = new ArticleTags()
        {
            Id = articleTagId
        };
        _context.ArticleTags.Remove(articleTag);
        await _context.SaveChangesAsync();
    }
}