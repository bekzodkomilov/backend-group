using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<DishService>();
builder.Services.AddScoped<IDishesRepository, DishesRepository>();
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("Restaurant.Api")
    );
}, ServiceLifetime.Singleton);

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
