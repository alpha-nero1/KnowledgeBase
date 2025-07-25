using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers();
services.AddDbContext<Api.Controllers.OrdersDbContext>(options => options.UseInMemoryDatabase("OrdersDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// This is the magic, we need to expose the Program class as partial
// so that the integration tests can access it! ~ Ale :P
public partial class Program { }
