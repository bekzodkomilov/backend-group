using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BotDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("Restaurant.TelegramBot")
    );
}, ServiceLifetime.Singleton);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
