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
        var records = await _context.Articles.Include(a=>a.ArticleTags).ThenInclude(at=>at.Tag).ToListAsync();
        return _mapper.Map<List<Articles>>(records);
    }

    public async Task<ArticlePaginationModel> GetArticlesPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.Articles.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var articles = await _context.Articles.Skip((page - 1) * (int) perPage).Take((int)perPage).Include(a=>a.ArticleTags).ThenInclude(at=>at.Tag).Where(e => e.Visible==true).ToListAsync();
        var response = new ArticlePaginationModel()
        {
            Articles = _mapper.Map<List<Articles>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount,
            TotalArticles = articles.Count
        };
        return response;
    }
    
    public async Task<ArticlePaginationModel> GetReverseArticlesPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.Articles.Count() / perPage);
        if (pageCount == 0) pageCount = 1;
        var reverseDB = await _context.Articles.Include(a=>a.ArticleTags).ThenInclude(at=>at.Tag).Where(e => e.Visible==true).ToListAsync();
        reverseDB = Enumerable.Reverse(reverseDB).ToList();
        var articles = reverseDB.Skip((page - 1) * (int) perPage).Take((int) perPage);
        var response = new ArticlePaginationModel()
        {
            Articles = _mapper.Map<List<Articles>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount,
            TotalArticles = articles.Count()
        };
        return response;
    }
    
    public async Task<ArticlePaginationModel> SearchArticlesPaginated(int page,float perPage, string search)
    {
        var result = await _context.Articles.Include(a => a.Blogger).Include(a => a.ArticleTags).ThenInclude(at => at.Tag)
            .Where(e => e.Visible == true).Where(e => e.Title.Contains(search) || e.Content.Contains(search)).ToListAsync();
        

        var pageCount = Math.Ceiling(result.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var articles = result.Skip((page - 1) * (int) perPage).Take((int)perPage);
        var response = new ArticlePaginationModel()
        {
            Articles = _mapper.Map<List<Articles>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount,
            TotalArticles = articles.Count()
        };
        return response;
    }
    
    public async Task<ArticlePaginationModel> SearchReverseArticlesPaginated(int page,float perPage, string search)
    {
        var result = await _context.Articles.Include(a => a.Blogger).Include(a => a.ArticleTags)
            .ThenInclude(at => at.Tag)
            .Where(e => e.Visible == true).Where(e => e.Title.Contains(search) || e.Content.Contains(search))
            .ToListAsync();
        
        result = Enumerable.Reverse(result).ToList();
        var pageCount = Math.Ceiling(result.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var articles = result.Skip((page - 1) * (int) perPage).Take((int)perPage);
        var response = new ArticlePaginationModel()
        {
            Articles = _mapper.Map<List<Articles>>(articles),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount,
            TotalArticles = articles.Count()
        };
        return response;
    }

    public async Task<Articles?> GetArticleByIdAsync(int articleId)
    {
        //FIXME: Comments list is not included in the response
        var article = await _context.Articles.Include(a=>a.Blogger).Include(a=>a.ArticleTags).ThenInclude(at=>at.Tag).Include(a=>a.Comments).ThenInclude(c=>c.Blogger).FirstOrDefaultAsync(x=>x.Id==articleId);
        if (article is not null)
        {
            article.ViewCount++;
            await _context.SaveChangesAsync();
        }
        return article;
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
            article.ThumbnailUrl = articleModel.ThumbnailUrl;
            article.Description = articleModel.Description;
            article.BloggerId = articleModel.BloggerId;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteArticleAsync(int articleId)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(x=>x.Id == articleId);
        if (article is not null)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}