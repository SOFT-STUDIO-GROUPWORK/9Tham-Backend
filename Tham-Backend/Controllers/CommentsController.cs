using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _repository;
    public CommentsController(ICommentRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CommentModel>>> GetComments()
    {
        var comments = await _repository.GetCommentsAsync();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentModel>> GetCommentById([FromRoute] int id)
    {
        var comment = await _repository.GetCommentByIdAsync(id);
        if (comment is null)
        {
            return NotFound("Comment not found!");
        }

        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CommentModel commentModel)
    {
        var commentId = await _repository.AddCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new {id = commentId}, commentId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentModel commentModel)
    {
        await _repository.UpdateCommentAsync(id, commentModel);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        await _repository.DeleteCommentAsync(id);
        return Ok();
    }
}