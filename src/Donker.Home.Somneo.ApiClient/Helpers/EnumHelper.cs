using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

        internal static IEnumerable<DayOfWeek> DayFlagsToDaysOfWeek(DayFlags dayFlags)
        {
            return Enum.GetValues<DayFlags>()
                .Where(df => df != DayFlags.None && dayFlags.HasFlag(df))
                .Select(df => Enum.Parse<DayOfWeek>(df.ToString()));
        }
    }
}
