using System.Reflection;
using AdventureSetup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var mapFileName = Path.Combine(path, "AdventureMap.json");

        switch (args.Length)
        {
            default:
                Console.WriteLine("*** Invalid command line arguments.");
                return -1;
            case 0:
                break;
            case 1:
                mapFileName = args[0];
                break;
        }

        if (!File.Exists(mapFileName))
        {
            Console.WriteLine("*** File not found: {0}", mapFileName);
            return -2;
        }

        // Configure the host
        using var host = Host.CreateDefaultBuilder(args)
            .UseOrleans(siloBuilder =>
            {
                siloBuilder
                .UseLocalhostClustering()
                .AddAdoNetGrainStorage("ultraStore", options =>
                {
                    options.Invariant = "System.Data.SqlClient";
                    options.ConnectionString = "Server=tcp:db,1433;Initial Catalog=OrleansDb;Persist Security Info=False;User ID=SA;Password=yourStrong(!)Password;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
                });
            })
            .Build();

        // Start the host
        await host.StartAsync();

        Console.WriteLine("Map file name is '{0}'.", mapFileName);
        Console.WriteLine("Setting up Adventure, please wait ...");

        // Initialize the game world
        var client = host.Services.GetRequiredService<IGrainFactory>();
        var adventure = new AdventureGame(client);
        await adventure.Configure(mapFileName);

        Console.WriteLine("Setup completed.");
        Console.WriteLine("Now you can launch the client.");

        // Exit when any key is pressed
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        await host.StopAsync();

        return 0;
    }
}