using Microsoft.EntityFrameworkCore;
using TaxiDrivers.Data;
using TaxiDrivers.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlite("Data Source=Data/app.db");
    }, ServiceLifetime.Singleton
);
builder.Services.AddSingleton<CarService>();
builder.Services.AddSingleton<DriverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
