using DeveFileFlows.Common.Grains;
using DeveFileFlows.Common.Pocos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using System;

namespace DeveFileFlows.Client
{
    public class Program
    {
        public const string InputFileToken = "%INPUTFILETOKEN%";
        public const string OutputFileToken = "%OUTPUTFILETOKEN%";

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var host = new HostBuilder()
                .UseOrleansClient(clientBuilder =>
                {
                    clientBuilder.UseLocalhostClustering()
                        .AddMemoryStreams("chat");
                })
                .Build();



            var client = host.Services.GetRequiredService<IClusterClient>();

            Console.WriteLine("Connecting...");
            await host.StartAsync();
            Console.WriteLine("Connected :)");



            var fileFlow1 = client.GetGrain<IFileFlowGrain>(0);
            await fileFlow1.SetFileFlowConfig(new FileFlowConfig("CompressImage"));
            await fileFlow1.SetSteps(new List<FileFlowStep>()
            {
                new FileFlowStep(
                    "C:\\Program Files\\FileOptimizer\\Plugins64\\jpegoptim.exe",
                    $"-o --all-progressive \"{ImageOptimizationStep.InputFileToken}\"")
            });





            await ProcessLoopAsync(client);

            await host.StopAsync();
        }



        static async Task ProcessLoopAsync(IClusterClient client)
        {
            string? input = null;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input.StartsWith("/exit"))
                {
                    break;
                }



                //await SendMessage(client, input);
            } while (input is not "/exit");
        }
    }
}