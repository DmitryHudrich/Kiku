using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kiku.Telegram {
    internal class Kiku(ILogger<Kiku> logger) {
        public async Task RunAsync() {
            using var cts = new CancellationTokenSource();
            var bot = new TelegramBotClient("6335377827:AAFuEgYVSMn57Ct7BYFXeg3w2JnAcm9ClGM", cancellationToken: cts.Token);
            var me = await bot.GetMeAsync();
            bot.OnMessage += OnMessage;
            bot.OnMessage += OnEbloMessage;

            logger.LogInformation($"@{me.Username} is running... Press Enter to terminate");
            _ = Console.ReadLine();
            cts.Cancel();

            async Task OnMessage(Message msg, UpdateType type) {
                if (msg.Text is null) return;
                logger.LogInformation($"Received {type} '{msg.Text}' in {msg.Chat}");
                _ = await bot.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
            }

            async Task OnEbloMessage(Message msg, UpdateType upd) {
                if (msg.Text != null) {
                    if (msg.Text.ToLower().Contains("eblo")) {

                        using var client = new HttpClient() {

                        };
                        await bot.SendTextMessageAsync(msg.Chat, $"Укажите город");

                        if (msg.Text != null) {
                            var weather = await client.GetFromJsonAsync<WeatherRequest>($"https://wttr.in/{msg.Text}?format=j1");

                            if (weather != null) {
                                await bot.SendTextMessageAsync(msg.Chat, (weather.current_condition[0].temp_C + " " + weather.nearest_area[0].country[0].value));
                                return;
                            }
                        }

                    }

                }


            }
        }
    }
}
