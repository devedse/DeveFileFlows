using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        // Configure the host
        using var host = Host.CreateDefaultBuilder(args)
            .UseOrleans(siloBuilder =>
            {
                siloBuilder
                .UseLocalhostClustering()
                .AddAdoNetGrainStorage("ultraStore", options =>
                {
                    options.Invariant = "System.Data.SqlClient";
                    options.ConnectionString = "Server=localhost;Initial Catalog=OrleansDb;Persist Security Info=False;User ID=SA;Password=yourStrong(!)Password;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
                });
            })
            .Build();

        // Start the host
        await host.StartAsync();

        // Initialize the game world
        var client = host.Services.GetRequiredService<IGrainFactory>();


        Console.WriteLine("Setup completed.");
        Console.WriteLine("Now you can launch the client.");

        // Exit when any key is pressed
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        await host.StopAsync();

        return 0;
    }
}