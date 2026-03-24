using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inventory Service API",
        Version = "v1",
        Description = "Microservice for managing inventory in the E-Commerce platform. Handles stock tracking, warehouse management, and inventory level monitoring.",
        Contact = new OpenApiContact { Name = "Member 4" }
    });
});

// Configure port
builder.WebHost.UseUrls("http://localhost:5004");

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Service API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("Inventory Service is running on http://localhost:5004");
Console.WriteLine("Swagger UI: http://localhost:5004/swagger");

app.Run();
