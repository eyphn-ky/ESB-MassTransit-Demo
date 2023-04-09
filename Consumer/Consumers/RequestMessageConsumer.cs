using MassTransit;
using Shared.RequestResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            //.....process
            Console.WriteLine(context.Message.Text);

            //işlem tamamlandıktan sonra geriye response mahiyetinde bir mesaj dönmek gerekir. Dolayısıyla context.RespondMessage metodunu kullanarak bun sağlarız.
            await context.RespondAsync<ResponseMessage>(new() { Text=$"{context.Message.MessageNo}. response to request"});
        }
    }
}
