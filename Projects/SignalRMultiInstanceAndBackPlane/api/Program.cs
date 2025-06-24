using System.Security.Claims;
using Api.ChatHub;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

#region Vars

string clientUrl = "http://localhost:3000";
string redisUrl = "localhost:6379";

#endregion;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Now signalR is multi-tenanted.
services.AddSignalR().AddStackExchangeRedis(redisUrl);
services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisUrl));
services.AddSingleton<IUserConnectionStore, UserConnectionStore>();
services.AddSingleton<IChatDispatcher, ChatDispatcher>();

services.AddCors(options =>
{
    options.AddPolicy("Dev", policy =>
    {
        policy.WithOrigins(clientUrl)
              .AllowAnyHeader()
              .AllowAnyMethod()
              // REQUIRED FOR SIGNALR
              .AllowCredentials();
    });
});

var key = new SymmetricSecurityKey(Convert.FromBase64String("2wEVL9RGsnxqOt/hx3ZfZJGZ92Z04GEQQk+4Hg9UZfs="));
services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key
        };

        // ⚠ Required to enable SignalR auth via access_token query string
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chathub"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

app.UseCors("Dev");
app.MapGet("/", () => "Hello World!");

var users = new Dictionary<string, string>
{
    { "ale", "Password1" },
    { "leo", "Password1" }
};

/**
 * Dummy login endpoint!
 */
app.MapPost("/login", (HttpContext http, string username, string password) =>
{
    if (!users.TryGetValue(username, out var correctPassword) || correctPassword != password)
        return Results.Unauthorized();

    var claims = new[] { new Claim(ClaimTypes.Name, username) };
    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    );

    var tokenString = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
    return Results.Ok(new { token = tokenString });
});
app.MapHub<ChatHub>("/chathub");

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception e)
    {
        // Log the exception (you could use ILogger for richer logging)
        Console.WriteLine($"Unhandled exception: {e.Message}");
        Console.WriteLine(e.StackTrace);

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred.");
    }
});

// Set up Redis multiplexing subscription.
var redis = app.Services.GetRequiredService<IConnectionMultiplexer>();
var sub = redis.GetSubscriber();
// Get our hub context.
var hubContext = app.Services.GetRequiredService<IHubContext<ChatHub>>();

// Subscribe to the notify.users on the stack exchange and pass the message on via this hub.
sub.Subscribe("notify.users", async (channel, value) =>
{
    Console.WriteLine($"New message: {value}");
    try
    {
        var payload = JsonSerializer.Deserialize<RedisMessage>(value!);
        if (string.IsNullOrEmpty(payload?.Recipient))
        {
            return;
        }

        Console.WriteLine($"Passing message on: {payload.Message}");
        var chatDispatcher = app.Services.GetRequiredService<IChatDispatcher>();
        await chatDispatcher.SendAsync(
            payload.Recipient,
            payload.Sender,
            payload.Message
        );
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Redis Subscribe Error]: {ex.Message}");
    }
});

app.Run();
