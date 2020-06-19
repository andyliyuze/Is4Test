using Is4.Domain.Shared.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumerDemo
{
    public class UpdateCustomerConsumer : IConsumer<ValueEntered>
    {
        public async Task Consume(ConsumeContext<ValueEntered> context)
        {
            await Console.Out.WriteLineAsync($"Updating customer: {context.Message}");

            // update the customer address
        }
    }
}
