using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

    public async Task<List<Articles>> GetArticlesAsync()
    {
        var records = await _context.Articles.Include(a=>a.ArticleTags).ToListAsync();
        return _mapper.Map<List<Articles>>(records);
    }

    public async Task<ArticlePaginationModel> GetArticlesPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.Articles.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var articles = await _context.Articles.Skip((page - 1) * (int) perPage).Take((int)perPage).Include(a=>a.ArticleTags).ToListAsync();
        var response = new ArticlePaginationModel()
        {
            Articles = _mapper.Map<List<Articles>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
    }
    
    public async Task<ArticlePaginationModel> SearchArticlesPaginated(int page,float perPage, string search)
    {
        var qureyWhere = await _context.Articles.Include(a=>a.Blogger).Include(a=>a.ArticleTags).Where(e => e.Title.Contains(search) || e.Content.Contains(search)).ToListAsync();
        var pageCount = Math.Ceiling(qureyWhere.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var articles = qureyWhere.Skip((page - 1) * (int) perPage).Take((int)perPage);
        var response = new ArticlePaginationModel()
        {
            Articles = _mapper.Map<List<Articles>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
    }

    public async Task<Articles?> GetArticleByIdAsync(int articleId)
    {
        var article = await _context.Articles.Include(a=>a.Blogger).Include(a=>a.ArticleTags).Include(a=>a.Comments).FirstOrDefaultAsync(x=>x.Id==articleId);
        if (article is not null)
        {
            article.ViewCount++;
            await _context.SaveChangesAsync();
        }
        return _mapper.Map<Articles>(article);
    }
    
    public async Task<int> AddArticleAsync(ArticleModel articleModel)
    {
        var article = _mapper.Map<Articles>(articleModel);
        article.Published = DateTime.Now;

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
            article.Description = articleModel.Description;
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