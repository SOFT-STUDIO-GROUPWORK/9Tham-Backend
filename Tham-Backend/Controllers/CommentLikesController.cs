using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentLikesController : ControllerBase
{
    private readonly ICommentLikeRepository _repository;

    public CommentLikesController(ICommentLikeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentLikes>>> GetCommentLikes()
    {
        var commentLikes = await _repository.GetCommentLikesAsync();
        return Ok(commentLikes);
    }

    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<CommentLikes>> GetCommentLikeById([FromRoute] int id)
    {
        var commentLike = await _repository.GetCommentLikeByIdAsync(id);
        if (commentLike is null)
        {
            return NotFound("CommentLike not found!");
        }

        return Ok(commentLike);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddCommentLike([FromBody] CommentLikeModel commentLikeModel)
    {
        var commentLikeId = await _repository.AddCommentLikeAsync(commentLikeModel);
        return CreatedAtAction(nameof(GetCommentLikeById), new {id = commentLikeId}, commentLikeId);
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateCommentLike([FromRoute] int id, [FromBody] CommentLikeModel commentLikeModel)
    {
        await _repository.UpdateCommentLikeAsync(id, commentLikeModel);
        return Ok();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteCommentLike([FromRoute] int id)
    {
        await _repository.DeleteCommentLikeAsync(id);
        return Ok();
    }
}