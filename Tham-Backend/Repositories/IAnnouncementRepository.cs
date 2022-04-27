using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public interface IAnnouncementRepository
{
    Task<List<Announcements>> GetAnnouncementsAsync();
    Task<Announcements?> GetAnnouncementsByIdAsync(int announcementId);
    Task<int> AddAnnouncementAsync(AnnouncementModel announcementModel);
    Task UpdateAnnouncementAsync(int announcementId, AnnouncementModel announcementModel);
    Task DeleteAnnouncementAsync(int announcementId);
}