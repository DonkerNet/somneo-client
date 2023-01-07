using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the details of a Somneo device.
/// </summary>
public sealed class DeviceDetails
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// The name of the device.
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// The series of products the device is a part of.
    /// </summary>
    [JsonPropertyName("type")] // For some reason, the series is described by the 'type' property (i.e.: HF367x)
    public string Series { get; init; }
    /// <summary>
    /// The model of the device.
    /// </summary>
    [JsonPropertyName("ctn")] // For some reason, the 'ctn' property seems to describe the model instead of the 'modelid' property (i.e.: HF3671/01)
    public string Model { get; init; }
    /// <summary>
    /// The serial number of the device.
    /// </summary>
    public string Serial { get; init; }
    /// <summary>
    /// The product ID of the device.
    /// </summary>
    public string ProductId { get; init; }
    /// <summary>
    /// The product range the device is a part of.
    /// </summary>
    [JsonPropertyName("modelid")] // For some reason, the product range is described by the 'modelid' property (i.e.: Healthy Sleep)
    public string ProductRange { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
