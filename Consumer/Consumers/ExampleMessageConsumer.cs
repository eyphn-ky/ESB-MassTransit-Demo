using MassTransit;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    //IMesssage türünü implemente eden consumer konfigürasyonda çağrıldıysa consume et
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen Mesaj:{context.Message.Text}");
            return Task.CompletedTask;

        }
    }
}
