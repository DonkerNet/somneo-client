using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class RelaxBreatheSettingsMapper
{
    public static RelaxBreatheSettings ToModel(RelaxBreatheSettingsDto dto)
    {
        int breathsPerMinute = dto.AvailableBreathsPerMinute[dto.Program - 1];

        bool isLight = dto.Type == 0;
        bool isSound = dto.Type == 1;

        int? lightIntensity = isLight ? dto.Intensity : null;
        int? soundVolume = isSound ? dto.Volume : null;

        return new RelaxBreatheSettings(
            dto.Enabled,
            dto.Duration,
            lightIntensity,
            soundVolume,
            breathsPerMinute,
            isLight,
            isSound,
            dto.AvailableBreathsPerMinute);
    }
}
