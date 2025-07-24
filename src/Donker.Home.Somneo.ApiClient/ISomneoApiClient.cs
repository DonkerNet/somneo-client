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
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the details of the device as a <see cref="DeviceDetails"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<DeviceDetails> GetDeviceDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves details about the Somneo's wifi connection.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the details of the wifi connection as a <see cref="WifiDetails"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<WifiDetails> GetWifiDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves details about the Somneo's firmware.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the firmware details as a <see cref="FirmwareDetails"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<FirmwareDetails> GetFirmwareDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves details about the locale set for the Somneo device.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the locale details as a <see cref="Locale"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<Locale> GetLocaleAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves details about the time set for the Somneo device.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the time details as a <see cref="Time"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<Time> GetTimeAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Sensors

    /// <summary>
    /// Retrieves the Somneo's sensor data, containing the temperature, light level, sound level and humidity.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the sensor data as a <see cref="SensorData"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<SensorData> GetSensorDataAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Lights

    /// <summary>
    /// Retrieves the current light state.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the light state as a <see cref="LightState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<LightState> GetLightStateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles the normal light.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the light.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task ToggleLightAsync(bool enabled, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the level of the normal light and enables the light as well.
    /// </summary>
    /// <param name="lightLevel">The light level to set. Value must be between 1 and 25.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="lightLevel"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetLightLevelAsync(int lightLevel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles the night light.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the night light.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task ToggleNightLightAsync(bool enabled, CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Display

    /// <summary>
    /// Retrieves the current state of the display.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the display state as a <see cref="DisplayState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<DisplayState> GetDisplayStateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles whether the display should always be shown or if it should disable automatically after a period of time.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the display permanently.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task TogglePermanentDisplayAsync(bool enabled, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the brightness level of the display.
    /// </summary>
    /// <param name="displayLevel">The brightness level to set. Value must be between 1 and 6.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="displayLevel"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetDisplayLevelAsync(int displayLevel, CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Wake-up sounds

    /// <summary>
    /// Enables a preview of a wake-up sound.
    /// </summary>
    /// <param name="wakeUpSound">The wake-up sound to play.</param>
    /// <param name="volume">The volume. Value must be between 1 and 25.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="wakeUpSound"/> or <paramref name="volume"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task EnableWakeUpSoundPreviewAsync(WakeUpSound wakeUpSound, int volume, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables the preview of a wake-up sound.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task DisableWakeUpSoundPreviewAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: FM radio

    /// <summary>
    /// Retrieves the configured presets of FM radio frequencies.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the FM radio presets as an <see cref="FMRadioPresets"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<FMRadioPresets> GetFMRadioPresetsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the FM frequency of a preset with the specified position.
    /// </summary>
    /// <param name="preset">The preset position. Value must be between 1 and 5.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the FM frequency as a <see cref="float"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="preset"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<float> GetFMRadioPresetAsync(int preset, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the state of the FM radio.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the FM radio state as an <see cref="FMRadioState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<FMRadioState> GetFMRadioStateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Enables the FM radio for the current preset.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task EnableFMRadioAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Enables the FM radio for the specified preset.
    /// </summary>
    /// <param name="preset">The preset. Value must be between 1 and 5.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="preset"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task EnableFMRadioPresetAsync(int preset, CancellationToken cancellationToken = default);

    /// <summary>
    /// Seeks a new FM radio station in the specified direction for the currently selected preset, if the FM radio is enabled.
    /// </summary>
    /// <param name="seekDirection">The seek direction.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="seekDirection"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SeekFMRadioStationAsync(RadioSeekDirection seekDirection, CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: AUX

    /// <summary>
    /// Enables the auxiliary input device.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task EnableAUXAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Audio player

    /// <summary>
    /// Retrieves the state of the audio player.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the audio player state as a <see cref="PlayerState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<PlayerState> GetPlayerStateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the volume of the audio player.
    /// </summary>
    /// <param name="volume">The volume. Value must be between 1 and 25.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="volume"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetPlayerVolumeAsync(int volume, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables the audio player.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task DisablePlayerAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Alarms

    /// <summary>
    /// Retrieves the alarms.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning an <see cref="IReadOnlyList{T}"/> containing <see cref="Alarm"/> objects.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<IReadOnlyList<Alarm>> GetAlarmsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles an alarm by it's position in the alarm list. If the alarm does not exist yet, it will be added with default settings for that position.
    /// </summary>
    /// <param name="position">The position of the alarm to toggle. Value must be between 1 and 16.</param>
    /// <param name="enabled">Whether to enable or disable the alarm.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="position"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task ToggleAlarmAsync(int position, bool enabled, CancellationToken cancellationToken = default);

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
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetAlarmWithWakeUpSoundAsync(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        WakeUpSound wakeUpSound, int volume,
        CancellationToken cancellationToken = default);

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
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetAlarmWithFMRadioAsync(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int fmRadioPreset, int volume,
        CancellationToken cancellationToken = default);

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
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetAlarmWithoutSoundAsync(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme sunriseColors, int sunriseIntensity, int sunriseDuration,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes an alarm by it's position in the alarm list and restores the default settings for that position. Removal will fail when only two alarms are left.
    /// </summary>
    /// <param name="position">The position of the alarm to remove. Value must be between 1 and 16.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="position"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task RemoveAlarmAsync(int position, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the settings of an alarm by it's position in the alarm list.
    /// </summary>
    /// <param name="position">The position of the alarm to retrieve the settings for. Value must be between 1 and 16.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the settings as an <see cref="AlarmSettings"/> object if the alarm is set; otherwise, <c>null</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="position"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<AlarmSettings?> GetAlarmSettingsAsync(int position, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the snooze time in minutes for all alarms.
    /// </summary>
    /// <param name="minutes">The snooze time in minutes. Value must be between 1 and 20.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="minutes"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetSnoozeTimeAsync(int minutes, CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Timer

    /// <summary>
    /// Gets the current state of the Somneo's timer, used for the RelaxBreathe and sunset functions.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the timer state as a <see cref="TimerState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<TimerState> GetTimerStateAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Sunrise

    /// <summary>
    /// Enables a preview of a sunrise with the specified settings.
    /// </summary>
    /// <param name="colors">The type of sunrise to preview.</param>
    /// <param name="intensity">
    /// The intensity of the sunrise to preview.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when the <paramref name="colors"/> or <paramref name="intensity"/> parameter is out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task EnableSunrisePreviewAsync(ColorScheme colors, int intensity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables the preview of a sunrise.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task DisableSunrisePreviewAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Sunset

    /// <summary>
    /// Gets the settings of the Sunset function.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the settings as a <see cref="SunsetSettings"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<SunsetSettings> GetSunsetSettingsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles the Sunset function.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the sunset.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task ToggleSunsetAsync(bool enabled, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the Sunset settings with the specified sunset sound.
    /// </summary>
    /// <param name="colors">The type of sunset colors to show.</param>
    /// <param name="intensity">
    /// The maximum intensity of the sunset.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="duration">
    /// The duration of the sunset.
    /// Value must be between 5 and 60, with 5 minute steps in between.
    /// </param>
    /// <param name="sunsetSound">The sunset sound to play.</param>
    /// <param name="volume">
    /// The volume of the sunset sound that is played.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetSunsetSettingsWithSunsetSoundAsync(
        ColorScheme colors, int intensity, int duration,
        SunsetSound sunsetSound, int volume,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the Sunset settings with the specified FM radio preset.
    /// </summary>
    /// <param name="colors">The type of sunset colors to show.</param>
    /// <param name="intensity">
    /// The maximum intensity of the sunset.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="duration">
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
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetSunsetSettingsWithFMRadioAsync(
        ColorScheme colors, int intensity, int duration,
        int fmRadioPreset, int volume,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the Sunset settings without any sound.
    /// </summary>
    /// <param name="colors">The type of sunset colors to show.</param>
    /// <param name="intensity">
    /// The maximum intensity of the sunset.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="duration">
    /// The duration of the sunset.
    /// Value must be between 5 and 60, with 5 minute steps in between.
    /// </param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetSunsetSettingsWithoutSoundAsync(ColorScheme colors, int intensity, int duration, CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: Bedtime

    /// <summary>
    /// Starts a new bedtime session.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task StartBedtimeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Ends a running bedtime session.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the information of the bedtime session as a <see cref="BedtimeInfo"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<BedtimeInfo> EndBedtimeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about the most recent bedtime session.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the information of the most recent bedtime session as a <see cref="BedtimeInfo"/> object, if there was one.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<BedtimeInfo?> GetLastBedtimeInfoAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Somneo: RelaxBreathe

    /// <summary>
    /// Retrieves the settings of RelaxBreathe, used for breathing exercises to make you fall asleep faster.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task returning the settings as a <see cref="RelaxBreatheSettings"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task<RelaxBreatheSettings> GetRelaxBreatheSettingsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles RelaxBreathe on or off.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable RelaxBreathe.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task ToggleRelaxBreatheAsync(bool enabled, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the RelaxBreathe settings, using sound for the breathing exercises.
    /// </summary>
    /// <param name="duration">How long the breathing exercises should run. Must be 5, 10 or 15 minutes.</param>
    /// <param name="breathsPerMinuteOption">
    /// The option (index) that specifies the amount of breaths per minute for the exercise.
    /// Available options can be retrieved using the <see cref="GetRelaxBreatheSettingsAsync"/> method.
    /// </param>
    /// <param name="volume">The volume used for the sound of the breathing exercises. Must be between 1 and 25.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetRelaxBreatheSettingsWithSoundAsync(int duration, int breathsPerMinuteOption, int volume, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the RelaxBreathe settings, using light for the breathing exercises.
    /// </summary>
    /// <param name="duration">How long the breathing exercises should run. Must be 5, 10 or 15 minutes.</param>
    /// <param name="breathsPerMinuteOption">
    /// The option (index) that specifies the amount of breaths per minute for the exercise.
    /// Available options can be retrieved using the <see cref="GetRelaxBreatheSettingsAsync"/> method.
    /// </param>
    /// <param name="lightIntensity">The intensity used for the light of the breathing exercises. Must be between 1 and 25.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when any of the supplied parameters are out of range.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    Task SetRelaxBreatheSettingsWithLightAsync(int duration, int breathsPerMinuteOption, int lightIntensity, CancellationToken cancellationToken = default);

    #endregion
}
