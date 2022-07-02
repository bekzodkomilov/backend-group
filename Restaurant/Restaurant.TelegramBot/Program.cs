using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.TelegramBot;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BotDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("Restaurant.TelegramBot")
    );
}, ServiceLifetime.Singleton);

builder.Services.AddHostedService<Bot>();
builder.Services.AddSingleton<TelegramBotClient>(b => new TelegramBotClient(builder.Configuration.GetConnectionString("Token")));
builder.Services.AddSingleton<BotHandlers>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBookedDishesRepository, BookedDishesRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
