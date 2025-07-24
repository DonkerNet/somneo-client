using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class AlarmSettingsMapper
{
    public static AlarmSettings ToModel(AlarmSettingsDto dto)
    {
        var repeatDays = EnumMapper.GetDaysOfWeek(dto.RepeatDayFlags);
        bool powerWakeEnabled = dto.PowerWakeSize == 255;
        bool hasSunrise = dto.SunriseIntensity > 0;
        var soundDevice = EnumMapper.GetSoundDeviceType(dto.SoundDevice);
        int? channelOrPreset = !string.IsNullOrEmpty(dto.ChannelOrPreset) ? int.Parse(dto.ChannelOrPreset) : null;

        int? powerWakeHour = null;
        int? powerWakeMinute = null;
        ColorScheme? sunriseColors = null;
        int? sunriseDuration = null;
        int? sunriseIntensity = null;
        int? fmRadioPreset = null;
        WakeUpSound? wakeUpSound = null;
        int? volume = null;

        if (powerWakeEnabled)
        {
            powerWakeHour = dto.PowerWakeHour;
            powerWakeMinute = dto.PowerWakeMinute;
        }

        if (hasSunrise)
        {
            sunriseColors = EnumMapper.GetColorScheme(dto.SunriseColors);
            sunriseDuration = dto.SunriseDuration;
            sunriseIntensity = dto.SunriseIntensity;
        }

        switch (soundDevice)
        {
            case SoundDeviceType.FMRadio:
                fmRadioPreset = channelOrPreset;
                volume = dto.Volume;
                break;
            case SoundDeviceType.WakeUpSound:
                wakeUpSound = EnumMapper.GetWakeUpSound(channelOrPreset);
                volume = dto.Volume;
                break;
        }

        return new AlarmSettings(
            repeatDays,
            dto.Position,
            dto.Enabled,
            dto.Hour,
            dto.Minute,
            powerWakeEnabled,
            powerWakeHour,
            powerWakeMinute,
            sunriseColors,
            sunriseDuration,
            sunriseIntensity,
            soundDevice,
            fmRadioPreset,
            wakeUpSound,
            volume);
    }
}
