using System;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient
{
    /// <summary>
    /// Client that provides communication with the Philips Somneo API.
    /// </summary>
    public interface ISomneoApiClient
    {
        #region Public properties

        /// <summary>
        /// Gets the hostname of the Somneo device.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Gets the maximum request timeout in milliseconds.
        /// </summary>
        int Timeout { get; }

        #endregion

        #region General device information

        /// <summary>
        /// Retrieves details about the Somneo device itself.
        /// </summary>
        /// <returns>The details of the device as a <see cref="DeviceDetails"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        DeviceDetails GetDeviceDetails();

        /// <summary>
        /// Retrieves details about the Somneo's wifi connection.
        /// </summary>
        /// <returns>The details of the wifi connection as a <see cref="WifiDetails"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        WifiDetails GetWifiDetails();

        /// <summary>
        /// Retrieves details about the Somneo's firmware.
        /// </summary>
        /// <returns>The firmware details as a <see cref="FirmwareDetails"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        FirmwareDetails GetFirmwareDetails();

        /// <summary>
        /// Retrieves details about the locale set for the Somneo device.
        /// </summary>
        /// <returns>The locale details as a <see cref="Locale"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        Locale GetLocale();

        /// <summary>
        /// Retrieves details about the time set for the Somneo device.
        /// </summary>
        /// <returns>The time details as a <see cref="Time"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        Time GetTime();

        #endregion

        #region Sensors

        /// <summary>
        /// Retrieves the Somneo's sensor data, containing the temperature, light level, sound level and humidity.
        /// </summary>
        /// <returns>The sensor data as a <see cref="SensorData"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        SensorData GetSensorData();

        #endregion

        #region Light management

        /// <summary>
        /// Retrieves the current light settings.
        /// </summary>
        /// <returns>The light settings as a <see cref="LightSettings"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        LightSettings GetLightSettings();

        /// <summary>
        /// Toggles the normal light.
        /// </summary>
        /// <param name="enabled">Whether to enable or disable the light.</param>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void ToggleLight(bool enabled);

        /// <summary>
        /// Sets the level of the normal light and enables the light as well.
        /// </summary>
        /// <param name="lightLevel">The light level to set. Value must be between 1 and 25.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="lightLevel"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void SetLightLevel(int lightLevel);

        /// <summary>
        /// Toggles the night light.
        /// </summary>
        /// <param name="enabled">Whether to enable or disable the night light.</param>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void ToggleNightLight(bool enabled);

        /// <summary>
        /// Toggles the sunrise preview mode.
        /// </summary>
        /// <param name="enabled">Whether to enable or disable the sunrise preview.</param>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void ToggleSunrisePreview(bool enabled);

        #endregion

        #region Display management

        /// <summary>
        /// Retrieves the current settings of the display.
        /// </summary>
        /// <returns>The display settings as a <see cref="LightSettings"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        DisplaySettings GetDisplaySettings();

        /// <summary>
        /// Toggles whether the display should always be shown or if it should disable automatically after a period of time.
        /// </summary>
        /// <param name="enabled">Whether to enable or disable the display permanently.</param>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void TogglePermanentDisplay(bool enabled);

        /// <summary>
        /// Sets the brightness level of the display.
        /// </summary>
        /// <param name="brightnessLevel">The brightness level to set. Value must be between 1 and 6.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="brightnessLevel"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void SetDisplayLevel(int brightnessLevel);

        #endregion
    }
}
