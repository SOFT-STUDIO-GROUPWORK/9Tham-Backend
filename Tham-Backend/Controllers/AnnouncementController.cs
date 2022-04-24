using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementRepository _repository;
    
    public AnnouncementController(IAnnouncementRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Announcements>>> Get()
    {
        var announcements = await _repository.GetAnnouncementsAsync();
        return Ok(announcements);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Announcements>> GetById(int id)
    {
        var announcement = await _repository.GetAnnouncementsByIdAsync(id);
        if (announcement is null) return NotFound("Announcement not found");
        return Ok(announcement);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] AnnouncementModel announcementModel)
    {
        var announcementId = await _repository.AddAnnouncementAsync(announcementModel);
        return CreatedAtAction(nameof(GetById), new {id = announcementId}, announcementId);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AnnouncementModel announcementModel)
    {
        await _repository.UpdateAnnouncementAsync(id, announcementModel);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAnnouncementAsync(id);
        return Ok();
    }
}

