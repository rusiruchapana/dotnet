using dotnet_learn.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnet_learn.Data;

public class DotNetLearnDbContext: DbContext
{
    public DotNetLearnDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public  DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}