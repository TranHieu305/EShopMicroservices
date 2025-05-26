using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

services.AddValidatorsFromAssembly(typeof(Program).Assembly);
services.AddCarter();

services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();

// Validate: Register the handler
services.AddExceptionHandler<CustomExceptionHandler>();

if (builder.Environment.IsDevelopment())
{
    services.InitializeMartenWith<CatalogInitialData>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

// Validate: Enable exception handling middleware (middleware pipeline)
app.UseExceptionHandler(options => { });

app.Run();
