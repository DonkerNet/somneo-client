using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class TimeMapper
{
    public static Time ToModel(TimeDto dto)
    {
        return new Time(
            dto.DateTime,
            dto.TimezoneOffset,
            dto.CurrentDSTOffset,
            dto.DSTChangeOver);
    }
}
