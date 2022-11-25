using AdventureGrainInterfaces;
using DeveFileFlows.GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

Console.WriteLine();
Console.WriteLine("What's your name?");
var name = Console.ReadLine()!;
var player = client.GetGrain<IPlayerGrain>(Guid.NewGuid());
await player.SetName(name);

var room1 = client.GetGrain<IRoomGrain>(0);
await player.SetRoomGrain(room1);

Console.WriteLine(await player.Play("look"));

var result = "Start";
try
{
    while (result is not "")
    {
        var command = Console.ReadLine()!;

        result = await player.Play(command);
        Console.WriteLine(result);
    }
}
finally
{
    await player.Die();
    Console.WriteLine("Game over!");
    await host.StopAsync();
}
