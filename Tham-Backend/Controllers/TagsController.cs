using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<List<Tags>>> GetTags()
    {
        var tags = await _repository.GetTagsAsync();
        return Ok(tags);
    }
    
    [HttpGet("{search}/{page:min(1)}/{perPage:min(1)}")]
    public async Task<ActionResult<TagPaginationModel>> SearchTags([FromRoute] string search,int page, int perPage)
    {
        var tags = await _repository.SearchTagsPaginated(page,(float)perPage,search);
        return Ok(tags);
    }
    
    [HttpGet("{page:min(1)}/{perPage:min(1)}")]
    public async Task<ActionResult<List<TagPaginationModel>>> GetTags([FromRoute] int page, int perPage)
    {
        var tags = await _repository.GetTagsPaginated(page,(float)perPage);
        return Ok(tags);
    }
    
    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<Tags>> GetTagById([FromRoute]int id)
    {
        var tag = await _repository.GetTagByIdAsync(id);
        if (tag is null)
        {
            return NotFound("Tag not found!");
        }
        return Ok(tag);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddTag([FromBody] TagModel tagModel)
    {
        var tag = await _repository.AddTagAsync(tagModel);
        return CreatedAtAction(nameof(GetTagById), new { id = tag }, tag);
    }
    
    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateTag([FromRoute]int id, [FromBody] TagModel tagModel)
    {
        await _repository.UpdateTagAsync(id, tagModel);
        return Ok();
    }
    
    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteTag([FromRoute]int id)
    {
        await _repository.DeleteTagAsync(id);
        return Ok();
    }
}