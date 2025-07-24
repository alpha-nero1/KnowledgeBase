using Amazon.SQS;
using common.options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
services.Configure<AwsOptions>(config.GetSection("AWS"));
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    // Fudge the IAmazonSQS credentials with LocalStack!
    services.AddSingleton<IAmazonSQS>(_ =>
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
