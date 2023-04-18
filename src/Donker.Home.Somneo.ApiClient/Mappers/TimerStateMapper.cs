using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class TimerStateMapper
{
    public static TimerState ToModel(TimerStateDto dto)
    {
        var relaxBreatheTime = TimeSpan.FromSeconds((dto.RelaxBreatheMinutes * 60) + dto.RelaxBreatheSeconds);
        bool relaxBreatheEnabled = relaxBreatheTime > TimeSpan.Zero;
        var sunsetTime = TimeSpan.FromSeconds((dto.SunsetMinutes * 60) + dto.SunsetSeconds);
        bool sunsetTimeEnabled = sunsetTime > TimeSpan.Zero;
        bool enabled = relaxBreatheEnabled || sunsetTimeEnabled;

        return new TimerState(
            dto.StartTime,
            relaxBreatheEnabled,
            sunsetTimeEnabled,
            enabled,
            relaxBreatheEnabled ? relaxBreatheTime : null,
            sunsetTimeEnabled ? sunsetTime : null);
    }
}
