# SignalR + Redis Pub/Sub + Backplane Integration

This guide explains how to set up a SignalR application using Redis in two ways:

1. As a backplane — to scale SignalR hubs across multiple server instances.
2. As a pub/sub message bus — so external services can send messages to users.

---

## 🧠 Architecture Overview

- Redis handles both:
  - SignalR backplane (automatic hub connection sync across instances)
  - Pub/Sub channel for worker-to-client messaging
- This enables scalable, real-time apps with decoupled backend logic.

```
External Worker ─┬─> Redis (Pub/Sub) ─┐
                 └──────────────────> SignalR Server(s) ─> Clients
```

---

## 🧰 Requirements

- ASP.NET Core with SignalR
- Redis (local or hosted)
- NuGet packages:
  - Microsoft.AspNetCore.SignalR.StackExchangeRedis
  - StackExchange.Redis

---

## 🔧 1. Install Redis

Run locally via Docker:

```bash
docker run -p 6379:6379 redis
```

---

## 📦 2. Install Required NuGet Packages

```bash
dotnet add package Microsoft.AspNetCore.SignalR.StackExchangeRedis
dotnet add package StackExchange.Redis
```

---

## 🛠 3. Configure SignalR Server with Redis Backplane

Update your Program.cs:

```csharp
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add Redis backplane and pub/sub
builder.Services.AddSignalR().AddStackExchangeRedis("localhost:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

var app = builder.Build();
app.UseCors("Dev");

app.MapHub<ChatHub>("/chathub");
app.Run();
```

---

## 🔁 4. Subscribe to Redis Pub/Sub Channel

Below app.Run(), or in a hosted service:

```csharp
var redis = app.Services.GetRequiredService<IConnectionMultiplexer>();
var sub = redis.GetSubscriber();
var hubContext = app.Services.GetRequiredService<IHubContext<ChatHub>>();

sub.Subscribe("notify.users", async (channel, value) =>
{
    try
    {
        var payload = JsonSerializer.Deserialize<RedisMessage>(value!);

        if (!string.IsNullOrEmpty(payload?.User))
            await hubContext.Clients.User(payload.User).SendAsync("ReceiveMessage", "System", payload.Message);
        else
            await hubContext.Clients.All.SendAsync("ReceiveMessage", "System", payload?.Message ?? "Unknown");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Redis Subscribe Error]: {ex.Message}");
    }
});

public record RedisMessage(string User, string Message);
```

---

## 💬 5. Define Your SignalR Hub

```csharp
public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        var user = Context.User?.Identity?.Name ?? "Anonymous";
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
```

---

## 🧑‍💻 6. External Worker Publishes to Redis

This can be any language or service. Here's a .NET console example:

```csharp
using StackExchange.Redis;
using System.Text.Json;

var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
var pub = redis.GetSubscriber();

var message = new { user = "Ale", message = "Your report is ready!" };
var json = JsonSerializer.Serialize(message);

await pub.PublishAsync("notify.users", json);
Console.WriteLine("Published.");
```

---

## ✅ Benefits

- Clients connected to any instance receive messages reliably.
- External systems can trigger SignalR messages without a SignalR connection.
- Scalable architecture: just run more instances.

---

## 🔐 Security Note

For production:
- Secure Redis with auth/password.
- Validate messages before broadcasting.
- Consider rate-limiting and auditing.

---

Let me know if you’d like a working solution or Docker Compose setup to go with this guide!
