using DeveFileFlows.GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .UseOrleansClient(clientBuilder =>
                clientBuilder.UseLocalhostClustering())
            .Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IClusterClient>();

        var fileFlow1 = client.GetGrain<IFileFlowGrain>(0);

        var theconfig = await fileFlow1.GetFileFlowConfig();
        Console.WriteLine(theconfig.Name);

        var thenewconfig = new FileFlowConfig()
        {
            Name = "CompressImage",
            Description = "Hallo dit is een onschrijving die het graan beschrijft"
        };
        await fileFlow1.SetFileFlowConfig(thenewconfig);
        await fileFlow1.SetSteps(new List<FileFlowStep>()
        {
            new FileFlowStep(
                "C:\\Program Files\\FileOptimizer\\Plugins64\\jpegoptim.exe",
                $"-o --all-progressive \""+"%INPUTFILETOKEN%"+"\"")
        });


        var flowRunnerGrain = client.GetGrain<IFlowRunnerGrain>(0);
        await flowRunnerGrain.RunFlow(0, "FileName");
        Console.WriteLine("Flow ran :)");

        var result = "";
        try
        {
            while (result is not "exit")
            {
                result = Console.ReadLine()!;
            }
            Console.WriteLine("Exit, byee");
        }
        finally
        {
            await host.StopAsync();
        }
    }
}