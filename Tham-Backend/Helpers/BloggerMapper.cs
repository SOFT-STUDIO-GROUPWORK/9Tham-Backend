using AutoMapper;
using Tham_Backend.Data;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class BloggerMapper: Profile
{
    public BloggerMapper()
    {
        CreateMap<Bloggers, BloggerModel>().ReverseMap();
    }
}