using AutoMapper;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class LikeMapper:Profile
{
    public LikeMapper()
    {
        CreateMap<Likes, LikeModel>().ReverseMap();
    }
}