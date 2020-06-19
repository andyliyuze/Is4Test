using CacheCleaner.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace CacheCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(HostBuilderExtension.RegisteService)
                .UseConsoleLifetime()
                .Build();
            host.Run();
        }
    }
}
