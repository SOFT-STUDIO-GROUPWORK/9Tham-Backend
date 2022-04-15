using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagRepository _repository;
    
    public TagsController(ITagRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TagModel>>> GetTags()
    {
        var tags = await _repository.GetTagsAsync();
        return Ok(tags);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TagModel>> GetTagById([FromRoute]int id)
    {
        var tag = await _repository.GetTagByIdAsync(id);
        if (tag is null)
        {
            return NotFound("Tag not found!");
        }
        return Ok(tag);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTag([FromBody] TagModel tagModel)
    {
        var tag = await _repository.AddTagAsync(tagModel);
        return CreatedAtAction(nameof(GetTagById), new { id = tag }, tag);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag([FromRoute]int id, [FromBody] TagModel tagModel)
    {
        await _repository.UpdateTagAsync(id, tagModel);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag([FromRoute]int id)
    {
        await _repository.DeleteTagAsync(id);
        return Ok();
    }
}