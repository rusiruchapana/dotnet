using dotnet_learn.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_learn.Repositories;

public interface IRegionRepository
{
    Task<Region> AddRegion(Region region);
    Task<List<Region>> GetAll();
    Task<Region> GetRegionsById(Guid id);
    Task<Region> UpdateRegion(Guid id, Region region);
    Task<bool> DeleteRegion(Guid id);
}