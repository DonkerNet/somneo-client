using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// The type of sound device usable by the Somneo's audio player.
    /// </summary>
    public enum SoundDeviceType
    {
        /// <summary>
        /// No sound device.
        /// </summary>
        [EnumMember(Value = "off")]
        [Description("None")]
        None,
        /// <summary>
        /// Wake-up sounds.
        /// </summary>
        [EnumMember(Value = "wus")]
        [Description("Wake-up sound")]
        WakeUpSound,
        /// <summary>
        /// FM radio.
        /// </summary>
        [EnumMember(Value = "fmr")]
        [Description("FM radio")]
        FMRadio,
        /// <summary>
        /// Auxiliary input.
        /// </summary>
        [EnumMember(Value = "aux")]
        [Description("AUX")]
        AUX
    }

    /// <summary>
    /// The direction in which to seek a station on the radio band.
    /// </summary>
    public enum RadioSeekDirection
    {
        /// <summary>
        /// Seek a radio station on the band in forwards direction.
        /// </summary>
        [Description("Up")]
        Up,
        /// <summary>
        /// Seek a radio station on the band in backwards direction.
        /// </summary>
        [Description("Down")]
        Down
    }

    /// <summary>
    /// The color scheme used for a sunrise or sunset.
    /// </summary>
    public enum ColorScheme
    {
        /// <summary>
        /// No light.
        /// </summary>
        [Description("No light")]
        NoLight,
        /// <summary>
        /// Sunny day.
        /// </summary>
        [Description("Sunny day")]
        SunnyDay,
        /// <summary>
        /// Island red.
        /// </summary>
        [Description("Island red")]
        IslandRed,
        /// <summary>
        /// Nordic white.
        /// </summary>
        [Description("Nordic white")]
        NordicWhite,
        /// <summary>
        /// Carribean red.
        /// </summary>
        [Description("Carribean red")]
        CarribeanRed
    }

    /// <summary>
    /// The wake-up sound used by an alarm.
    /// </summary>
    public enum WakeUpSound
    {
        /// <summary>
        /// Forest birds wake-up sound.
        /// </summary>
        [Description("Forest birds")]
        ForestBirds = 1,
        /// <summary>
        /// Summer birds wake-up sound.
        /// </summary>
        [Description("Summer birds")]
        SummerBirds = 2,
        /// <summary>
        /// Buddha wake-up sound.
        /// </summary>
        [Description("Buddha wake-up")]
        BuddhaWakeUp = 3,
        /// <summary>
        /// Morning alps wake-up sound.
        /// </summary>
        [Description("Morning alps")]
        MorningAlps = 4,
        /// <summary>
        /// Yoga harmony wake-up sound.
        /// </summary>
        [Description("Yoga harmony")]
        YogaHarmony = 5,
        /// <summary>
        /// Nepal bowls wake-up sound.
        /// </summary>
        [Description("Nepal bowls")]
        NepalBowls = 6,
        /// <summary>
        /// Summer lake wake-up sound.
        /// </summary>
        [Description("Summer lake")]
        SummerLake = 7,
        /// <summary>
        /// Ocean waves wake-up sound.
        /// </summary>
        [Description("Ocean waves")]
        OceanWaves = 8
    }

    [Flags]
    internal enum DayFlags : byte
    {
        None = 0,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,
        Sunday = 128
    }
}
