using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _repository;
    private readonly IUserService _userService;
    public CommentsController(ICommentRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Comments>>> GetComments()
    {
        var comments = await _repository.GetCommentsAsync();
        return Ok(comments);
    }

    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<Comments>> GetCommentById([FromRoute] int id)
    {
        var comment = await _repository.GetCommentByIdAsync(id);
        if (comment is null)
        {
            return NotFound("Comment not found!");
        }

        return Ok(comment);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddComment([FromBody] CommentModel commentModel)
    {
        var commentId = await _repository.AddCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new {id = commentId}, commentId);
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentModel commentModel,  [FromServices]IBloggerRepository bloggerRepository)
    {
        /*var oldComment = await _repository.GetCommentByIdAsync(id);
        if (oldComment is not null)
        {
            var user = await bloggerRepository.GetBloggerByIdAsync((int) oldComment.BloggerId);
            if (user is not null)
            {
                if (_userService.GetRole() == "User")
                {
                    if (_userService.GetEmail() != user.Email) return Unauthorized("You are not owner of this account!");
                }
                await _repository.UpdateCommentAsync(id, commentModel);
                return Ok();
            }
        }
        
        return NotFound();*/
        
        await _repository.UpdateCommentAsync(id, commentModel);
        return Ok();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id, [FromServices]IBloggerRepository bloggerRepository)
    {
        var oldComment = await _repository.GetCommentByIdAsync(id);
        if (oldComment is not null)
        { 
            var user = await bloggerRepository.GetBloggerByIdAsync((int) oldComment.BloggerId);
            if (user is not null)
            {
                if (_userService.GetRole() == "User")
                {
                    if (_userService.GetEmail() != user.Email) return Unauthorized("You are not owner of this account!");
                }
                await _repository.DeleteCommentAsync(id);
                return Ok();
            }
        }
        
        return NotFound();
    }
}