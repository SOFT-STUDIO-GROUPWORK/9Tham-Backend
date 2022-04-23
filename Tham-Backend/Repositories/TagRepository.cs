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
    
    public async Task<TagPaginationModel> SearchTagsPaginated(int page,float perPage, string search)
    {
        var qureyWhere = await _context.Tags.Where(e => e.Name.Contains(search)).ToListAsync();
        var pageCount = Math.Ceiling(qureyWhere.Count() / perPage);
        if (pageCount == 0) pageCount = 1;

        var tags = qureyWhere.Skip((page - 1) * (int) perPage).Take((int)perPage);
        var response = new TagPaginationModel()
        {
            Tags = _mapper.Map<List<TagModel>>(tags),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
    }
    
    public async Task<TagPaginationModel> GetTagsPaginated(int page,float perPage)
    {
        var pageCount = Math.Ceiling(_context.Tags.Count() / perPage);
        if (pageCount == 0) pageCount = 1;
        

        var tags = await _context.Tags.Skip((page - 1) * (int) perPage).Take((int)perPage).ToListAsync();
        var response = new TagPaginationModel()
        {
            Tags = _mapper.Map<List<TagModel>>(tags),
            CurrentPage = page,
            FirstPage = 1,
            LastPage = (int) pageCount
        };
        return response;
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