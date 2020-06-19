using MassTransit;
using Newtonsoft.Json;
using System;

namespace ConsumerDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            { 
                cfg.Host(new Uri("amqp://172.17.1.164:30579"));

                cfg.ReceiveEndpoint("customer_update_queue", e =>
                {
                    e.Bind("value-enterd-exchange");
                    e.Consumer<UpdateCustomerConsumer>();
                });                
            });

             
            busControl.Start();
            Console.ReadLine();
        }
    }
}
