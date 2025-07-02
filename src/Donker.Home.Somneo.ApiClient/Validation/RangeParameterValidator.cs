using System.Numerics;

namespace Donker.Home.Somneo.ApiClient.Validation;

/// <summary>
/// Defines a range within a parameter value is considered to be valid and optionally in what number of steps the parameter is allowed to be increased or decreased.
/// </summary>
/// <typeparam name="T">The type of the parameter.</typeparam>
/// <param name="paramDescription">The short, human-readable description of this parameter.</param>
/// <param name="minValue">The minimum value the parameter can have.</param>
/// <param name="maxValue">The maximum value the parameter can have.</param>
/// <param name="step">Optional. The step in which the parameter is allowed to be increased or decreased.</param>
public class RangeParameterValidator<T>(string paramDescription, T minValue, T maxValue, T? step = default)
    where T : IComparable<T>, IModulusOperators<T, T, T>
{
    /// <summary>
    /// The short, human-readable description of this parameter.
    /// </summary>
    public string ParamDescription { get; } = paramDescription;
    /// <summary>
    /// The minimum value the parameter can have.
    /// </summary>
    public T MinValue { get; } = minValue;
    /// <summary>
    /// The maximum value the parameter can have.
    /// </summary>
    public T MaxValue { get; } = maxValue;
    /// <summary>
    /// The step in which the parameter is allowed to be increased or decreased.
    /// </summary>
    public T? Step { get; } = step;

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the specified parameter value is outside of the valid range, or does not match the valid steps.
    /// </summary>
    /// <param name="value">The parameter value to validate.</param>
    /// <param name="paramName">The parameter name to include when an exception is thrown.</param>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the parameter value is out of range.</exception>
    public void ThrowIfOutOfRange(T value, string paramName)
    {
        bool isInRange = value.CompareTo(MinValue) >= 0 && value.CompareTo(MaxValue) <= 0;
        bool hasStep = Step != null && !Equals(Step, default(T));

        if (!isInRange && !hasStep)
        {
            string errorMessage = $"The {ParamDescription} must be between {MinValue} and {MaxValue}.";
            throw new ArgumentOutOfRangeException(paramName, value, errorMessage);
        }

        bool isValidStep = !hasStep || Equals(value % Step!, default(T));

        if (!isInRange || !isValidStep)
        {
            string errorMessage = $"The {ParamDescription} must be between {MinValue} and {MaxValue}, with incremental steps of {Step}.";
            throw new ArgumentOutOfRangeException(paramName, value, errorMessage);
        }
    }
}
