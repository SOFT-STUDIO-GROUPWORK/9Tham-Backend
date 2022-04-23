using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleTagsController : ControllerBase
{
    private readonly IArticleTagRepository _repository;
    private readonly IArticleRepository _articleRepository;
    public ArticleTagsController(IArticleTagRepository repository, IArticleRepository articleRepository)
    {
        _repository = repository;
        _articleRepository = articleRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ArticleTags>>> GetArticleTags()
    {
        var articleTags = await _repository.GetArticleTagsAsync();
        return Ok(articleTags);
    }

    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<ArticleTags>> GetArticleTagById([FromRoute] int id)
    {
        var articleTag = await _repository.GetArticleTagByIdAsync(id);
        if (articleTag is null)
        {
            return NotFound("Article tag not found!");
        }

        return Ok(articleTag);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddArticleTag([FromBody] ArticleTagModel articleTagModel)
    {
        //prevent adding multiple identical tag to an article
        var article = await _articleRepository.GetArticleByIdAsync(articleTagModel.ArticleId);

        if (article is null) return NotFound("The article is not existed");
        
        var existingTag = article.ArticleTags.FirstOrDefault(articleTag => articleTag.TagId == articleTagModel.TagId);
        if (existingTag is not null) return BadRequest("The article already has this tag");
        
        var articleTagId = await _repository.AddArticleTagAsync(articleTagModel);
        return CreatedAtAction(nameof(GetArticleTagById), new {id = articleTagId}, articleTagId);

    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateArticleTag([FromRoute] int id, [FromBody] ArticleTagModel articleTagModel)
    {
        await _repository.UpdateArticleTagAsync(id, articleTagModel);
        return Ok();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteArticleTag([FromRoute] int id)
    {
        await _repository.DeleteArticleTagAsync(id);
        return Ok();
    }
}