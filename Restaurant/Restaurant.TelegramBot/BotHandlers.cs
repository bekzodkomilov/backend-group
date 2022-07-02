using System.Text.RegularExpressions;
using Restaurant.Data.Repositories.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Restaurant.TelegramBot;
public class BotHandlers
{
    private readonly ILogger<BotHandlers> _logger;
    private readonly IUsersRepository _userRepo;

    public BotHandlers(ILogger<BotHandlers> logger, IUsersRepository userRepo)
    {
        _userRepo = userRepo;
        _logger = logger;
    }
    public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken ctoken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException => $"Error occured with Telegram Client: {exception.Message}",
            _ => exception.Message
        };
        _logger.LogCritical(errorMessage);
        return Task.CompletedTask;
    }
    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken ctoken)
    {
        var handler = update.Type switch
        {
            UpdateType.Message => BotOnMessageRecieved(client, update.Message),
            UpdateType.CallbackQuery => BotOnCallbackQueryRecieved(client, update.CallbackQuery),
            _ => UnknownUpdateHandler(client, update)
        };
        try
        {
            await handler;
        }
        catch(Exception e)
        {
            _logger.LogWarning(e.Message);
        }
    }

    private async Task UnknownUpdateHandler(ITelegramBotClient client, Update update)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnCallbackQueryRecieved(ITelegramBotClient client, CallbackQuery? callbackQuery)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnMessageRecieved(ITelegramBotClient client, Message message)
    {
        var user = await _userRepo.GetByIdAsync(message.Chat.Id);
        if(message?.Text == "/start" && !(await _userRepo.ExistsAsync(message.Chat.Id)))
        {
            user = new Domain.Entities.BotEntities.User();
            user.ChatId = message.Chat.Id;
            user.Fullname = $"{message.From?.FirstName} {message.From?.LastName}";
            user.Username = message.From?.Username;
            user.Process = Domain.Enums.EProcess.EnteringFullName;
            await _userRepo.InsertAsync(user);

            await client.SendTextMessageAsync(
                user.ChatId,
                "Salom ismingizni kiriting."
            );
        }
        else
        {
            if(user.Process == Domain.Enums.EProcess.EnteringFullName)
            {
                user.Fullname = message.Text;
                user.Process = Domain.Enums.EProcess.SendingContact;
                await _userRepo.UpdateAsync(user);

                await client.SendTextMessageAsync(
                    user.ChatId,
                    $"Iltimos telefon raqamingizni yuboring",
                    replyMarkup: null
                );
            }
            else if(user.Process == Domain.Enums.EProcess.SendingContact)
            {
                if(message.Contact == null)
                {
                    if(Regex.Match(message.Text, @"(?:[+][9]{2}[8][0-9]{2}[0-9]{3}[0-9]{2}[0-9]{2})").Success)
                    {
                        user.PhoneNumber = message.Text;
                    }
                    else
                    {
                        await client.SendTextMessageAsync(
                            user.ChatId,
                            "Iltimos to'gri formatdagi telefon raqam jo'nating. Masalan: +998917777777"
                        );
                        _logger.LogInformation($"PhoneNumber doesn't match: {user.ChatId} {user.Username}");
                        return;
                    }
                }
                else user.PhoneNumber = message.Contact.PhoneNumber;
                user.Process = Domain.Enums.EProcess.None;

                await _userRepo.UpdateAsync(user);

                await client.SendTextMessageAsync(
                    user.ChatId,
                    "Siz endi botdan foydalanishingiz mumkin"
                );
            }
        }
    }
}