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
    public ArticleTagsController(IArticleTagRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ArticleTagModel>>> GetArticleTags()
    {
        var articleTags = await _repository.GetArticleTagsAsync();
        return Ok(articleTags);
    }

    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<ArticleTagModel>> GetArticleTagById([FromRoute] int id)
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