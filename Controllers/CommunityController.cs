using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Mapper;
using Reddit.Models;

namespace Reddit.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CommunityController : ControllerBase
{
    private readonly ApplcationDBContext _context;
    private readonly IMapper _mapper;

    public CommunityController(ApplcationDBContext applcationDbContext, IMapper mapper)
    {
        _context = applcationDbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Community>>> GetCommunitys()
    {
        var data = await _context.Communities.ToListAsync();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Community>> GetCommunity(int id)
    {
        var community = await _context.Communities.FirstOrDefaultAsync(c => c.Id == id);
        
        if (community == null)
        {
            return NotFound();
        }

        return Ok(community);
    }

    [HttpPost]
    public async Task<ActionResult<Community>> AddComunity(CreateComunityDto createComunityDto)
    {
        var community = _mapper.ToCommunity(createComunityDto);
         await _context.Communities.AddAsync(community);
         await _context.SaveChangesAsync();
         
         return CreatedAtAction("GetCommunity", new { id = community.Id }, community);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCommunity(int id)
    {
        var community = await _context.Communities.FindAsync(id);
        if (community == null)
        {
            return NotFound();
        }

        _context.Communities.Remove(community);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Community>> UpdateCommunity(int id, Community community)
    {
        if (id != community.Id)
        {
            return BadRequest();
        }

        var existingCommunity = await _context.Communities.FindAsync(id);

        if (existingCommunity == null)
        {
            return NotFound();
        }
        
        existingCommunity.Name = community.Name;
        existingCommunity.Description = community.Description;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CommunityExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    private bool CommunityExists(int id)
    {
        return _context.Communities.Any(e => e.Id == id);
    }
}