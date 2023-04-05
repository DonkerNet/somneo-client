using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class LightStateMapper
{
    public static LightState ToModel(LightStateDto dto)
    {
        bool lightEnabled = dto.Enabled && !dto.SunriseOrSunsetEnabled;

        return new LightState(
            lightEnabled,
            dto.LightLevel,
            dto.NightLightEnabled,
            dto.SunriseOrSunsetEnabled);
    }
}
