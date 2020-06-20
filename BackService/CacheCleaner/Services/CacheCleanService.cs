using Is4.Domain.Shared.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CacheCleaner.Services
{
    public class CacheCleanService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                //amqp://localhost:5672
                cfg.Host(new Uri("amqp://172.17.1.164:30579"));

                cfg.ConfigureJsonDeserializer(settings =>
                {
                    settings.TypeNameHandling = TypeNameHandling.Auto;
                    return settings;
                });
                cfg.Message<ValueEntered>(x =>
                {
                    x.SetEntityName("value-enterd-exchange");
                });
            });

            // Important! The bus must be started before using it!
            await busControl.StartAsync();
            try
            {
                do
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    await busControl.Publish<ValueEntered>(new
                    {
                        Value = value
                    });
                }
                while (true);
            }
            finally
            {
                await busControl.StopAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
