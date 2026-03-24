using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Customer Service API",
        Version = "v1",
        Description = "Microservice for managing customers in the E-Commerce platform. Handles customer registration, profile management, and contact information.",
        Contact = new OpenApiContact { Name = "Member 3" }
    });
});

// Configure port
builder.WebHost.UseUrls("http://localhost:5003");

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Service API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("Customer Service is running on http://localhost:5003");
Console.WriteLine("Swagger UI: http://localhost:5003/swagger");

app.Run();
