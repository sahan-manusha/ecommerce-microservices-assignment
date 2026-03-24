using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Service API",
        Version = "v1",
        Description = "Microservice for managing products in the E-Commerce platform. Handles product catalog operations including creation, retrieval, update, and deletion of products.",
        Contact = new OpenApiContact { Name = "Member 1" }
    });
});

// Configure port
builder.WebHost.UseUrls("http://localhost:5001");

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("Product Service is running on http://localhost:5001");
Console.WriteLine("Swagger UI: http://localhost:5001/swagger");

app.Run();
