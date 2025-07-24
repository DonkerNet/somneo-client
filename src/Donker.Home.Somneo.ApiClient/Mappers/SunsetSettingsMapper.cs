using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class SunsetSettingsMapper
{
    public static SunsetSettings ToModel(SunsetSettingsDto dto)
    {
        var soundDevice = EnumMapper.GetSoundDeviceType(dto.SoundDevice);
        int? channelOrPreset = !string.IsNullOrEmpty(dto.ChannelOrPreset) ? int.Parse(dto.ChannelOrPreset) : null;
        var colors = EnumMapper.GetColorScheme(dto.Colors)!.Value;

        int? fmRadioPreset = null;
        SunsetSound? sunsetSound = null;
        int? volume = null;

        switch (soundDevice)
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
            dto.Intensity,
            dto.Duration,
            colors,
            soundDevice,
            fmRadioPreset,
            sunsetSound,
            volume);
    }
}
