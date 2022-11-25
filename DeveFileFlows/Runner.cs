using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveFileFlows
{
    public class Runner
    {
        public static async Task Run(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                 .UseOrleans(siloBuilder =>
                 {
                     siloBuilder
                         .UseLocalhostClustering()
                         //.AddMemoryGrainStorage("PubSubStore")
                         .AddAdoNetGrainStorage("OrleansStorage", options =>
                         {
                             options.Invariant = "System.Data.SqlClient";
                             options.ConnectionString = "Server=tcp:db,1433;Initial Catalog=OrleansDb;Persist Security Info=False;User ID=SA;Password=yourStrong(!)Password;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
                         });
                     //.AddMemoryStreams("files");
                 })
                 .RunConsoleAsync();
        }
    }
}
