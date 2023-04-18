namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the details of a Somneo device.
/// </summary>
public sealed class DeviceDetails
{
    /// <summary>
    /// The name of the device.
    /// </summary>
    public string AssignedName { get; }
    /// <summary>
    /// The type number of the device.
    /// </summary>
    public string TypeNumber { get; }
    /// <summary>
    /// The serial number of the device.
    /// </summary>
    public string Serial { get; }
    /// <summary>
    /// The product ID of the device.
    /// </summary>
    public string ProductId { get; }
    /// <summary>
    /// The product name of the device.
    /// </summary>
    public string ProductName { get; }
    /// <summary>
    /// The model ID of the device.
    /// </summary>
    public string ModelId { get; }

    internal DeviceDetails(
        string assignedName,
        string typeNumber,
        string serial,
        string productId,
        string productName,
        string modelId)
    {
        AssignedName = assignedName;
        TypeNumber = typeNumber;
        Serial = serial;
        ProductId = productId;
        ProductName = productName;
        ModelId = modelId;
    }
}
