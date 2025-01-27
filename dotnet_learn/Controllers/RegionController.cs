using dotnet_learn.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_learn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController: ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Region> regions = new List<Region>
        {
            new Region
            {
                Id = Guid.NewGuid(),
                Code = "test1",
                Name = "test1",
                RegionImageUrl = "test1"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Code = "test2",
                Name = "test2",
                RegionImageUrl = "test2"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Code = "test3",
                Name = "test3",
                RegionImageUrl = "test3"
            }
        };
        return Ok(regions);
    }
}