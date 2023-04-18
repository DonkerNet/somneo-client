using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;
using System.Collections.ObjectModel;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class AlarmMapper
{
    public static IReadOnlyList<Alarm> ToModels(AlarmStatesDto alarmStatesDto, AlarmSchedulesDto alarmSchedulesDto)
    {
        int alarmCount = alarmStatesDto.Set.Length;
        var alarms = new List<Alarm>(alarmCount);

        for (int i = 0; i < alarmCount; ++i)
        {
            if (!alarmStatesDto.Set[i])
                continue;

            int powerWakeIndex = i * 3;

            bool enabled = alarmStatesDto.Enabled[i];
            bool powerWakeEnabled = alarmStatesDto.PowerWake[powerWakeIndex] == 255;
            int? powerWakeHour = powerWakeEnabled ? alarmStatesDto.PowerWake[powerWakeIndex + 1] : null;
            int? powerWakeMinute = powerWakeEnabled ? alarmStatesDto.PowerWake[powerWakeIndex + 2] : null;

            var repeatDays = EnumMapper.GetDaysOfWeek(alarmSchedulesDto.RepeatDayFlags[i]).ToList();

            int hour = alarmSchedulesDto.Hours[i];
            int minute = alarmSchedulesDto.Minutes[i];

            var alarm = new Alarm(
                repeatDays,
                i + 1,
                enabled,
                hour,
                minute,
                powerWakeEnabled,
                powerWakeHour,
                powerWakeMinute);

            alarms.Add(alarm);
        }

        return new ReadOnlyCollection<Alarm>(alarms);
    }
}
