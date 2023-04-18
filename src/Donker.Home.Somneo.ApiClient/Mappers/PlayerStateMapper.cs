using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class PlayerStateMapper
{
    public static PlayerState ToModel(PlayerStateDto dto)
    {
        var device = EnumMapper.GetSoundDeviceType(dto.Device);
        int channelOrPreset = !string.IsNullOrEmpty(dto.ChannelOrPreset) ? int.Parse(dto.ChannelOrPreset) : 0;

        int? fmRadioPreset = null;
        WakeUpSound? wakeUpSound = null;
        SunsetSound? sunsetSound = null;

        switch (device)
        {
            case SoundDeviceType.FMRadio:
                fmRadioPreset = channelOrPreset;
                break;
            case SoundDeviceType.WakeUpSound:
                wakeUpSound = EnumMapper.GetWakeUpSound(channelOrPreset);
                break;
            case SoundDeviceType.Sunset:
                sunsetSound = EnumMapper.GetSunsetSound(channelOrPreset);
                break;
        }

        return new PlayerState(
            dto.Enabled,
            device.HasValue ? dto.Volume : null,
            device,
            fmRadioPreset,
            wakeUpSound,
            sunsetSound);
    }
}
