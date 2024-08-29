using System.Net.Http.Json;
using Kiku.Domain.Weather;
using Microsoft.Extensions.Logging;

namespace Kiku.Logic;

public interface IWeatherService {
    Task<Weather?> GetCurrentWeatherAsync(String city);
}

public class WeatherService(ILogger<WeatherService> logger) : IWeatherService {
    public async Task<Weather?> GetCurrentWeatherAsync(String city) {
        logger.LogInformation("Weather request.");
        using var httpClient = new HttpClient();
        var weatherResponse = await httpClient.GetAsync($"https://wttr.in/{city}?format=j1");
        var res = weatherResponse.StatusCode != System.Net.HttpStatusCode.NotFound ? await ParseContentAsync(weatherResponse.Content) : null;
        return res;
    }

    private static async Task<Weather> ParseContentAsync(HttpContent content) {
        var weather = await content.ReadFromJsonAsync<Request>();
        var res = new Weather(
            Place: new Place(
                Country: weather!.nearest_area[0].country[0].value,
                City: weather.nearest_area[0].areaName[0].value),
            Tempertature: new Tempertature(
                FeelsLikeC: Int32.Parse(weather.current_condition[0].FeelsLikeC),
                Cloudcover: Int32.Parse(weather.current_condition[0].cloudcover),
                Humidity: Int32.Parse(weather.current_condition[0].humidity),
                CurrentTemperature: Int32.Parse(weather.current_condition[0].temp_C))
        );
        return res;
    }

    #region WeatherRequest

#pragma warning disable IDE1006
#pragma warning disable CA1707

    internal record class Request(
        Request.Current_condition[] current_condition,
        Request.Nearest_area[] nearest_area,
        Request.Data[] request,
        Request.Weather[] weather
    ) {
        internal record Current_condition(
            String FeelsLikeC,
            String FeelsLikeF,
            String cloudcover,
            String humidity,
            String localObsDateTime,
            String observation_time,
            String precipInches,
            String precipMM,
            String pressure,
            String pressureInches,
            String temp_C,
            String temp_F,
            String uvIndex,
            String visibility,
            String visibilityMiles,
            String weatherCode,
            WeatherDesc[] weatherDesc,
            WeatherIconUrl[] weatherIconUrl,
            String winddir16Point,
            String winddirDegree,
            String windspeedKmph,
            String windspeedMiles
        );

        internal record WeatherDesc(
            String value
        );

        internal record WeatherIconUrl(
            String value
        );

        internal record Nearest_area(
            AreaName[] areaName,
            Country[] country,
            String latitude,
            String longitude,
            String population,
            Region[] region,
            WeatherUrl[] weatherUrl
        );

        internal record AreaName(
            String value
        );

        internal record Country(
            String value
        );

        internal record Region(
            String value
        );

        internal record WeatherUrl(
            String value
        );

        internal record Data(
            String query,
            String type
        );

        internal record Weather(
            Astronomy[] astronomy,
            String avgtempC,
            String avgtempF,
            String date,
            Hourly[] hourly,
            String maxtempC,
            String maxtempF,
            String mintempC,
            String mintempF,
            String sunHour,
            String totalSnow_cm,
            String uvIndex
        );

        internal record Astronomy(
            String moon_illumination,
            String moon_phase,
            String moonrise,
            String moonset,
            String sunrise,
            String sunset
        );

        internal record Hourly(
            String DewPointC,
            String DewPointF,
            String FeelsLikeC,
            String FeelsLikeF,
            String HeatIndexC,
            String HeatIndexF,
            String WindChillC,
            String WindChillF,
            String WindGustKmph,
            String WindGustMiles,
            String chanceoffog,
            String chanceoffrost,
            String chanceofhightemp,
            String chanceofovercast,
            String chanceofrain,
            String chanceofremdry,
            String chanceofsnow,
            String chanceofsunshine,
            String chanceofthunder,
            String chanceofwindy,
            String cloudcover,
            String diffRad,
            String humidity,
            String precipInches,
            String precipMM,
            String pressure,
            String pressureInches,
            String shortRad,
            String tempC,
            String tempF,
            String time,
            String uvIndex,
            String visibility,
            String visibilityMiles,
            String weatherCode,
            WeatherDesc1[] weatherDesc,
            WeatherIconUrl1[] weatherIconUrl,
            String winddir16Point,
            String winddirDegree,
            String windspeedKmph,
            String windspeedMiles
        );

        internal record WeatherDesc1(
            String value
        );

        internal record WeatherIconUrl1(
            String value
        );

        #endregion WeatherRequest
    }
}
