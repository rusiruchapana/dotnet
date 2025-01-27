using dotnet_learn.Data;
using dotnet_learn.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_learn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController: ControllerBase
{
    private readonly DotNetLearnDbContext _context;
    public RegionController(DotNetLearnDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        List<Region> regions = _context.Regions.ToList();
        return Ok(regions);
    }
}