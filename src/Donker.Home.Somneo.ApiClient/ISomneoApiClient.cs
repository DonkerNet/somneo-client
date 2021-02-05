using System;
using System.Collections.Generic;
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

        #region General

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

        #region Lights

        /// <summary>
        /// Retrieves the current light state.
        /// </summary>
        /// <returns>The light state as a <see cref="LightState"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        LightState GetLightState();

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

        #region Display

        /// <summary>
        /// Retrieves the current state of the display.
        /// </summary>
        /// <returns>The display state as a <see cref="DisplayState"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        DisplayState GetDisplayState();

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

        #region Wake-up sounds

        /// <summary>
        /// Plays a wake-up sound.
        /// </summary>
        /// <param name="wakeUpSound">The wake-up sound to play.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="wakeUpSound"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void PlayWakeUpSound(WakeUpSound wakeUpSound);

        #endregion

        #region FM radio

        /// <summary>
        /// Retrieves the configured presets of FM radio frequencies.
        /// </summary>
        /// <returns>The FM radio presets as an <see cref="FMRadioPresets"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        FMRadioPresets GetFMRadioPresets();

        /// <summary>
        /// Sets the preset of the specified position to the specified FM frequency.
        /// </summary>
        /// <param name="position">The preset position. Value must be between 1 and 5.</param>
        /// <param name="frequency">The FM frequency. Value must be between 87.50 to 107.99.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> or <paramref name="frequency"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void SetFMRadioPreset(int position, float frequency);

        /// <summary>
        /// Retrieves the state of the FM radio.
        /// </summary>
        /// <returns>The FM radio state as an <see cref="FMRadioState"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        FMRadioState GetFMRadioState();

        /// <summary>
        /// Enables the FM radio.
        /// </summary>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void EnableFMRadio();

        /// <summary>
        /// Enables the FM radio for the specified preset.
        /// </summary>
        /// <param name="preset">The preset. Value must be between 1 and 5.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="preset"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void EnableFMRadioPreset(int preset);

        /// <summary>
        /// Seeks a new FM radio station in the specified direction for the currently selected preset, if the FM radio is enabled.
        /// </summary>
        /// <param name="direction">The seek direction.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="direction"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void SeekFMRadioStation(RadioSeekDirection direction);

        #endregion

        #region AUX

        /// <summary>
        /// Enables the auxiliary input device.
        /// </summary>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void EnableAUX();

        #endregion

        #region Audio player

        /// <summary>
        /// Retrieves the state of the audio player.
        /// </summary>
        /// <returns>The audio player state as a <see cref="PlayerState"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        PlayerState GetPlayerState();

        /// <summary>
        /// Sets the volume of the audio player.
        /// </summary>
        /// <param name="position">The volume. Value must be between 1 and 25.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="volume"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void SetPlayerVolume(int volume);

        /// <summary>
        /// Disables the audio player.
        /// </summary>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void DisablePlayer();

        #endregion

        #region Alarms

        /// <summary>
        /// Retrieves the alarms.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyList{T}"/> containing <see cref="Alarm"/> objects.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        IReadOnlyList<Alarm> GetAlarms();

        /// <summary>
        /// Toggles an alarm by it's position in the alarm list. If the alarm does not exist yet, it will be added with default settings for that position.
        /// </summary>
        /// <param name="position">The position of the alarm to toggle. Value must be between 1 and 16.</param>
        /// <param name="enabled">Whether to enable or disable the alarm.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void ToggleAlarm(int position, bool enabled);

        /// <summary>
        /// Removes an alarm by it's position in the alarm list and restores the default settings for that position. Removal will fail when only two alarms are left.
        /// </summary>
        /// <param name="position">The position of the alarm to remove. Value must be between 1 and 16.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void RemoveAlarm(int position);

        /// <summary>
        /// Gets the settings of an alarm by it's position in the alarm list.
        /// </summary>
        /// <param name="position">The position of the alarm to retrieve the settings for. Value must be between 1 and 16.</param>
        /// <returns>The settings as an <see cref="AlarmSettings"/> object if the alarm is set; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        AlarmSettings GetAlarmSettings(int position);

        /// <summary>
        /// Sets the snooze time in minutes for all alarms.
        /// </summary>
        /// <param name="minutes">The snooze time in minutes. Value must be between 1 and 20.</param>
        /// <exception cref="ArgumentException">Exception thrown when the <paramref name="minutes"/> parameter is invalid.</exception>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        void SetSnoozeTime(int minutes);

        #endregion
    }
}
