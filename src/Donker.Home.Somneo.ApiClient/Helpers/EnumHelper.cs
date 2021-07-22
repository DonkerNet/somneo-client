using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Helpers
{
    /// <summary>
    /// Contains helper methods for enum types.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the description of an enum value from the <see cref="DescriptionAttribute"/>, or returns <c>null</c> if the attribute is not present.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The value of the enum.</param>
        /// <returns>The description if present; otherwise, <c>null</c>.</returns>
        public static string GetDescription<TEnum>(TEnum enumValue)
            where TEnum : struct
        {
            Type type = typeof(TEnum);

            if (!type.IsEnum)
                throw new ArgumentException("The supplied value is not an enum.", nameof(enumValue));

            return type
                .GetField(enumValue.ToString())
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description;
        }

        /// <summary>
        /// Gets the <see cref="EnumMemberAttribute.Value"/> property of an enum value from the <see cref="EnumMemberAttribute"/>, or returns <c>null</c> if the attribute is not present.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The value of the enum.</param>
        /// <returns>The <see cref="EnumMemberAttribute"/> value if present; otherwise, <c>null</c>.</returns>
        public static string GetEnumMemberValue<TEnum>(TEnum enumValue)
            where TEnum : struct
        {
            Type type = typeof(TEnum);
            
            if (!type.IsEnum)
                throw new ArgumentException("The supplied value is not an enum.", nameof(enumValue));

            return type
                .GetField(enumValue.ToString())
                .GetCustomAttribute<EnumMemberAttribute>()?
                .Value;
        }

        internal static SunriseType GetSunriseType(int number, int intensity)
        {
            return number switch
            {
                0 when intensity > 0 => SunriseType.SunnyDay,
                1 => SunriseType.IslandRed,
                2 => SunriseType.NordicWhite,
                3 => SunriseType.CarribeanRed,
                _ => SunriseType.NoLight,
            };
        }

        internal static int GetSunriseTypeNumber(SunriseType sunriseType)
        {
            return sunriseType switch
            {
                SunriseType.IslandRed => 1,
                SunriseType.NordicWhite => 2,
                SunriseType.CarribeanRed => 3,
                _ => 0,
            };
        }

        internal static IEnumerable<DayOfWeek> DayFlagsToDaysOfWeek(DayFlags dayFlags)
        {
            return Enum.GetValues<DayFlags>()
                .Where(df => df != DayFlags.None && dayFlags.HasFlag(df))
                .Select(df => Enum.Parse<DayOfWeek>(df.ToString()));
        }

        internal static DayFlags DaysOfWeekToDayFlags(IEnumerable<DayOfWeek> daysOfWeek)
        {
            if (daysOfWeek == null)
                return DayFlags.None;

            DayFlags dayFlags = DayFlags.None;

            foreach (DayOfWeek dayOfWeek in daysOfWeek.Distinct())
                dayFlags |= Enum.Parse<DayFlags>(dayOfWeek.ToString());


            return dayFlags;
        }
    }
}
