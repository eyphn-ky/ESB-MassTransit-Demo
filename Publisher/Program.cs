using MassTransit;
using Shared.Messages;

string rabbitMQUri = "amqps://oxystewb:g8ZxUdsUXLX2FY6u0PfNIRftYF7dc2HC@moose.rmq.cloudamqp.com/oxystewb";
string queueName = "example-queue";
//bütün işlemler bus sınıfı üzerinden yürütülecektir.
IBusControl bus =Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});
//ilgili kuyruğa mesaj gönderme işlemi
ISendEndpoint sendEndpoint=await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderilecek Mesaj : ");
string message = Console.ReadLine();
//tek bir kuyruğa mesaj yollanacaksa Send metodu kullanılır.
await sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
});
Console.Read();

