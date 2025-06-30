namespace Donker.Home.Somneo.ApiClient.Validation;

/// <summary>
/// Defines an enumerable that contains the values a parameter is allowed to have.
/// </summary>
/// <typeparam name="TEnum">The enumerable type of the parameter.</typeparam>
/// <param name="ParamDescription">The short, human-readable description of this parameter.</param>
public record EnumParameterValidator<TEnum>(string ParamDescription)
    where TEnum : struct, Enum
{
    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the specified parameter value is outside of the range of enumerable values.
    /// </summary>
    /// <param name="value">The parameter value to validate.</param>
    /// <param name="paramName">The parameter name to include when an exception is thrown.</param>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the parameter value is out of range.</exception>
    public void ThrowIfOutOfRange(TEnum value, string paramName)
    {
        if (!Enum.IsDefined(value))
            throw new ArgumentOutOfRangeException(paramName, value, $"The {ParamDescription} is invalid.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when one or more parameter values are outside of the range of enumerable values.
    /// </summary>
    /// <param name="values">The parameter values to validate.</param>
    /// <param name="paramName">The parameter name to include when an exception is thrown.</param>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the parameter value is out of range.</exception>
    public void ThrowIfOutOfRange(IEnumerable<TEnum> values, string paramName)
    {
        foreach (var value in values)
        {
            if (!Enum.IsDefined(value))
                throw new ArgumentOutOfRangeException(paramName, value, $"One or more {ParamDescription} values are invalid.");
        }
    }
}
