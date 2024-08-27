namespace Kiku.Telegram {

    public record WeatherRequest(
        Current_condition[] current_condition,
        Nearest_area[] nearest_area,
        Request[] request,
        Weather[] weather
    );

    public record Current_condition(
        string FeelsLikeC,
        string FeelsLikeF,
        string cloudcover,
        string humidity,
        string localObsDateTime,
        string observation_time,
        string precipInches,
        string precipMM,
        string pressure,
        string pressureInches,
        string temp_C,
        string temp_F,
        string uvIndex,
        string visibility,
        string visibilityMiles,
        string weatherCode,
        WeatherDesc[] weatherDesc,
        WeatherIconUrl[] weatherIconUrl,
        string winddir16Point,
        string winddirDegree,
        string windspeedKmph,
        string windspeedMiles
    );

    public record WeatherDesc(
        string value
    );

    public record WeatherIconUrl(
        string value
    );

    public record Nearest_area(
        AreaName[] areaName,
        Country[] country,
        string latitude,
        string longitude,
        string population,
        Region[] region,
        WeatherUrl[] weatherUrl
    );

    public record AreaName(
        string value
    );

    public record Country(
        string value
    );

    public record Region(
        string value
    );

    public record WeatherUrl(
        string value
    );

    public record Request(
        string query,
        string type
    );

    public record Weather(
        Astronomy[] astronomy,
        string avgtempC,
        string avgtempF,
        string date,
        Hourly[] hourly,
        string maxtempC,
        string maxtempF,
        string mintempC,
        string mintempF,
        string sunHour,
        string totalSnow_cm,
        string uvIndex
    );

    public record Astronomy(
        string moon_illumination,
        string moon_phase,
        string moonrise,
        string moonset,
        string sunrise,
        string sunset
    );

    public record Hourly(
        string DewPointC,
        string DewPointF,
        string FeelsLikeC,
        string FeelsLikeF,
        string HeatIndexC,
        string HeatIndexF,
        string WindChillC,
        string WindChillF,
        string WindGustKmph,
        string WindGustMiles,
        string chanceoffog,
        string chanceoffrost,
        string chanceofhightemp,
        string chanceofovercast,
        string chanceofrain,
        string chanceofremdry,
        string chanceofsnow,
        string chanceofsunshine,
        string chanceofthunder,
        string chanceofwindy,
        string cloudcover,
        string diffRad,
        string humidity,
        string precipInches,
        string precipMM,
        string pressure,
        string pressureInches,
        string shortRad,
        string tempC,
        string tempF,
        string time,
        string uvIndex,
        string visibility,
        string visibilityMiles,
        string weatherCode,
        WeatherDesc1[] weatherDesc,
        WeatherIconUrl1[] weatherIconUrl,
        string winddir16Point,
        string winddirDegree,
        string windspeedKmph,
        string windspeedMiles
    );

    public record WeatherDesc1(
        string value
    );

    public record WeatherIconUrl1(
        string value
    );
}
