using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kiku.Telegram;

internal class Kiku(ILogger<Kiku> logger) {
    private TelegramBotClient client = null!;

    public async Task RunAsync() {
        using var cts = new CancellationTokenSource();
        var token = Environment.GetCommandLineArgs()[1];
        client = new TelegramBotClient(token, cancellationToken: cts.Token);
        var me = await client.GetMeAsync();
        client.OnMessage += OnEbloMessage;

        logger.LogInformation($"@{me.Username} is running... Press Enter to terminate");
        _ = Console.ReadLine();
        cts.Cancel();
    }

    private async Task OnEbloMessage(Message msg, UpdateType upd) {
        if (msg.Text is null) {
            logger.LogInformation("berba");
            return;
        }
        var tgargs = msg.Text?.Split(' ').ToList();
        tgargs?.ForEach(static el => el?.Trim());
        if (tgargs?[0] == "weather") {
            using var httpClient = new HttpClient();
            var weather = await httpClient.GetFromJsonAsync<WeatherRequest>($"https://wttr.in/{tgargs[1]}?format=j1");
            var weatherMessage =
                weather == null
                    ? "Wrong city"
                    : $"""
                            City: {weather.nearest_area[0].region[0].value}
                            Country: {weather.nearest_area[0].country[0].value}
                            Day: {weather.weather[0].maxtempC} Night: {weather.weather[0].mintempC}
                            """;
            logger.LogInformation("weather");

            _ = await client.SendTextMessageAsync(msg.Chat, weatherMessage);
            return;
        }
    }
}
