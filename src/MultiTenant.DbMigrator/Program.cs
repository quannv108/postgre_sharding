using Core.Dal.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenant.DbMigrator;

public static class Program
{
    public static void Main(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json", false, false);
        configurationBuilder.AddEnvironmentVariables();
        var configuration = configurationBuilder.Build();
        
        var services = new ServiceCollection();
        services.AddCoreDalEf(configuration.GetConnectionString("DefaultConnection")!);

        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("Start Migration...");
        context.Database.Migrate();
        Console.WriteLine("Migration Done!");
        context.Database.CloseConnection();
    }
}