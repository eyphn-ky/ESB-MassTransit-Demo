using Consumer.Consumers;
using MassTransit;

Console.WriteLine("Consumer");

string rabbitMQUri = "amqps://oxystewb:g8ZxUdsUXLX2FY6u0PfNIRftYF7dc2HC@moose.rmq.cloudamqp.com/oxystewb";
string queueName = "example-queue";
string requestQueue = "request-queue";

////MassTransit ile ilgili temel konfigürasyonu belirlemeyi Bus sınıfı ile sağlıyuoruz. Bus MassTransit sınıfından gelir.
////CreateUsingRabbitMQ RabbitMQ üzerinden bir operasyon yürüteceğimizi belirtmek için kullanılır.
//IBusControl bus =Bus.Factory.CreateUsingRabbitMq(factory =>
//{
//    factory.Host(rabbitMQUri);

//    //consumer görevi görmesi, mesajı okuyabilmesi için gerekli configuration
//    factory.ReceiveEndpoint(queueName, endpoint =>
//    {
//        //verilen türde consumer'a ait mesaj gelirse consume et
//        //MassTransit'te consumerlar gelen mesajı IConsumer interface'ini implemente etmiş olan sınıflarda Receive etmektedirler.
//        endpoint.Consumer<ExampleMessageConsumer>();
//    });
//});
////hiçbir yerde bit çevirme yapılmadı masstransit bu sorumluluğu üstlenir.
////bus start edilmezse consumer çalışmaz
//await bus.StartAsync();
//Console.Read();

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();
Console.Read();