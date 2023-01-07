using System.Net;

namespace Donker.Home.Somneo.ApiClient;

/// <summary>
/// Exception thrown for errors that occur when using the <see cref="SomneoApiClient"/>.
/// </summary>
public sealed class SomneoApiException : Exception
{
    /// <summary>
    /// The status code returned by the API.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }
    /// <summary>
    /// The response content returned by the API, describing the error.
    /// </summary>
    public string? Content { get; }

    internal SomneoApiException(string? message)
        : base(message)
    {
    }

    internal SomneoApiException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    internal SomneoApiException(string? message, HttpStatusCode statusCode, string? content)
        : base(message)
    {
        StatusCode = statusCode;
        Content = content;
    }
}
