using common;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Config MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });

});


var app = builder.Build();

app.MapGet("/", async () =>
{
    var messageBus = app.Services.GetRequiredService<IBus>();
    var message = new OrderUpdateEvent(
        Guid.NewGuid(),
        new Random().Next(100000, 1000000),
        OrderStatus.Complete,
        DateTime.UtcNow
    );
    await messageBus.Publish(message);
    Console.WriteLine($"[api] sent {message}");
});

app.Run();
