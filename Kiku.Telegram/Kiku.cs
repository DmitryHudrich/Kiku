using Kiku.Logic;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kiku.Telegram;

internal class Kiku(ILogger<Kiku> logger, IWeatherService weatherService) {
    private TelegramBotClient client = null!;

    public async Task RunAsync() {
        using var cts = new CancellationTokenSource();
        var token = Environment.GetEnvironmentVariable("token");
        client = new TelegramBotClient(token, cancellationToken: cts.Token);
        var me = await client.GetMeAsync();
        client.OnMessage += PrepairDispatch;

        logger.LogInformation($"@{me.Username} is running... Press Enter to terminate");
        _ = Console.ReadLine();
        cts.Cancel();
    }

    private async Task PrepairDispatch(Message msg, UpdateType upd) {
        if (msg.Text is null) {
            logger.LogInformation("msg is null");
            return;
        }
        var tgargs = msg.Text?.Split(' ').ToList();
        tgargs?.ForEach(static el => el?.Trim());

        await Dispatch(msg, tgargs);
    }

    private async Task Dispatch(Message message, List<String>? userMessageArgs) {
        switch (userMessageArgs?[0]) {
            case "weather":
                await SendWeather(message.Chat, userMessageArgs[1]);
                break;
            default:
                break;
        }
    }

    private async Task SendWeather(Chat chat, String city) {
        var weather = await weatherService.GetCurrentWeatherAsync(city);
        string weatherMessage;
        if (weather == null)
            weatherMessage = "Wrong city";
        else
            weatherMessage = $"""
                    City: {weather.Place.City}
                    Country: {weather.Place.Country}
                    Temperature: {weather.Tempertature.CurrentTemperature}
                    """;
        _ = await client.SendTextMessageAsync(chat, weatherMessage);
    }
}
