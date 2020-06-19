using CacheCleaner.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheCleaner.Extensions
{
    public static class HostBuilderExtension
    {
        public static void RegisteService(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<CacheCleanService>();             
        }
    }
}
