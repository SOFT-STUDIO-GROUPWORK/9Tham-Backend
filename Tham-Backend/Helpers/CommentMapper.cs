using AutoMapper;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class CommentMapper : Profile
{
    public CommentMapper()
    {
        CreateMap<Comments, CommentModel>().ReverseMap();
    }
}