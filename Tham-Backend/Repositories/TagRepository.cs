using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Data;
using Tham_Backend.Models;

namespace Tham_Backend.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public TagRepository(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Tags>> GetTagsAsync()
    {
        var records = await _context.Tags.ToListAsync();
        return _mapper.Map<List<Tags>>(records);
    }

    public async Task<Tags?> GetTagByIdAsync(int tagId)
    {
        var record = await _context.Tags.FindAsync(tagId);
        return _mapper.Map<Tags>(record);
    }

    public async Task<int> AddTagAsync(TagModel tagModel)
    {
        var tag = _mapper.Map<Tags>(tagModel);

        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();

        return tag.Id;
    }

    public async Task UpdateTagAsync(int tagId, TagModel tagModel)
    {
        //tagModel.Id = tagId;//XXX: Will it break?
        var newTag = _mapper.Map<Tags>(tagModel);
        newTag.Id = tagId;
        _context.Tags.Update(newTag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTagAsync(int tagId)
    {
        var tag = new Tags()
        {
            Id = tagId,
        };
        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
    }
}