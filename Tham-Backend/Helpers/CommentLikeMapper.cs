using AutoMapper;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class CommentLikeMapper:Profile
{
    public CommentLikeMapper()
    {
        CreateMap<CommentLikes, CommentLikeModel>().ReverseMap();
    }
}