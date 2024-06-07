using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Dal.EF;

// ReSharper disable once UnusedType.Global
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // This is a design-time factory, so we don't have access to configuration.
        // We need to create the connection string manually.
        const string connectionString = "Host=localhost;Database=multitenantapp;Username=postgres;Password=mypass";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.Config(connectionString);
        
        var options = optionsBuilder.Options;
        return new AppDbContext(options);
    }
}