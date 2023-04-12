using System.ComponentModel;
using System.Reflection;

namespace Donker.Home.Somneo.TestConsole.Helpers;

public static class EnumHelper
{
    public static string? GetDescription<TEnum>(TEnum enumValue)
        where TEnum : struct, Enum
    {
        Type type = typeof(TEnum);

        string enumMemberName = enumValue.ToString()!;

        return type
            .GetField(enumMemberName)?
            .GetCustomAttribute<DescriptionAttribute>()?
            .Description;
    }

    public static bool TryCast<TEnum>(int value, out TEnum result)
        where TEnum : struct, Enum
    {
        foreach (TEnum enumValue in Enum.GetValues<TEnum>())
        {
            int intValue = Convert.ToInt32(enumValue);
            if (intValue == value)
            {
                result = enumValue;
                return true;
            }
        }

        result = default;
        return false;
    }
}
