var builder = WebApplication.CreateBuilder(args);

// Add controllers 
builder.Services.AddControllers();

// Add Swagger for browser-based API testing 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow MAUI app (and any local client) to call this API 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable Swagger UI in development 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.MapControllers();
app.Run();