using System.ComponentModel;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// The type of sound device usable by the Somneo's audio player.
/// </summary>
public enum SoundDeviceType
{
    /// <summary>
    /// Wake-up sounds.
    /// </summary>
    [Description("Wake-up sound")]
    WakeUpSound,
    /// <summary>
    /// FM radio.
    /// </summary>
    [Description("FM radio")]
    FMRadio,
    /// <summary>
    /// Auxiliary input.
    /// </summary>
    [Description("AUX")]
    AUX,
    /// <summary>
    /// RelaxBreathe.
    /// </summary>
    [Description("RelaxBreathe")]
    RelaxBreathe,
    /// <summary>
    /// Sunset.
    /// </summary>
    [Description("Sunset")]
    Sunset
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
    NordicWhite
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
    ForestBirds,
    /// <summary>
    /// Summer birds wake-up sound.
    /// </summary>
    [Description("Summer birds")]
    SummerBirds,
    /// <summary>
    /// Buddha wake-up sound.
    /// </summary>
    [Description("Buddha wake-up")]
    BuddhaWakeUp,
    /// <summary>
    /// Morning alps wake-up sound.
    /// </summary>
    [Description("Morning alps")]
    MorningAlps,
    /// <summary>
    /// Yoga harmony wake-up sound.
    /// </summary>
    [Description("Yoga harmony")]
    YogaHarmony,
    /// <summary>
    /// Nepal bowls wake-up sound.
    /// </summary>
    [Description("Nepal bowls")]
    NepalBowls,
    /// <summary>
    /// Summer lake wake-up sound.
    /// </summary>
    [Description("Summer lake")]
    SummerLake,
    /// <summary>
    /// Ocean waves wake-up sound.
    /// </summary>
    [Description("Ocean waves")]
    OceanWaves
}

/// <summary>
/// The sound used for the sunset.
/// </summary>
public enum SunsetSound
{
    /// <summary>
    /// Soft rain sunset sound.
    /// </summary>
    [Description("Soft rain")]
    SoftRain,
    /// <summary>
    /// Ocean waves sunset sound.
    /// </summary>
    [Description("Ocean waves")]
    OceanWaves,
    /// <summary>
    /// Under water sunset sound.
    /// </summary>
    [Description("Under water")]
    UnderWater,
    /// <summary>
    /// Summer lake sunset sound.
    /// </summary>
    [Description("Summer lake")]
    SummerLake
}
