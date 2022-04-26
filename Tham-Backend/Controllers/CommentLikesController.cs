using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentLikesController : ControllerBase
{
    private readonly ICommentLikeRepository _repository;
    private readonly IUserService _userService;

    public CommentLikesController(ICommentLikeRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
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
    
    [HttpGet("/toggleLikes/{commentId:min(1)}/{bloggerId:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> ToggleLike([FromRoute] int commentId, [FromRoute] int bloggerId, [FromServices]ICommentRepository commentRepository)
    {
        var comment = await commentRepository.GetCommentByIdAsync(commentId);
        if (comment is null) return NotFound();
        
        var matchedLike = comment.CommentLikes.Find(like => like.BloggerId == bloggerId);
        if (matchedLike is null)
        {
            //add like
            var commentLikeModel = new CommentLikeModel()
            {
                CommentId = commentId,
                BloggerId = bloggerId
            };
            await _repository.AddCommentLikeAsync(commentLikeModel);
        }
        else
        {
            //remove like
            await _repository.DeleteCommentLikeAsync(matchedLike.Id);
        }

        return Ok();
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateCommentLike([FromRoute] int id, [FromBody] CommentLikeModel commentLikeModel, [FromServices]IBloggerRepository bloggerRepository)
    {
        var oldCommentLike = await _repository.GetCommentLikeByIdAsync(id);
        if (oldCommentLike is not null)
        { 
            var user = await bloggerRepository.GetBloggerByIdAsync((int) oldCommentLike.BloggerId);
            if (user is not null)
            {
                if (_userService.GetRole() == "User")
                {
                    if (_userService.GetEmail() != user.Email) return Unauthorized("You are not owner of this comment like!");
                }
                await _repository.UpdateCommentLikeAsync(id, commentLikeModel);
                return Ok();
            }
        }

        return NotFound();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteCommentLike([FromRoute] int id,[FromServices]IBloggerRepository bloggerRepository)
    {var oldCommentLike = await _repository.GetCommentLikeByIdAsync(id);
        if (oldCommentLike is not null)
        { 
            var user = await bloggerRepository.GetBloggerByIdAsync((int) oldCommentLike.BloggerId);
            if (user is not null)
            {
                if (_userService.GetRole() == "User")
                {
                    if (_userService.GetEmail() != user.Email) return Unauthorized("You are not owner of this comment like!");
                }
                await _repository.DeleteCommentLikeAsync(id);
                return Ok();
            }
        }
        
        return NotFound();
    }
}