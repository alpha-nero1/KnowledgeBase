using worker;
using worker.options;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
services.Configure<KafkaOptions>(config.GetSection("Kafka"));
services.AddHostedService<KafkaWorker>();

var host = builder.Build();
host.Run();
