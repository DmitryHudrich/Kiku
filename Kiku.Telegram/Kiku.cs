using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kiku.Telegram {
    internal class Kiku(ILogger<Kiku> logger) {
        private TelegramBotClient client = null!;

        public async Task RunAsync() {
            using var cts = new CancellationTokenSource();
            client = new TelegramBotClient("6335377827:AAEkKRfPZt-HP46ssF_ibLLBSglam5R18dI", cancellationToken: cts.Token);
            var me = await client.GetMeAsync();
            client.OnMessage += OnMessage;
            // client.OnMessage += OnEbloMessage;

            logger.LogInformation($"@{me.Username} is running... Press Enter to terminate");
            _ = Console.ReadLine();
            cts.Cancel();

        }

        private async Task OnMessage(Message msg, UpdateType type) {
            if (msg.Text is null) return;
            logger.LogInformation($"Received {type} '{msg.Text}' in {msg.Chat}");
            _ = await client.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
        }

        private async Task OnEbloMessage(Message msg, UpdateType upd) {
            var tgargs = msg.Text?.Split(' ').ToList();
            tgargs?.ForEach(static el => el?.Trim());
            if (msg.Text != null) {
                if (tgargs?[0] == "eblo") {

                    using var httpClient = new HttpClient() {

                    };

                    if (msg.Text != null) {
                        var weather = await httpClient.GetFromJsonAsync<WeatherRequest>($"https://wttr.in/{tgargs[1]}?format=j1");

                        if (weather != null) {
                            _ = await client.SendTextMessageAsync(msg.Chat, weather.current_condition[0].temp_C + " " + weather.nearest_area[0].country[0].value);
                            return;
                        }
                    }
                }
            }
        }
    }
}
