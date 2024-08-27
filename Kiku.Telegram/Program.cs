using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(static builder => builder.AddConsole());
serviceCollection.AddSingleton<Kiku>();

var serviceProvider = serviceCollection.BuildServiceProvider();
await serviceProvider.GetRequiredService<Kiku>().RunAsync();

internal class Kiku(ILogger<Kiku> logger) {
    public async Task RunAsync() {
        using var cts = new CancellationTokenSource();
        var bot = new TelegramBotClient("6335377827:AAFuEgYVSMn57Ct7BYFXeg3w2JnAcm9ClGM", cancellationToken: cts.Token);
        var me = await bot.GetMeAsync();
        bot.OnMessage += OnMessage;

        logger.LogInformation($"@{me.Username} is running... Press Enter to terminate");
        _ = Console.ReadLine();
        cts.Cancel();

        async Task OnMessage(Message msg, UpdateType type) {
            if (msg.Text is null) return;
            logger.LogInformation($"Received {type} '{msg.Text}' in {msg.Chat}");
            _ = await bot.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
        }
    }
}

