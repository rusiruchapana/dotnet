using dotnet_learn.Data;
using dotnet_learn.Models.Domain;
using dotnet_learn.Models.DTO;
using dotnet_learn.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_learn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController: ControllerBase
{
    //DI
    private readonly DotNetLearnDbContext _context;
    public RegionController(DotNetLearnDbContext context)
    {
        _context = context;
    }

    
    //Get all regions.
    [HttpGet]
    public async Task<IActionResult>  GetAll()
    {
        List<Region> regions = await _context.Regions.ToListAsync();
        List<RegionDTO> regionDtos = new List<RegionDTO>();
        
        foreach (var region in regions)
        {
            RegionDTO regionDto = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };
            regionDtos.Add(regionDto);
        }
        return Ok(regionDtos);
    }

    //Get a region by Id.
    [HttpGet("{id}")]
    public async Task<IActionResult>  GetRegionsById(Guid id)
    {
        Region region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        if(region == null)
            return NotFound();

        RegionDTO regionDto = new RegionDTO
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };
        
        return Ok(regionDto);
    }



    //Create region.
    [HttpPost]
    public async Task<IActionResult>  AddRegion(AddRegionRequestDTO addRegionRequestDto)
    {
        var region = new Region
        {
            Id = Guid.NewGuid(),
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl,
        };
        
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();

        RegionDTO regionDto = new RegionDTO
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl,
        };
        
        return Ok(regionDto);
    }


    //Update a Region.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegion(Guid id , AddRegionRequestDTO addRegionRequestDto)
    {
        Region region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        region.Code = addRegionRequestDto.Code;
        region.Name = addRegionRequestDto.Name;
        region.RegionImageUrl = addRegionRequestDto.RegionImageUrl;

        _context.Regions.Update(region);
        await _context.SaveChangesAsync();

        RegionDTO regionDto = new RegionDTO
        {
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl,
        };
        return Ok(regionDto);
    }

    
    //Delete a region.
    [HttpDelete("{id}")]
    public async Task<IActionResult>  DeleteRegion(Guid id)
    {
        Region region = await _context.Regions.FindAsync(id);
        if(region == null)
            return NotFound();

        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    

}