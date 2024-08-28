namespace Kiku.Domain.Weather;

public record Weather(Place Place, Tempertature Tempertature);
public record Place(String Country, String City);
public record Tempertature(Int32 FeelsLikeC, Int32 Cloudcover, Int32 Humidity, Int32 CurrentTemperature);
