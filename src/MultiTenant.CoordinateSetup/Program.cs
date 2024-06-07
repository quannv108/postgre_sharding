using Core.Dal.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable UseStringInterpolation

namespace MultiTenant.CoordinateSetup;

class Program
{
    static void Main(string[] args)
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
        
        // for coordinator, need to let it know about workers
        // https://docs.citusdata.com/en/stable/admin_guide/cluster_management.html
        // https://docs.citusdata.com/en/v11.2/installation/multi_node_debian.html#steps-to-be-executed-on-the-coordinator-node
        if(bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var isRunningInContainer) && isRunningInContainer)
        {
            Console.WriteLine("Running in container");
            if (bool.TryParse(Environment.GetEnvironmentVariable("IS_COORDINATOR"), out var isC) && isC)
            {
                ConfigAsCoordinator(context);
            }
            else
            {
                Console.WriteLine("Running as worker");
            }
        }
        else
        {
            Console.WriteLine("Not running in container");
        }
    }
    
    private static void ConfigAsCoordinator(AppDbContext context)
    {
        Console.WriteLine("Running as coordinator");

        // if worker already existing, don't add again
        // var workerNodes = context.Database.SqlQuery<string>($"SELECT \"node_name\" FROM citus_get_active_worker_nodes();").ToList();
        // if(workerNodes.Count > 0)
        // {
        //     Console.WriteLine("worker node already exist, remove them first");
        //     foreach (var existingWorkerNode in workerNodes)
        //     {
        //         context.Database.ExecuteSqlRaw(string.Format("SELECT * from citus_remove_node('{0}', 5432);", existingWorkerNode));
        //     }
        // }
        
        // verify if already coordinated
        // NOTE: this actually always true
        // var isCoordinated = context.Database.SqlQuery<bool>($"SELECT * FROM citus_is_coordinator();").ToList().Single();
        // if (isCoordinated)
        // {
        //     Console.WriteLine("Already coordinated");
        // }
        
        // set coordinator
        var coordinatorHostName = Environment.GetEnvironmentVariable("COORDINATOR");
        context.Database.ExecuteSqlRaw(string.Format("SELECT citus_set_coordinator_host('{0}', 5432);", coordinatorHostName));
        Console.WriteLine("Coordinator set");

        // add workers
        var workers = Environment.GetEnvironmentVariable("WORKERS")?.Split(",")!;
        Console.WriteLine($"workers: {string.Join(",", workers)}");
        foreach (var worker in workers)
        {
            context.Database.ExecuteSqlRaw(string.Format("SELECT * from citus_add_node('{0}', 5432);", worker));
        }

        context.Database.CloseConnection();
        Console.WriteLine("Workers added");
    }
}