using AutoMapper;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class AnnouncementMapper:Profile
{
    public AnnouncementMapper()
    {
        CreateMap<Announcements, AnnouncementModel>().ReverseMap();
    }
}