using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class SunsetSettingsMapper
{
    public static SunsetSettings ToModel(SunsetSettingsDto dto)
    {
        var device = EnumMapper.GetSoundDeviceType(dto.Device);
        int? channelOrPreset = !string.IsNullOrEmpty(dto.ChannelOrPreset) ? int.Parse(dto.ChannelOrPreset) : null;
        var sunsetColors = EnumMapper.GetColorScheme(dto.SunsetColors)!.Value;

        int? fmRadioPreset = null;
        SunsetSound? sunsetSound = null;
        int? volume = null;

        switch (device)
        {
            case SoundDeviceType.FMRadio:
                fmRadioPreset = channelOrPreset;
                volume = dto.Volume;
                break;
            case SoundDeviceType.Sunset:
                sunsetSound = EnumMapper.GetSunsetSound(channelOrPreset);
                volume = dto.Volume;
                break;
        }

        return new SunsetSettings(
            dto.Enabled,
            dto.SunsetIntensity,
            dto.SunsetDuration,
            sunsetColors,
            device,
            fmRadioPreset,
            sunsetSound,
            volume);
    }
}
