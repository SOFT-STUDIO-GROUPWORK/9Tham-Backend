using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticleRepository _repository;
    public ArticlesController(IArticleRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("")]
    public async Task<ActionResult<List<ArticleModel>>> GetArticles()
    {
        var articles = await _repository.GetArticlesAsync();
        return Ok(articles);
    }

    [HttpGet("paginate/{page}")]
    public async Task<ActionResult<List<ArticleModel>>> GetArticles([FromRoute]int page)
    {
        var articles = await _repository.GetPaginatedArticles(page);
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleModel>> GetArticle([FromRoute]int id)
    {
        var article = await _repository.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound("Article not found!");
        }
        return Ok(article);
    }

    [HttpPost("")]
    public async Task<ActionResult<List<ArticleModel>>> AddArticle([FromBody] ArticleModel article)
    {
        var articleId = await _repository.AddBookAsync(article);
        return CreatedAtAction(nameof(GetArticle),new{id=articleId},articleId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<ArticleModel>>> UpdateArticle([FromRoute]int id,[FromBody] ArticleModel articleModel)
    {
        await _repository.UpdateArticleAsync(id, articleModel);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ArticleModel>> DeleteArticle([FromRoute]int id)
    {
        await _repository.DeleteArticleAsync(id);
        return Ok();
    }
}