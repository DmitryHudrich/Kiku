using Kiku.Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(static builder => builder.AddConsole());
serviceCollection.AddTransient<IWeatherService, WeatherService>();
serviceCollection.AddSingleton<Kiku.Telegram.Kiku>();

var serviceProvider = serviceCollection.BuildServiceProvider();
await serviceProvider.GetRequiredService<Kiku.Telegram.Kiku>().RunAsync();

