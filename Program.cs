using Microsoft.EntityFrameworkCore;
using BookLibrarySystem.Data;
using BookLibrarySystem.Repositories;
using Microsoft.OpenApi.Models;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryContext>(options =>
options.UseInMemoryDatabase("BookLibrarySystemDB"));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddControllers().AddNewtonsoftJson();

// Add logging services
builder.Logging.AddConsole();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Library System API", Version = "v1" });
});

var app = builder.Build();

// Configure the middleware pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Library System API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();