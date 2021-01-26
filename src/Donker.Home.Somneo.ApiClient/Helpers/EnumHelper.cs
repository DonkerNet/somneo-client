using System;
using System.ComponentModel;
using System.Reflection;

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
                type = Nullable.GetUnderlyingType(type);

            if (type?.IsEnum != true)
                throw new ArgumentException("The supplied value is not an enum.", nameof(enumValue));

            return type
                .GetField(enumValue.ToString())
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description;
        }
    }
}
