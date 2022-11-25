using DeveFileFlows.GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public const string InputFileToken = "%INPUTFILETOKEN%";
    public const string OutputFileToken = "%OUTPUTFILETOKEN%";

    public static async Task Main(string[] args)
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
                "C:\\Program Files\\FileOptimizer\\Plugins64\\Leanify.exe",
                $"-p --keep-exif --keep-icc --jpeg-keep-all -i {1} \"{InputFileToken}\""),
            new FileFlowStep(
                "C:\\Program Files\\FileOptimizer\\Plugins64\\jpegoptim.exe",
                $"-o --all-progressive \"{InputFileToken}\""),
            new FileFlowStep(
                "C:\\Program Files\\FileOptimizer\\Plugins64\\jpegtran.exe",
                $"-progressive -optimize -copy all \"{InputFileToken}\" \"{InputFileToken}\""),
            new FileFlowStep(
                "C:\\Program Files\\FileOptimizer\\Plugins64\\mozjpegtran.exe",
                $"-outfile \"{OutputFileToken}\" -progressive -optimize -perfect -optimize -copy all \"{InputFileToken}\"")
        });


        var flowRunnerGrain = client.GetGrain<IFlowRunnerGrain>(0);
        await flowRunnerGrain.RunFlow(0, "C:\\TheFolder\\test.jpg");
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