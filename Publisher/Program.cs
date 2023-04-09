using MassTransit;
using Shared.Messages;
using Shared.RequestResponseMessages;
Console.WriteLine("Publisher");

string rabbitMQUri = "amqps://oxystewb:g8ZxUdsUXLX2FY6u0PfNIRftYF7dc2HC@moose.rmq.cloudamqp.com/oxystewb";
string queueName = "example-queue";
string requestQueue = "request-queue";
////bütün işlemler bus sınıfı üzerinden yürütülecektir.
//IBusControl bus =Bus.Factory.CreateUsingRabbitMq(factory =>
//{
//    factory.Host(rabbitMQUri);
//});
////ilgili kuyruğa mesaj gönderme işlemi
//ISendEndpoint sendEndpoint=await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

//Console.Write("Gönderilecek Mesaj : ");
//string message = Console.ReadLine();
////tek bir kuyruğa mesaj yollanacaksa Send metodu kullanılır.
//await sendEndpoint.Send<IMessage>(new ExampleMessage()
//{
//    Text = message
//});
//Console.Read();
//---------------REQUEST-RESPONSE PATTERN UYGULAMASI---------------------------//
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});
//Publisher request yapacak ama aynı zamanda response'u da okuması gerekiyor. Dolayısıyla Host'u start etmek gerekiyor.
await bus.StartAsync();
//Request yapılacağı zaman bu şekilde ayar yapılır.
var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{requestQueue}"));

int i = 1;
while (true)
{
    await Task.Delay(200);
    //verilen türde bir response beklediğini bildirip request attık.
    var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i,Text=$"{i++}. request" });
    Console.WriteLine($"Response Received: {response.Message.Text}");

}
Console.Read();