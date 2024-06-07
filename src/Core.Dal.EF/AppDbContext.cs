using Core.Models.Tenants;
using Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Core.Dal.EF;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("citus");

        // it's required that `tenant_id` and `id` should be primary key
        // because we will use `tenant_id` as a shard key
        modelBuilder.Entity<AppUser>()
            .HasKey(x => new { x.Id, x.TenantId })
            .HasName("pk_app_users");
    }
}