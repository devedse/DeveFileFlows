using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Orleans.Persistence.AdoNet;
using System.Text;

namespace DeveFileFlows.ConsoleApp
{
    internal class Program
    {
        public static async void Main(string[] args)
        {
            await Runner.Run(args);
        }
    }
}