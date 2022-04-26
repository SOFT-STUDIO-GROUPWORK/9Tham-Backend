using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;
using Tham_Backend.Services;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly ILikeRepository _repository;
    private readonly IUserService _userService;

    public LikesController(ILikeRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Likes>>> GetLikes()
    {
        var likes = await _repository.GetLikesAsync();
        return Ok(likes);
    }

    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<Likes>> GetLikeById([FromRoute] int id)
    {
        var like = await _repository.GetLikeByIdAsync(id);
        if (like is null)
        {
            return NotFound("Like not found!");
        }

        return Ok(like);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> ToggleLike([FromRoute] int articleId, [FromRoute] int bloggerId, [FromServices]IArticleRepository articleRepository)
    {
        var article = await articleRepository.GetArticleByIdAsync(articleId);
        if (article is null) return NotFound();
        
        var matchedLike = article.Likes.Find(like => like.BloggerId == bloggerId);
        if (matchedLike is null)
        {
            //add like
            var likeModel = new LikeModel()
            {
                    ArticleId = articleId,
                    BloggerId = bloggerId
            };
            await _repository.AddLikeAsync(likeModel);
        }
        else
        {
            //remove like
            await _repository.DeleteLikeAsync(matchedLike.Id);
        }

        return Ok();
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddLike([FromBody] LikeModel likeModel)
    {
        var likeId = await _repository.AddLikeAsync(likeModel);
        return CreatedAtAction(nameof(GetLikeById), new {id = likeId}, likeId);
    }

    [HttpGet("toggleLikes/{articleId:min(1)}/{bloggerId:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateLike([FromRoute] int id, [FromBody] LikeModel likeModel,[FromServices]IBloggerRepository bloggerRepository)
    {
        var oldLike = await _repository.GetLikeByIdAsync(id);
        if (oldLike is not null)
        {
            var user = await bloggerRepository.GetBloggerByIdAsync((int) oldLike.BloggerId);
            if (user is not null)
            {
                if (_userService.GetRole() == "User")
                {
                    if (_userService.GetEmail() != user.Email) return Unauthorized("You are not owner of this like!");
                }
                await _repository.UpdateLikeAsync(id, likeModel);
                return Ok();
            }
        }

        return NotFound();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteLike([FromRoute] int id,[FromServices]IBloggerRepository bloggerRepository)
    {
        var oldLike = await _repository.GetLikeByIdAsync(id);
        if (oldLike is not null)
        {
            var user = await bloggerRepository.GetBloggerByIdAsync((int) oldLike.BloggerId);
            if (user is not null)
            {
                if (_userService.GetRole() == "User")
                {
                    if (_userService.GetEmail() != user.Email) return Unauthorized("You are not owner of this like!");
                }
                await _repository.DeleteLikeAsync(id);
                return Ok();
            }
        }

        return NotFound();
    }
}