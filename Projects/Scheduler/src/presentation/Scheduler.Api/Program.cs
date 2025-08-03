using Hangfire;
using Scheduler.Application;
using Scheduler.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddApplication();
services.AddInfrastructure(configuration, true);
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard("/hangfire");
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();
