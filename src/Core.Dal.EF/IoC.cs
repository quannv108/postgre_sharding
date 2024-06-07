using Core.Dal.EF.Repositories;
using Core.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Core.Dal.EF;

public static class IoC
{
    public static IServiceCollection AddCoreDalEf(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.Config(connectionString);
        });

        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IAppUserRepository, UserRepository>();

        return services;
    }
    
    public static void Config(this DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();
        
        optionsBuilder.UseNpgsql(dataSource);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
}