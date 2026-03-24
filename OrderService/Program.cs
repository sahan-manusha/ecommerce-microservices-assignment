using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Service API",
        Version = "v1",
        Description = "Microservice for managing orders in the E-Commerce platform. Handles order processing including creation, tracking, updates, and cancellation of customer orders.",
        Contact = new OpenApiContact { Name = "Member 2" }
    });
});

// Configure port
builder.WebHost.UseUrls("http://localhost:5002");

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("Order Service is running on http://localhost:5002");
Console.WriteLine("Swagger UI: http://localhost:5002/swagger");

app.Run();
