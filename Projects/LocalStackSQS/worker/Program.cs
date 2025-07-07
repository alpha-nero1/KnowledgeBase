using Amazon.SQS;
using common.options;
using worker;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddHostedService<MessageWorker>();
services.Configure<AwsOptions>(config.GetSection("AWS"));

if (builder.Environment.IsDevelopment())
{
    // Fudge the IAmazonSQS credentials with LocalStack!
    services.AddSingleton<IAmazonSQS>(_ =>
    {
        var cfg = new AmazonSQSConfig
        {
            ServiceURL = "http://localhost:4566",
            AuthenticationRegion = "ap-southeast-2",   // any value
            UseHttp = true
        };
        return new AmazonSQSClient("test", "test", cfg); // dummy creds
    });
}

var host = builder.Build();
host.Run();
