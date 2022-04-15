using AutoMapper;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class ArticleTagMapper: Profile
{
    public ArticleTagMapper()
    {
        CreateMap<ArticleTags, ArticleTagModel>().ReverseMap();
    }
}