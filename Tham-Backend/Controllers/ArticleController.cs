using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Controllers;

[Route("/article/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly DataContext _context;

    public ArticleController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAllArticle()
    {
        return Ok(await _context.Articles.ToListAsync());
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