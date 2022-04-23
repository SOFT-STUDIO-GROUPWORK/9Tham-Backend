using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleRepository _repository;

    public ArticlesController(IArticleRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<ArticleModel>>> GetArticles()
    {
        var articles = await _repository.GetArticlesAsync();
        return Ok(articles);
    }

    [HttpGet("{page:min(1)}/{perPage:min(1)}")]
    public async Task<ActionResult<List<ArticleModel>>> GetArticles([FromRoute] int page, int perPage)
    {
        var articles = await _repository.GetPaginatedArticles(page,(float)perPage);
        return Ok(articles);
    }

    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<ArticleModel>> GetArticle([FromRoute] int id)
    {
        var article = await _repository.GetArticleByIdAsync(id);
        if (article is null)
        {
            return NotFound("Article not found!");
        }
        return Ok(article);
    }

    // ! Fix incoming model
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<ArticleModel>>> AddArticle([FromBody] ArticleModel article)
    {
        var articleId = await _repository.AddArticleAsync(article);
        return CreatedAtAction(nameof(GetArticle), new {id = articleId}, articleId);
    }

    // ! Fix incoming model
    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<ArticleModel>>> UpdateArticle([FromRoute] int id,
        [FromBody] ArticleModel articleModel)
    {
        await _repository.UpdateArticleAsync(id, articleModel);
        return Ok();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ArticleModel>> DeleteArticle([FromRoute] int id)
    {
        await _repository.DeleteArticleAsync(id);
        return Ok();
    }
}