using dotnet_learn.Data;
using dotnet_learn.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_learn.Repositories;

public class RegionRepositoryImpl: IRegionRepository
{
    private readonly DotNetLearnDbContext _context;

    public RegionRepositoryImpl(DotNetLearnDbContext context)
    {
        this._context = context;
    }
    
    public async Task<Region> AddRegion(Region region)
    {
        var var1 = await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
        var region2 = var1.Entity;
        return region2;
    }

    public async Task<List<Region>> GetAll()
    {
        var regions = await _context.Regions.ToListAsync();
        return regions;
    }

    public async Task<Region> GetRegionsById(Guid id)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(r=>r.Id==id);
        return region;
    }

    public async Task<Region> UpdateRegion(Guid id, Region region)
    {
        var beforeUpdating = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        beforeUpdating.Code = region.Code;
        beforeUpdating.Name = region.Name;
        beforeUpdating.RegionImageUrl = region.RegionImageUrl;

        _context.Regions.Update(beforeUpdating);
        await _context.SaveChangesAsync();
        
        return beforeUpdating;
    }

    public async Task<bool> DeleteRegion(Guid id)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        if (region == null)
            return false;
        
        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();
        return true;
    }
}