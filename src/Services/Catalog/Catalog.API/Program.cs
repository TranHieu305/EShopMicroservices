var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;


var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
