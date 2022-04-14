using AutoMapper;
using Tham_Backend.Data;
using Tham_Backend.Models;

namespace Tham_Backend.Helpers;

public class TagMapper: Profile
{
    public TagMapper()
    {
        CreateMap<Tags, TagModel>().ReverseMap();
    }
}