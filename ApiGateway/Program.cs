using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "E-Commerce API Gateway",
        Version = "v1",
        Description = "Central API Gateway for the E-Commerce Microservices Platform. Routes requests to Product Service (5001), Order Service (5002), Customer Service (5003), and Inventory Service (5004)."
    });
});

// Add Ocelot
builder.Services.AddOcelot(builder.Configuration);

// Configure port
builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/product-service/swagger/v1/swagger.json", "Product Service API v1");
    c.SwaggerEndpoint("/order-service/swagger/v1/swagger.json", "Order Service API v1");
    c.SwaggerEndpoint("/customer-service/swagger/v1/swagger.json", "Customer Service API v1");
    c.SwaggerEndpoint("/inventory-service/swagger/v1/swagger.json", "Inventory Service API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

// Use Ocelot middleware
await app.UseOcelot();

Console.WriteLine("===========================================");
Console.WriteLine("  E-Commerce API Gateway is running!");
Console.WriteLine("  Gateway:  http://localhost:5000");
Console.WriteLine("  Swagger:  http://localhost:5000/swagger");
Console.WriteLine("===========================================");
Console.WriteLine("  Downstream Microservices:");
Console.WriteLine("  - Product Service:   http://localhost:5001");
Console.WriteLine("  - Order Service:     http://localhost:5002");
Console.WriteLine("  - Customer Service:  http://localhost:5003");
Console.WriteLine("  - Inventory Service: http://localhost:5004");
Console.WriteLine("===========================================");

app.Run();
