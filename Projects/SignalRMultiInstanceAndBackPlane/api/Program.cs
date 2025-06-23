using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddSignalR();

services.AddCors(options =>
{
    options.AddPolicy("Dev", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
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

app.Run();
