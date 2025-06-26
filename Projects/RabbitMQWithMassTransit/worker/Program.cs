using common;
using MassTransit;
using worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    // This is the money babay!
    x.AddConsumer<OrderUpdateEventConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // configures this worker as a receiver and to set up receiving endpoints.
        cfg.ConfigureEndpoints(ctx);

        // Listen on DLQ...
        cfg.ReceiveEndpoint("OrderUpdateEvent_error", e =>
        {
            e.Handler<Fault<OrderUpdateEvent>>(ctx =>
            {
                var originalMessage = ctx.Message.Message;
                var exception = ctx.Message.Exceptions.FirstOrDefault()?.Message;

                Console.WriteLine($"[DLQ] Alert sent for failed message: {originalMessage}");
                return Task.CompletedTask;
            });
        });
    });
});

var host = builder.Build();
host.Run();
