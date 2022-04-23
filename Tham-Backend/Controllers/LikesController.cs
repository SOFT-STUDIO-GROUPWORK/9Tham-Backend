using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly ILikeRepository _repository;

    public LikesController(ILikeRepository repository)
    {
        _repository = repository;
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
    public async Task<IActionResult> AddLike([FromBody] LikeModel likeModel)
    {
        var likeId = await _repository.AddLikeAsync(likeModel);
        return CreatedAtAction(nameof(GetLikeById), new {id = likeId}, likeId);
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateLike([FromRoute] int id, [FromBody] LikeModel likeModel)
    {
        await _repository.UpdateLikeAsync(id, likeModel);
        return Ok();
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> DeleteLike([FromRoute] int id)
    {
        await _repository.DeleteLikeAsync(id);
        return Ok();
    }
}