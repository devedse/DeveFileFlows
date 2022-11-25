using Microsoft.Extensions.Hosting;
using System;

namespace DeveFileFlows.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var host = new HostBuilder()
                .UseOrleansClient(clientBuilder =>
                {
                    clientBuilder.UseLocalhostClustering()
                        .AddMemoryStreams("chat");
                })
                .Build();

        }
    }
}