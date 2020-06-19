using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Is4Test.Services
{
    public class CacheCleanService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly IMemoryCache _cache;
        public CacheCleanService(IBusControl busControl, IMemoryCache cache)
        {
            _cache = cache;
            _busControl = busControl;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
