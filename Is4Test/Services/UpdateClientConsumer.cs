using Is4.Domain.Shared.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Is4Test.Services
{
    public class UpdateClientConsumer : IConsumer<ValueEntered>
    {
        private readonly IMemoryCache _cache;
        public UpdateClientConsumer(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task Consume(ConsumeContext<ValueEntered> context)
        {
            _cache.Remove(context.Message.Value);
            Console.WriteLine($"清楚缓存{context.Message.Value}");
        }
    }
}
