var builder = WebApplication.CreateBuilder(args);

#region DI
var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();
ApplicationDependencyInjection.AddApplication(services);

#endregion

#region Build app
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
#endregion;
