using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Mappers;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient;

/// <summary>
/// Client that provides communication with the Philips Somneo API.
/// </summary>
public interface ISomneoApiClient
{
    #region Public properties

    /// <summary>
    /// Gets the base address used for making requests to the Somneo device.
    /// </summary>
    Uri? BaseAddress { get; }

    /// <summary>
    /// Gets the maximum request timeout.
    /// </summary>
    TimeSpan Timeout { get; }

    #endregion

    #region Somneo: General

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

    #region Somneo: Sensors

    /// <summary>
    /// Retrieves the Somneo's sensor data, containing the temperature, light level, sound level and humidity.
    /// </summary>
    /// <returns>The sensor data as a <see cref="SensorData"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    SensorData GetSensorData();

    #endregion

    #region Somneo: Lights

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

    #endregion

    #region Somneo: Display

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

    #region Somneo: Wake-up sounds

    /// <summary>
    /// Enables a preview of a wake-up sound.
    /// </summary>
    /// <param name="wakeUpSound">The wake-up sound to play.</param>
    /// <param name="volume">The volume. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="wakeUpSound"/> or <paramref name="volume"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void EnableWakeUpSoundPreview(WakeUpSound wakeUpSound, int volume);

    /// <summary>
    /// Disables the preview of a wake-up sound.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void DisableWakeUpSoundPreview();

    #endregion

    #region Somneo: FM radio

    /// <summary>
    /// Retrieves the configured presets of FM radio frequencies.
    /// </summary>
    /// <returns>The FM radio presets as an <see cref="FMRadioPresets"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    FMRadioPresets GetFMRadioPresets();

    /// <summary>
    /// Gets the FM frequency of a preset with the specified position.
    /// </summary>
    /// <param name="position">The preset position. Value must be between 1 and 5.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    float GetFMRadioPreset(int position);

    /// <summary>
    /// Retrieves the state of the FM radio.
    /// </summary>
    /// <returns>The FM radio state as an <see cref="FMRadioState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    FMRadioState GetFMRadioState();

    /// <summary>
    /// Enables the FM radio for the current preset.
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

    #region Somneo: AUX

    /// <summary>
    /// Enables the auxiliary input device.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void EnableAUX();

    #endregion

    #region Somneo: Audio player

    /// <summary>
    /// Retrieves the state of the audio player.
    /// </summary>
    /// <returns>The audio player state as a <see cref="PlayerState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    PlayerState GetPlayerState();

    /// <summary>
    /// Sets the volume of the audio player.
    /// </summary>
    /// <param name="volume">The volume. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="volume"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetPlayerVolume(int volume);

    /// <summary>
    /// Disables the audio player.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void DisablePlayer();

    #endregion

    #region Somneo: Alarms

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
    /// Sets and enables an alarm with a wake-up sound at the specified position in the alarm list and configures it with the specified settings.
    /// </summary>
    /// <param name="position">The position of the alarm to set. Value must be between 1 and 16.</param>
    /// <param name="hour">The hour of the alarm to set. Value must be between 0 and 23.</param>
    /// <param name="minute">The minute of the alarm to set. Value must be between 0 and 59.</param>
    /// <param name="powerWakeMinutes">Sets the amount of minutes when the PowerWake should start after the alarm is triggered. Optional. Value must be between 0 and 59.</param>
    /// <param name="repeatDays">The days on which to repeat the alarm. Optional.</param>
    /// <param name="sunriseColors">The type of sunrise colors to show when the alarm is triggered. Optional.</param>
    /// <param name="sunriseIntensity">
    /// The intensity of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunriseDuration">
    /// The duration of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set.
    /// Value must be between 5 and 40, with 5 minute steps in between.
    /// </param>
    /// <param name="wakeUpSound">The wake-up sound to play when the alarm is triggered.</param>
    /// <param name="volume">The volume of the wake-up sound that is played. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetAlarmWithWakeUpSound(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        WakeUpSound wakeUpSound, int volume);

    /// <summary>
    /// Sets and enables an alarm with FM radio at the specified position in the alarm list and configures it with the specified settings.
    /// </summary>
    /// <param name="position">The position of the alarm to set. Value must be between 1 and 16.</param>
    /// <param name="hour">The hour of the alarm to set. Value must be between 0 and 23.</param>
    /// <param name="minute">The minute of the alarm to set. Value must be between 0 and 59.</param>
    /// <param name="powerWakeMinutes">Sets the amount of minutes when the PowerWake should start after the alarm is triggered. Optional. Value must be between 0 and 59.</param>
    /// <param name="repeatDays">The days on which to repeat the alarm. Optional.</param>
    /// <param name="sunriseColors">The type of sunrise colors to show when the alarm is triggered. Optional.</param>
    /// <param name="sunriseIntensity">
    /// The intensity of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunriseDuration">
    /// The duration of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set.
    /// Value must be between 5 and 40, with 5 minute steps in between.
    /// </param>
    /// <param name="fmRadioPreset">The preset with the FM frequency of the channel to play when the alarm is triggered. Value must be between 1 and 5.</param>
    /// <param name="volume">The volume of the FM radio that is played. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetAlarmWithFMRadio(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int fmRadioPreset, int volume);

    /// <summary>
    /// Sets and enables an alarm with only a sunrise and without any sound.
    /// </summary>
    /// <param name="position">The position of the alarm to set. Value must be between 1 and 16.</param>
    /// <param name="hour">The hour of the alarm to set. Value must be between 0 and 23.</param>
    /// <param name="minute">The minute of the alarm to set. Value must be between 0 and 59.</param>
    /// <param name="powerWakeMinutes">Sets the amount of minutes when the PowerWake should start after the alarm is triggered. Optional. Value must be between 0 and 59.</param>
    /// <param name="repeatDays">The days on which to repeat the alarm. Optional.</param>
    /// <param name="sunriseColors">The type of sunrise colors to show when the alarm is triggered.</param>
    /// <param name="sunriseIntensity">
    /// The intensity of the sunrise to show when the alarm is triggered.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunriseDuration">
    /// The duration of the sunrise to show when the alarm is triggered.
    /// Value must be between 5 and 40, with 5 minute steps in between.
    /// </param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetAlarmWithoutSound(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme sunriseColors, int sunriseIntensity, int sunriseDuration);

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
    AlarmSettings? GetAlarmSettings(int position);

    /// <summary>
    /// Sets the snooze time in minutes for all alarms.
    /// </summary>
    /// <param name="minutes">The snooze time in minutes. Value must be between 1 and 20.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="minutes"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetSnoozeTime(int minutes);

    #endregion

    #region Somneo: Timer

    /// <summary>
    /// Gets the current state of the Somneo's timer, used for the RelaxBreathe and sunset functions.
    /// </summary>
    /// <returns>The timer state as a <see cref="TimerState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    TimerState GetTimerState();

    #endregion

    #region Somneo: Sunrise

    /// <summary>
    /// Enables a preview of a sunrise with the specified settings.
    /// </summary>
    /// <param name="sunriseColors">The type of sunrise to preview.</param>
    /// <param name="sunriseIntensity">
    /// The intensity of the sunrise to preview.
    /// Value must be between 1 and 25.
    /// </param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="sunriseColors"/> or <paramref name="sunriseIntensity"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void EnableSunrisePreview(ColorScheme sunriseColors, int sunriseIntensity);

    /// <summary>
    /// Disables the preview of a sunrise.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void DisableSunrisePreview();

    #endregion

    #region Somneo: Sunset

    /// <summary>
    /// Gets the settings of the Sunset function.
    /// </summary>
    /// <returns>The settings as a <see cref="SunsetSettings"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    SunsetSettings GetSunsetSettings();

    /// <summary>
    /// Toggles the Sunset function.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the sunset.</param>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void ToggleSunset(bool enabled);

    /// <summary>
    /// Sets the Sunset settings with the specified sunset sound.
    /// </summary>
    /// <param name="sunsetColors">The type of sunset colors to show.</param>
    /// <param name="sunsetIntensity">
    /// The maximum intensity of the sunset.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunsetDuration">
    /// The duration of the sunset.
    /// Value must be between 5 and 60, with 5 minute steps in between.
    /// </param>
    /// <param name="sunsetSound">The sunset sound to play.</param>
    /// <param name="volume">
    /// The volume of the sunset sound that is played.
    /// Value must be between 1 and 25.
    /// </param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetSunsetSettingsWithSunsetSound(
        ColorScheme sunsetColors, int sunsetIntensity, int sunsetDuration,
        SunsetSound sunsetSound, int volume);

    /// <summary>
    /// Sets the Sunset settings with the specified FM radio preset.
    /// </summary>
    /// <param name="sunsetColors">The type of sunset colors to show.</param>
    /// <param name="sunsetIntensity">
    /// The maximum intensity of the sunset.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunsetDuration">
    /// The duration of the sunset.
    /// Value must be between 5 and 60, with 5 minute steps in between.
    /// </param>
    /// <param name="fmRadioPreset">
    /// The preset with the FM frequency of the channel to play.
    /// Value must be between 1 and 5.
    /// </param>
    /// <param name="volume">
    /// The volume of the FM radio that is played.
    /// Value must be between 1 and 25.
    /// </param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetSunsetSettingsWithFMRadio(
        ColorScheme sunsetColors, int sunsetIntensity, int sunsetDuration,
        int fmRadioPreset, int volume);

    /// <summary>
    /// Sets the Sunset settings without any sound.
    /// </summary>
    /// <param name="sunsetColors">The type of sunset colors to show.</param>
    /// <param name="sunsetIntensity">
    /// The maximum intensity of the sunset.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunsetDuration">
    /// The duration of the sunset.
    /// Value must be between 5 and 60, with 5 minute steps in between.
    /// </param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void SetSunsetSettingsWithoutSound(ColorScheme sunsetColors, int sunsetIntensity, int sunsetDuration);

    #endregion

    #region Somneo: Bedtime

    /// <summary>
    /// Starts a new bedtime session.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    void StartBedtime();

    /// <summary>
    /// Ends a running bedtime session.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    BedtimeInfo EndBedtime();

    /// <summary>
    /// Returns information about the most recent bedtime session.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    BedtimeInfo? GetLastBedtimeInfo();

    #endregion

    #region Somneo: RelaxBreathe

    /// <summary>
    /// Retrieves the settings of RelaxBreathe, used for breathing exercises to make you fall asleep.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    RelaxBreatheSettings GetRelaxBreatheSettings();

    #endregion

    /*
    Methods left to add:   
    - Save relax breathe settings (sound)
        PUT /di/v1/products/1/wurlx
        {"rtype":1,"durat":10,"progr":4,"sndlv":17}
    - Save relax breathe settings (light)
        PUT /di/v1/products/1/wurlx
        {"rtype":0,"durat":10,"progr":4,"intny":13}
    - Toggle relax breathe
    */
}
