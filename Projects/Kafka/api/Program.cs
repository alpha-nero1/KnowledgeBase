using api.options;
using api.services;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// bind config section ➜ POCO
builder.Services.Configure<KafkaOptions>(
    builder.Configuration.GetSection("Kafka"));

// create a singleton IProducer once for the whole app
// This is the money right here!
builder.Services.AddSingleton<IProducer<Null,string>>(sp =>
{
    var opts = sp.GetRequiredService<IOptions<KafkaOptions>>().Value;
    var cfg  = new ProducerConfig { BootstrapServers = opts.BootstrapServers };
    return new ProducerBuilder<Null,string>(cfg).Build();
});

// Add our custom publishing service.
builder.Services.AddScoped<IPublisherService, PublisherService>();

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();

app.Run();
