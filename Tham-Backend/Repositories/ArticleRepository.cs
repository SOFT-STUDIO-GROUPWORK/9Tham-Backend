using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Data;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ArticleRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ArticleModel>> GetArticlesAsync()
    {
        var records = await _context.Articles.Include(article => article.Blogger).ToListAsync();
        return _mapper.Map<List<ArticleModel>>(records);
    }

    public async Task<ArticlePaginationModel> GetArticlesPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.Articles.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var articles = await _context.Articles.Skip((page - 1) * (int) perPage).Take(page).ToListAsync();
        var response = new ArticlePaginationModel()
        {
            Articles = _mapper.Map<List<ArticleModel>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
    }

    public async Task<ArticleModel?> GetArticleByIdAsync(int articleId)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(x=>x.Id==articleId);
        if (article is not null)
        {
            article.ViewCount++;
            await _context.SaveChangesAsync();
        }
        return _mapper.Map<ArticleModel>(article);
    }
    
    public async Task<int> AddArticleAsync(ArticleModel articleModel)
    {
        var article = _mapper.Map<Articles>(articleModel);
        article.Published = new DateTime();

        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();

        return article.Id;
    }

    public async Task UpdateArticleAsync(int articleId, ArticleModel articleModel)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(x=>x.Id == articleId);
        if (article is not null)
        {
            article.Title = articleModel.Title;
            article.Content = articleModel.Content;
            article.Visible = articleModel.Visible;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteArticleAsync(int articleId)
    {
        var article = new Articles()
        {
            Id = articleId,
        };
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }
}