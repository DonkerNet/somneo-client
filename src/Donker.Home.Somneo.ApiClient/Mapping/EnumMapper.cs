using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mapping;

internal static class EnumMapper
{
    public static SoundDeviceType? GetSoundDeviceType(string? value)
    {
        return value?.ToLowerInvariant() switch
        {
            "wus" => SoundDeviceType.WakeUpSound,
            "fmr" => SoundDeviceType.FMRadio,
            "aux" => SoundDeviceType.AUX,
            "rlx" => SoundDeviceType.RelaxBreathe,
            "dus" => SoundDeviceType.Sunset,
            _ => null // "off"
        };
    }

    public static string GetSoundDeviceTypeValue(SoundDeviceType? soundDeviceType)
    {
        return soundDeviceType switch
        {
            SoundDeviceType.WakeUpSound => "wus",
            SoundDeviceType.FMRadio => "fmr",
            SoundDeviceType.AUX => "aux",
            SoundDeviceType.RelaxBreathe => "rlx",
            SoundDeviceType.Sunset => "dus",
            _ => "off"
        };
    }

    public static ColorScheme? GetColorScheme(int? value)
    {
        return value switch
        {
            0 => ColorScheme.SunnyDay,
            1 => ColorScheme.IslandRed,
            2 => ColorScheme.NordicWhite,
            _ => null
        };
    }

    public static int? GetColorSchemeValue(ColorScheme? colorScheme)
    {
        return colorScheme switch
        {
            ColorScheme.SunnyDay => 0,
            ColorScheme.IslandRed => 1,
            ColorScheme.NordicWhite => 2,
            _ => null
        };
    }

    public static WakeUpSound? GetWakeUpSound(int? value)
    {
        return value switch
        {
            1 => WakeUpSound.ForestBirds,
            2 => WakeUpSound.SummerBirds,
            3 => WakeUpSound.BuddhaWakeUp,
            4 => WakeUpSound.MorningAlps,
            5 => WakeUpSound.YogaHarmony,
            6 => WakeUpSound.NepalBowls,
            7 => WakeUpSound.SummerLake,
            8 => WakeUpSound.OceanWaves,
            _ => null
        };
    }

    public static int? GetWakeUpSoundValue(WakeUpSound? wakeUpSound)
    {
        return wakeUpSound switch
        {
            WakeUpSound.ForestBirds => 1,
            WakeUpSound.SummerBirds => 2,
            WakeUpSound.BuddhaWakeUp => 3,
            WakeUpSound.MorningAlps => 4,
            WakeUpSound.YogaHarmony => 5,
            WakeUpSound.NepalBowls => 6,
            WakeUpSound.SummerLake => 7,
            WakeUpSound.OceanWaves => 8,
            _ => null
        };
    }

    public static SunsetSound? GetSunsetSound(int? value)
    {
        return value switch
        {
            1 => SunsetSound.SoftRain,
            2 => SunsetSound.OceanWaves,
            3 => SunsetSound.UnderWater,
            4 => SunsetSound.SummerLake,
            _ => null
        };
    }

    public static int? GetSunsetSound(SunsetSound? sunsetSound)
    {
        return sunsetSound switch
        {
            SunsetSound.SoftRain => 1,
            SunsetSound.OceanWaves => 2,
            SunsetSound.UnderWater => 3,
            SunsetSound.SummerLake => 4,
            _ => null
        };
    }

    public static IEnumerable<DayOfWeek> GetDaysOfWeek(byte value)
    {
        var dayFlags = (DayFlags)value;
        return Enum.GetValues<DayFlags>()
            .Where(df => df != DayFlags.None && dayFlags.HasFlag(df))
            .Select(df => Enum.Parse<DayOfWeek>(df.ToString()));
    }

    public static byte GetDaysOfWeekValue(IEnumerable<DayOfWeek>? daysOfWeek)
    {
        if (daysOfWeek == null)
            return (byte)DayFlags.None;

        DayFlags dayFlags = DayFlags.None;

        foreach (DayOfWeek dayOfWeek in daysOfWeek.Distinct())
            dayFlags |= Enum.Parse<DayFlags>(dayOfWeek.ToString());


        return (byte)dayFlags;
    }

    [Flags]
    private enum DayFlags : byte
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
