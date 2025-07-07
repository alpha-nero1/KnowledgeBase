using Amazon.SQS;
using Amazon;
using common.options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
services.Configure<AwsOptions>(config.GetSection("AWS"));

if (builder.Environment.IsDevelopment())
{
    // Fudge the IAmazonSQS credentials with LocalStack!
    builder.Services.AddSingleton<IAmazonSQS>(_ =>
    {
        var cfg = new AmazonSQSConfig
        {
            ServiceURL           = "http://localhost:4566",
            AuthenticationRegion = "ap-southeast-2",   // any value
            UseHttp              = true
        };
        return new AmazonSQSClient("test", "test", cfg); // dummy creds
    });
}

var app = builder.Build();

app.MapControllers();

app.Run();
