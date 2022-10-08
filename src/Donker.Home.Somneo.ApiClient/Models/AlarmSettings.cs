using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Donker.Home.Somneo.ApiClient.Helpers;
using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the settings of a specific alarm of the Somneo device.
    /// </summary>
    public sealed class AlarmSettings
    {
        private int? _powerWakeHour;
        private int? _powerWakeMinute;

        [JsonProperty("prfvs")]
        internal bool IsSet { get; init; }

        [JsonProperty("daynm")]
        internal DayFlags RepeatDayFlags
        {
            init => RepeatDays = new ReadOnlyCollection<DayOfWeek>(EnumHelper.DayFlagsToDaysOfWeek(value).ToList());
        }

        [JsonProperty("ctype")]
        internal byte ColorSchemeNumber { get; init; }

        [JsonProperty("pwrsz")]
        internal byte PowerWakeSize { get; init; }

        /// <summary>
        /// The position of the alarm in the alarm list. Can be between 1 and 16.
        /// </summary>
        [JsonProperty("prfnr")]
        public int Position { get; init; }
        /// <summary>
        /// Whether the alarm is enabled or disabled.
        /// </summary>
        [JsonProperty("prfen")]
        public bool Enabled { get; init; }
        /// <summary>
        /// The hour of the alarm.
        /// </summary>
        [JsonProperty("almhr")]
        public int Hour { get; init; }
        /// <summary>
        /// The minute of the alarm.
        /// </summary>
        [JsonProperty("almmn")]
        public int Minute { get; init; }
        /// <summary>
        /// On which days of the week the alarm is repeated.
        /// </summary>
        public IReadOnlyList<DayOfWeek> RepeatDays { get; private set; }
        /// <summary>
        /// Whether the PowerWake function is enabled or not for this alarm.
        /// </summary>
        public bool PowerWakeEnabled => PowerWakeSize == 255;
        /// <summary>
        /// The hour of the PowerWake, if enabled.
        /// </summary>
        [JsonProperty("pszhr")]
        public int? PowerWakeHour
        {
            get => PowerWakeEnabled ? _powerWakeHour : null;
            init => _powerWakeHour = value;
        }
        /// <summary>
        /// The minute of the PowerWake, if enabled.
        /// </summary>
        [JsonProperty("pszmn")]
        public int? PowerWakeMinute
        {
            get => PowerWakeEnabled ? _powerWakeMinute : null;
            init => _powerWakeMinute = value;
        }
        /// <summary>
        /// The duration of the sunrise.
        /// </summary>
        [JsonProperty("durat")]
        public int SunriseDuration { get; init; }
        /// <summary>
        /// The maximum light level of the sunrise.
        /// </summary>
        [JsonProperty("curve")]
        public int SunriseIntensity { get; init; }
        /// <summary>
        /// The type of sunrise colors shown.
        /// </summary>
        public ColorScheme SunriseColors => EnumHelper.GetColorScheme(ColorSchemeNumber, SunriseIntensity);
        /// <summary>
        /// The type of sound device used for the alarm sound.
        /// </summary>
        [JsonProperty("snddv")]
        public SoundDeviceType Device { get; init; }
        /// <summary>
        /// The channel or preset that is selected for the alarm sound.
        /// </summary>
        [JsonProperty("sndch")]
        public string ChannelOrPreset { get; init; }
        /// <summary>
        /// The alarm's volume level. Can be between 1 and 25.
        /// </summary>
        [JsonProperty("sndlv")]
        public int Volume { get; init; }

        /// <summary>
        /// Gets the FM-radio preset if the <see cref="Device"/> property is set to <see cref="SoundDeviceType.FMRadio"/>.
        /// </summary>
        public int? GetFMRadioPreset()
        {
            if (Device == SoundDeviceType.FMRadio
                && !string.IsNullOrEmpty(ChannelOrPreset)
                && int.TryParse(ChannelOrPreset, out int fmRadioPreset))
                return fmRadioPreset;

            return null;
        }
        /// <summary>
        /// Gets the wake-up sound if the <see cref="Device"/> property is set to <see cref="SoundDeviceType.WakeUpSound"/>.
        /// </summary>
        public WakeUpSound? GetWakeUpSound()
        {
            if (Device == SoundDeviceType.WakeUpSound
                    && !string.IsNullOrEmpty(ChannelOrPreset)
                    && Enum.TryParse(ChannelOrPreset, out WakeUpSound wakeUpSound))
                return wakeUpSound;

            return null;
        }
    }
}
