using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Context;
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
builder.Services.AddTransient<BotHandlers>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
