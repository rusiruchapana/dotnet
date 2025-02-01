using dotnet_learn.Data;
using dotnet_learn.Models.Domain;
using dotnet_learn.Models.DTO;
using dotnet_learn.Models.DTO.Request;
using dotnet_learn.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_learn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController: ControllerBase
{
    //DI
    private readonly IRegionRepository _regionRepository;
    public RegionController(IRegionRepository regionRepository)
    {
        this._regionRepository = regionRepository;
    }

    
    //Get all regions.
    [HttpGet]
    public async Task<IActionResult>  GetAll()
    {
        var  regions = await _regionRepository.GetAll();
        List<RegionDTO> dtos = new List<RegionDTO>();
        foreach (var region in regions)
        {
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };
            dtos.Add(regionDTO);
        }
        
        return Ok(dtos);
    }
    
    
     //Get a region by Id.
     [HttpGet("{id}")]
     public async Task<IActionResult>  GetRegionsById(Guid id)
     {
         var region = await _regionRepository.GetRegionsById(id);
         var dto = new RegionDTO
         {
             Id = region.Id,
             Code = region.Code,
             Name = region.Name,
             RegionImageUrl = region.RegionImageUrl,
         };
         return Ok(dto);
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

        await _regionRepository.AddRegion(region);

        var dtos = new RegionDTO
        {
            Id = region.Id,
            Code = region.Code, 
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl,
        };
        return Ok(dtos);
    }
    
    
    
    //Update a Region.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegion(Guid id , AddRegionRequestDTO addRegionRequestDto)
    {
        var region = new Region
        {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
        };

        var updatedRegion = await _regionRepository.UpdateRegion(id, region);

        var updatedDto = new RegionDTO
        {
            Id = updatedRegion.Id,
            Code = updatedRegion.Code,
            Name = updatedRegion.Name,
            RegionImageUrl = updatedRegion.RegionImageUrl,
        };
        return Ok(updatedRegion);
    }
    
    
    
    //Delete a region.
    [HttpDelete("{id}")]
    public async Task<IActionResult>  DeleteRegion(Guid id)
    {
        bool isDeleted = await _regionRepository.DeleteRegion(id);
        if (isDeleted == false)
            return NotFound();

        return NoContent();
    }
    

}