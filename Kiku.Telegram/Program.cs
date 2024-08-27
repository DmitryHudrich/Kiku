using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("6335377827:AAFuEgYVSMn57Ct7BYFXeg3w2JnAcm9ClGM", cancellationToken: cts.Token);
var me = await bot.GetMeAsync();
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel();

async Task OnMessage(Message msg, UpdateType type) {
    if (msg.Text is null) return;   // we only handle Text messages here
    Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
    await bot.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
}

