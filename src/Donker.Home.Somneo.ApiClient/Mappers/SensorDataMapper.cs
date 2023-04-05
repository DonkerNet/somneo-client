using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class SensorDataMapper
{
    public static SensorData ToModel(SensorDataDto dto)
    {
        return new SensorData(
            dto.CurrentTemperature,
            dto.AverageTemperature,
            dto.CurrentLight,
            dto.AverageLight,
            dto.CurrentSound,
            dto.AverageSound,
            dto.CurrentHumidity,
            dto.AverageHumidity);
    }
}
