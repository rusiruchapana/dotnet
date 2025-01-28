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

}