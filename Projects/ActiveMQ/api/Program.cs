using System.Text.Json;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using common;

#region Vars

string activeMqUrl = "activemq:tcp://localhost:61616";

#endregion;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    var factory = new ConnectionFactory(activeMqUrl);
    using var connection = factory.CreateConnection();
    connection.Start();

    using var session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);

    IDestination destination = session.GetTopic("demo.topic");

    using var producer = session.CreateProducer(destination);
    producer.DeliveryMode = MsgDeliveryMode.Persistent;

    var message = new DataMessage
    {
        OrderId = new Random().Next(10000, 100000),
        Data = "{}"
    };

    Console.WriteLine($"Sending message {JsonSerializer.Serialize(message)}");

    var msg = session.CreateTextMessage(JsonSerializer.Serialize(message));
    producer.Send(msg);
});

app.Run();
