using dotnet_learn.Data;
using dotnet_learn.Models.Domain;
using dotnet_learn.Models.DTO;
using dotnet_learn.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        List<Region> regions = _context.Regions.ToList();
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
    [HttpGet("{regionId}")]
    public IActionResult GetRegionsById([FromRoute(Name = "regionId")] Guid id)
    {
        Region region = _context.Regions.FirstOrDefault(r => r.Id == id);
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
    public IActionResult AddRegion(AddRegionRequestDTO addRegionRequestDto)
    {
        var region = new Region
        {
            Id = Guid.NewGuid(),
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl,
        };
        
        _context.Regions.Add(region);
        _context.SaveChanges();

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
    [HttpPut("{regionId}")]
    public IActionResult UpdateRegion(Guid id , AddRegionRequestDTO addRegionRequestDto)
    {
        Region region = _context.Regions.FirstOrDefault(r => r.Id == id);
        region.Code = addRegionRequestDto.Code;
        region.Name = addRegionRequestDto.Name;
        region.RegionImageUrl = addRegionRequestDto.RegionImageUrl;

        _context.Regions.Update(region);
        _context.SaveChanges();

        RegionDTO regionDto = new RegionDTO
        {
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl,
        };
        return Ok(regionDto);
    }

    
    //Delete a region.
    [HttpDelete("{regionId}")]
    public IActionResult DeleteRegion([FromRoute(Name = "regionId")] Guid id)
    {
        Region region = _context.Regions.Find(id);
        if(region == null)
            return NotFound();

        _context.Regions.Remove(region);
        _context.SaveChanges();
        return NoContent();
    }
    

}