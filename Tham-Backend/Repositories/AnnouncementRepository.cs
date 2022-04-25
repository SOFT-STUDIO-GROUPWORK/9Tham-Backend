using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class AnnouncementRepository: IAnnouncementRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AnnouncementRepository(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<Announcements>> GetAnnouncementsAsync()
    {
        return await _context.Announcements.ToListAsync();
    }

    public async Task<Announcements?> GetAnnouncementsByIdAsync(int announcementId)
    {
        return await _context.Announcements.FirstOrDefaultAsync(x=>x.Id==announcementId);
    }

    public async Task<int> AddAnnouncementAsync(AnnouncementModel announcementModel)
    {
        var announcement = _mapper.Map<Announcements>(announcementModel);

        await _context.Announcements.AddAsync(announcement);
        await _context.SaveChangesAsync();

        return announcement.Id;
    }

    public async Task UpdateAnnouncementAsync(int announcementId, AnnouncementModel announcementModel)
    {
        var newAnnouncement = _mapper.Map<Announcements>(announcementModel);
        newAnnouncement.Id = announcementId;
        _context.Announcements.Update(newAnnouncement);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAnnouncementAsync(int announcementId)
    {
        var announcement = await _context.Announcements.FirstOrDefaultAsync(x=>x.Id==announcementId);
        if (announcement is not null)
        {
        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();
        }
    }
}