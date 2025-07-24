using Amazon.S3;
using Amazon;

var builder = WebApplication.CreateBuilder(args);

// Add AWS S3 client configured for LocalStack
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = builder.Configuration.GetSection("AWS");
    return new AmazonS3Client(
        // Test credentials
        "test",
        "test",
        new AmazonS3Config
        {
            ServiceURL = config["ServiceURL"],
            ForcePathStyle = bool.Parse(config["ForcePathStyle"] ?? "true"),
            AuthenticationRegion = RegionEndpoint.APSoutheast2.SystemName
        }
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
