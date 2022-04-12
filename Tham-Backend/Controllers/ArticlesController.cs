using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly DataContext _context;

    public ArticlesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("paginate/{page}")]
    public async Task<ActionResult<List<Article>>> GetArticles(int page)
    {
        var perPage = 10f;
        var pageCount = Math.Ceiling(_context.Articles.Count() / perPage);
        if (pageCount == 0) pageCount = 1;
        var articles = await _context.Articles.Skip((page - 1) * (int) perPage).Take(page).ToListAsync();
        var response = new ArticleResponse
        {
            Articles = articles,
            CurrentPage = page,
            Pages = (int) pageCount
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return BadRequest("Article not found!");
        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<List<Article>>> AddArticle([FromBody] Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return Ok(article);
    }

    [HttpPut]
    public async Task<ActionResult<List<Article>>> UpdateArticle([FromBody] Article request)
    {
        var dbArticle = await _context.Articles.FindAsync(request.Id);
        if (dbArticle == null) return BadRequest("Article not found!");
        dbArticle.Content = request.Content;
        dbArticle.Title = request.Title;
        dbArticle.Visible = request.Visible;
        dbArticle.BloggerId = request.BloggerId;
        await _context.SaveChangesAsync();
        return Ok(dbArticle);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Article>> DeleteArticle(int id)
    {
        var dbArticle = await _context.Articles.FindAsync(id);
        if (dbArticle == null) return BadRequest("Article not found!");
        _context.Articles.Remove(dbArticle);
        await _context.SaveChangesAsync();
        return Ok(dbArticle);
    }
}