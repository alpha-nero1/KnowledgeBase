using worker;
using StackExchange.Redis;

#region Vars

string redisUrl = "localhost:6379";

#endregion;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
services.AddHostedService<Worker>();

// This is the money right here!
services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisUrl));

var host = builder.Build();
host.Run();
