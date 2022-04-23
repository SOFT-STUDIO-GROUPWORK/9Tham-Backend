using AutoMapper;
using Tham_Backend.Data;
using Tham_Backend.Models;
using Tham_Backend.Repositories;

namespace Tham_Backend.Helpers;

public class BloggerMapper: Profile
{
    public BloggerMapper()
    {
        CreateMap<Bloggers, BloggerModel>().ReverseMap();
        CreateMap<Bloggers, BloggerResponseModel>().ReverseMap();
    }
}