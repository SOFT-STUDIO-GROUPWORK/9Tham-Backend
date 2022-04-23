using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<ActionResult<List<ArticleModel>>> GetArticles()
    {
        var articles = await _repository.GetArticlesAsync();
        return Ok(articles);
    }
    
    [HttpGet("{search}")]
    public async Task<ActionResult<List<ArticleModel>>> SearchArticles([FromRoute] string search)
    {
        var articles = await _repository.Search();
        return Ok(articles);
    }

    [HttpGet("{page:min(1)}/{perPage:min(1)}")]
    public async Task<ActionResult<List<ArticlePaginationModel>>> GetArticles([FromRoute] int page, int perPage)
    {
        var articles = await _repository.GetArticlesPaginated(page,(float)perPage);
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

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<List<ArticleModel>>> AddArticle([FromBody] ArticleModel article)
    {
        var articleId = await _repository.AddArticleAsync(article);
        return CreatedAtAction(nameof(GetArticle), new {id = articleId}, articleId);
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<List<ArticleModel>>> UpdateArticle([FromRoute] int id,
        [FromBody] ArticleModel articleModel)
    {
        await _repository.UpdateArticleAsync(id, articleModel);
        return Ok();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ArticleModel>> DeleteArticle([FromRoute] int id)
    {
        
        await _repository.DeleteArticleAsync(id);
        return Ok();
    }
}