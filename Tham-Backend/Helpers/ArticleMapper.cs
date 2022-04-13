using AutoMapper;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class ArticleMapper : Profile
{
    public ArticleMapper()
    {
        CreateMap<Articles, ArticleModel>().ReverseMap();
    }
}