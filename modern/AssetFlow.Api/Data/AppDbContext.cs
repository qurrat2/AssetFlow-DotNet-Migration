using AssetFlow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetFlow.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Assignment> Assignments => Set<Assignment>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Department>().ToTable("Departments");
        b.Entity<User>().ToTable("Users");
        b.Entity<Asset>().ToTable("Assets");
        b.Entity<Assignment>().ToTable("Assignments");
    }
}