using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.ApiClient.Serialization;

namespace Donker.Home.Somneo.ApiClient;

/// <summary>
/// Client that provides communication with the Philips Somneo API.
/// </summary>
public sealed class SomneoApiClient : ISomneoApiClient, IDisposable
{
    private readonly SomneoApiSerializer _serializer = new();
    private readonly HttpClient _httpClient;
    private readonly bool _disposeHttpClient;

    private bool _disposed;
    
    private HttpClient HttpClient
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _httpClient;
        }
    }

    #region Public properties

    /// <summary>
    /// Gets the base address used for making requests to the Somneo device.
    /// </summary>
    public Uri? BaseAddress => HttpClient.BaseAddress;

    /// <summary>
    /// Gets or sets the maximum request timeout in milliseconds.
    /// </summary>
    public TimeSpan Timeout
    {
        get => HttpClient.Timeout;
        set => HttpClient.Timeout = value > TimeSpan.Zero ? value : TimeSpan.Zero;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SomneoApiClient"/> using a custom HTTP client.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for making requests to the Somneo device.</param>
    /// <param name="disposeHttpClient">Whether the HTTP client that is used should also be disposed when <see cref="SomneoApiClient.Dispose"/> is called.</param>
    /// <exception cref="ArgumentNullException">The HTTP client is null.</exception>
    public SomneoApiClient(HttpClient httpClient, bool disposeHttpClient)
    {
        if (httpClient == null)
            ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));

        _httpClient = httpClient;
        _disposeHttpClient = disposeHttpClient;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SomneoApiClient"/> using the specified hostname.
    /// </summary>
    /// <param name="host">The hostname that resolves to the Somneo device to connect with.</param>
    /// <exception cref="ArgumentNullException">The hostname is null.</exception>
    /// <exception cref="ArgumentException">The hostname is empty.</exception>
    /// <exception cref="UriFormatException">The hostname cannot be converted to a valid URI.</exception>
    public SomneoApiClient(string hostname)
    {
        ArgumentException.ThrowIfNullOrEmpty(hostname, nameof(hostname));

        var parsedBaseAddress = new Uri($"https://{hostname}", UriKind.Absolute);

        _httpClient = CreateHttpClient(parsedBaseAddress);
        _disposeHttpClient = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SomneoApiClient"/> using the specified base address.
    /// </summary>
    /// <param name="baseAddress">The base address of the Somneo device to use when making requests.</param>
    /// <exception cref="ArgumentNullException">The base address is null.</exception>
    public SomneoApiClient(Uri baseAddress)
    {
        ArgumentNullException.ThrowIfNull(baseAddress, nameof(baseAddress));

        _httpClient = CreateHttpClient(baseAddress);
        _disposeHttpClient = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SomneoApiClient"/> using the specified IP address as host.
    /// </summary>
    /// <param name="ipAddress">The IP address of the Somneo device to connect with.</param>
    /// <exception cref="ArgumentNullException">The IP address is null.</exception>
    public SomneoApiClient(IPAddress ipAddress)
    {
        ArgumentNullException.ThrowIfNull(ipAddress, nameof(ipAddress));

        var parsedBaseAddress = new Uri($"https://{ipAddress}", UriKind.Absolute);

        _httpClient = CreateHttpClient(parsedBaseAddress);
        _disposeHttpClient = true;
    }

    private static HttpClient CreateHttpClient(Uri baseAddress)
    {
        var handler = new HttpClientHandler
        {
            // Ignore SSL errors, as the Somneo device uses a self signed certificate
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };

        return new HttpClient(handler, true)
        {
            BaseAddress = baseAddress
        };
    }

    #endregion

    #region Somneo: General

    /// <summary>
    /// Retrieves details about the Somneo device itself.
    /// </summary>
    /// <returns>The details of the device as a <see cref="DeviceDetails"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public DeviceDetails GetDeviceDetails() => ExecuteGetRequest<DeviceDetails>("di/v1/products/1/device");

    /// <summary>
    /// Retrieves details about the Somneo's wifi connection.
    /// </summary>
    /// <returns>The details of the wifi connection as a <see cref="WifiDetails"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public WifiDetails GetWifiDetails() => ExecuteGetRequest<WifiDetails>("di/v1/products/0/wifi");

    /// <summary>
    /// Retrieves details about the Somneo's firmware.
    /// </summary>
    /// <returns>The firmware details as a <see cref="FirmwareDetails"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public FirmwareDetails GetFirmwareDetails() => ExecuteGetRequest<FirmwareDetails>("di/v1/products/0/firmware");

    /// <summary>
    /// Retrieves details about the locale set for the Somneo device.
    /// </summary>
    /// <returns>The locale details as a <see cref="Locale"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public Locale GetLocale() => ExecuteGetRequest<Locale>("di/v1/products/0/locale");

    /// <summary>
    /// Retrieves details about the time set for the Somneo device.
    /// </summary>
    /// <returns>The time details as a <see cref="Time"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public Time GetTime() => ExecuteGetRequest<Time>("di/v1/products/0/time");

    #endregion

    #region Somneo: Sensors

    /// <summary>
    /// Retrieves the Somneo's sensor data, containing the temperature, light level, sound level and humidity.
    /// </summary>
    /// <returns>The sensor data as a <see cref="SensorData"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public SensorData GetSensorData() => ExecuteGetRequest<SensorData>("di/v1/products/1/wusrd");

    #endregion

    #region Somneo: Light

    /// <summary>
    /// Retrieves the current light state.
    /// </summary>
    /// <returns>The light state as a <see cref="LightState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public LightState GetLightState() => ExecuteGetRequest<LightState>("di/v1/products/1/wulgt");

    /// <summary>
    /// Toggles the normal light.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the light.</param>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void ToggleLight(bool enabled)
    {
        object data = new
        {
            onoff = enabled,    // Toggle the light
            tempy = false,      // Disable sunrise preview
            ngtlt = false       // Disable the night light
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    /// <summary>
    /// Sets the level of the normal light and enables the light as well.
    /// </summary>
    /// <param name="lightLevel">The light level to set. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="lightLevel"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SetLightLevel(int lightLevel)
    {
        if (lightLevel < 1 || lightLevel > 25)
            throw new ArgumentException("The level must be between 1 and 25.", nameof(lightLevel));

        object data = new
        {
            ltlvl = lightLevel, // Set the level
            onoff = true,       // Enable the light
            tempy = false,      // Disable sunrise preview
            ngtlt = false       // Disable the night light
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    /// <summary>
    /// Toggles the night light.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the night light.</param>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void ToggleNightLight(bool enabled)
    {
        object data = new
        {
            onoff = false,  // Disable the regular light
            tempy = false,  // Disable sunrise preview
            ngtlt = enabled // Enable the night light
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    /// <summary>
    /// Toggles the sunrise preview mode.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the sunrise preview.</param>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void ToggleSunrisePreview(bool enabled)
    {
        object data = new
        {
            onoff = enabled,    // Enable the light
            tempy = enabled,    // Enable sunrise preview
            ngtlt = false       // Disable the night light
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    #endregion

    #region Somneo: Display

    /// <summary>
    /// Retrieves the current state of the display.
    /// </summary>
    /// <returns>The display state as a <see cref="DisplayState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public DisplayState GetDisplayState() => ExecuteGetRequest<DisplayState>("di/v1/products/1/wusts");

    /// <summary>
    /// Toggles whether the display should always be shown or if it should disable automatically after a period of time.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the display permanently.</param>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void TogglePermanentDisplay(bool enabled)
    {
        object data = new
        {
            dspon = enabled
        };

        ExecutePutRequest("di/v1/products/1/wusts", data);
    }

    /// <summary>
    /// Sets the brightness level of the display.
    /// </summary>
    /// <param name="brightnessLevel">The brightness level to set. Value must be between 1 and 6.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="brightnessLevel"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SetDisplayLevel(int brightnessLevel)
    {
        if (brightnessLevel < 1 || brightnessLevel > 6)
            throw new ArgumentException("The level must be between 1 and 6.", nameof(brightnessLevel));

        object data = new
        {
            brght = brightnessLevel
        };

        ExecutePutRequest("di/v1/products/1/wusts", data);
    }

    #endregion

    #region Somneo: Wake-up sounds

    /// <summary>
    /// Plays a wake-up sound.
    /// </summary>
    /// <param name="wakeUpSound">The wake-up sound to play.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="wakeUpSound"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void PlayWakeUpSound(WakeUpSound wakeUpSound)
    {
        if (!Enum.IsDefined(wakeUpSound))
            throw new ArgumentException("The wake-up sound is invalid.", nameof(wakeUpSound));

        object data = new
        {
            sndss = 1000,   // What is this?
            onoff = true,   // Enable the player
            tempy = true,   // ?
            snddv = "wus",  // Set the player to wake-up sound
            sndch = ((int)wakeUpSound).ToString()
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    #endregion

    #region Somneo: FM radio

    /// <summary>
    /// Retrieves the configured presets of FM radio frequencies.
    /// </summary>
    /// <returns>The FM radio presets ad a <see cref="FMRadioPresets"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public FMRadioPresets GetFMRadioPresets() => ExecuteGetRequest<FMRadioPresets>("di/v1/products/1/wufmp/00");

    /// <summary>
    /// Sets the preset of the specified position to the specified FM frequency.
    /// </summary>
    /// <param name="position">The preset position. Value must be between 1 and 5.</param>
    /// <param name="frequency">The FM frequency. Value must be within the range of 87.50 to 107.99.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> or <paramref name="frequency"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SetFMRadioPreset(int position, float frequency)
    {
        if (position < 1 || position > 5)
            throw new ArgumentException("The position must be between 1 and 5.", nameof(position));
        if (frequency < 87.50F || frequency > 107.99F)
            throw new ArgumentException("The frequency must be within the range of 87.50 to 107.99.", nameof(frequency));

        object data = new
        {
            fmfrq = frequency.ToString("0.00", NumberFormatInfo.InvariantInfo),
            fmcmd = "set",
            prstn = position
        };

        ExecutePutRequest("di/v1/products/1/wufmr", data);
    }

    /// <summary>
    /// Gets the FM frequency of a preset with the specified position.
    /// </summary>
    /// <param name="position">The preset position. Value must be between 1 and 5.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public float GetFMRadioPreset(int position)
    {
        if (position < 1 || position > 5)
            throw new ArgumentException("The position must be between 1 and 5.", nameof(position));

        object data = new
        {
            fmcmd = "recall",
            prstn = position
        };

        var result = ExecutePutRequest<Dictionary<string, object>>("di/v1/products/1/wufmr", data);

        if (result.TryGetValue("fmfrq", out object? frequencyObj)
            && frequencyObj != null
            && float.TryParse(frequencyObj.ToString(), NumberStyles.None, NumberFormatInfo.InvariantInfo, out float frequency))
            return frequency;

        return default;
    }

    /// <summary>
    /// Retrieves the state of the FM radio.
    /// </summary>
    /// <returns>The FM radio state as an <see cref="FMRadioState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public FMRadioState GetFMRadioState() => ExecuteGetRequest<FMRadioState>("di/v1/products/1/wufmr");

    /// <summary>
    /// Enables the FM radio.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void EnableFMRadio()
    {
        object data = new
        {
            sndss = 0,      // What is this?
            onoff = true,   // Enable the player
            tempy = false,  // ?
            snddv = "fmr"   // Set the player to FM radio
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    /// <summary>
    /// Enables the FM radio for the specified preset.
    /// </summary>
    /// <param name="preset">The preset. Value must be between 1 and 5.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="preset"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void EnableFMRadioPreset(int preset)
    {
        if (preset < 1 || preset > 5)
            throw new ArgumentException("The preset must be between 1 and 5.", nameof(preset));

        object data = new
        {
            sndss = 0,                  // What is this?
            onoff = true,               // Enable the player
            tempy = false,              // Disables the sunrise preview?
            snddv = "fmr",              // Set the player to FM radio
            sndch = preset.ToString()   // Set the "channel" to the preset number
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    /// <summary>
    /// Seeks a new FM radio station in the specified direction for the currently selected preset, if the FM radio is enabled.
    /// </summary>
    /// <param name="direction">The seek direction.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="direction"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SeekFMRadioStation(RadioSeekDirection direction)
    {
        if (!Enum.IsDefined(direction))
            throw new ArgumentException("The direction is invalid.", nameof(direction));

        object data = new
        {
            fmcmd = direction == RadioSeekDirection.Up ? "seekup" : "seekdown"
        };

        ExecutePutRequest("di/v1/products/1/wufmr", data);
    }

    #endregion

    #region Somneo: AUX

    /// <summary>
    /// Enables the auxiliary input device.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void EnableAUX()
    {
        object data = new
        {
            sndss = 0,      // What is this?
            onoff = true,   // Enable the player
            tempy = false,  // ?
            snddv = "aux"   // Set the player to AUX
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    #endregion

    #region Somneo: Audio player

    /// <summary>
    /// Retrieves the state of the audio player.
    /// </summary>
    /// <returns>The audio player state as a <see cref="PlayerState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public PlayerState GetPlayerState() => ExecuteGetRequest<PlayerState>("di/v1/products/1/wuply");

    /// <summary>
    /// Sets the volume of the audio player.
    /// </summary>
    /// <param name="position">The volume. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="volume"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SetPlayerVolume(int volume)
    {
        if (volume < 1 || volume > 25)
            throw new ArgumentException("The volume must be between 1 and 25.", nameof(volume));

        object data = new
        {
            sdvol = volume
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    /// <summary>
    /// Disables the audio player.
    /// </summary>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void DisablePlayer()
    {
        object data = new
        {
            onoff = false
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    #endregion

    #region Somneo: Alarms

    /// <summary>
    /// Retrieves the alarms.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyList{T}"/> containing <see cref="Alarm"/> objects.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public IReadOnlyList<Alarm> GetAlarms()
    {
        AlarmStates alarmStates = ExecuteGetRequest<AlarmStates>("di/v1/products/1/wualm/aenvs");
        AlarmSchedules alarmSchedules = ExecuteGetRequest<AlarmSchedules>("di/v1/products/1/wualm/aalms");

        int alarmCount = alarmStates.Set.Length;
        var alarms = new List<Alarm>(alarmCount);

        for (int i = 0; i < alarmCount; ++i)
        {
            if (!alarmStates.Set[i])
                continue;

            int powerWakeIndex = i * 3;

            bool enabled = alarmStates.Enabled[i];
            bool powerWakeEnabled = alarmStates.PowerWake[powerWakeIndex] == 255;
            int? powerWakeHour = powerWakeEnabled ? alarmStates.PowerWake[powerWakeIndex + 1] : null;
            int? powerWakeMinute = powerWakeEnabled ? alarmStates.PowerWake[powerWakeIndex + 2] : null;

            var repeatDays = EnumHelper.DayFlagsToDaysOfWeek(alarmSchedules.RepeatDayFlags[i]).ToList();

            int hour = alarmSchedules.Hours[i];
            int minute = alarmSchedules.Minutes[i];

            var alarm = new Alarm
            {
                Position = i + 1,
                Enabled = enabled,
                Hour = hour,
                Minute = minute,
                RepeatDays = new ReadOnlyCollection<DayOfWeek>(repeatDays),
                PowerWakeEnabled = powerWakeEnabled,
                PowerWakeHour = powerWakeHour,
                PowerWakeMinute = powerWakeMinute
            };

            alarms.Add(alarm);
        }

        return new ReadOnlyCollection<Alarm>(alarms);
    }

    /// <summary>
    /// Toggles an alarm by it's position in the alarm collection. If the alarm does not exist yet, it will be added with default settings for that position.
    /// </summary>
    /// <param name="position">The position of the alarm to toggle. Value must be between 1 and 16.</param>
    /// <param name="enabled">Whether to enable or disable the alarm.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void ToggleAlarm(int position, bool enabled)
    {
        if (position < 1 || position > 16)
            throw new ArgumentException("The position must be between 1 and 16.", nameof(position));

        object data;

        if (enabled)
        {
            data = new
            {
                prfnr = position,
                prfvs = true,       // Make sure the alarm is set for the specified position
                prfen = true        // Enable the alarm
            };
        }
        else
        {
            data = new
            {
                prfnr = position,
                prfen = false       // Disable the alarm
            };
        }

        ExecutePutRequest("di/v1/products/1/wualm/prfwu", data);
    }

    /// <summary>
    /// Sets and enables an alarm with a wake-up sound at the specified position in the alarm list and configures it with the specified settings.
    /// </summary>
    /// <param name="position">The position of the alarm to set. Value must be between 1 and 16.</param>
    /// <param name="hour">The hour of the alarm to set. Value must be between 0 and 23.</param>
    /// <param name="minute">The minute of the alarm to set. Value must be between 0 and 59.</param>
    /// <param name="powerWakeMinutes">Sets the amount of minutes when the PowerWake should start after the alarm is triggered. Optional. Value must be between 0 and 59.</param>
    /// <param name="repeatDays">The days on which to repeat the alarm. Optional.</param>
    /// <param name="sunriseColors">The type of sunrise to show when the alarm is triggered.</param>
    /// <param name="sunriseIntensity">
    /// The intensity of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set to something other than <see cref="ColorScheme.NoLight"/>.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunriseDuration">
    /// The duration of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set to something other than <see cref="ColorScheme.NoLight"/>.
    /// Value must be between 5 and 40, with 5 minute steps in between.
    /// </param>
    /// <param name="wakeUpSound">The wake-up sound to play when the alarm is triggered.</param>
    /// <param name="volume">The volume of the wake-up sound that is played. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SetAlarmWithWakeUpSound(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        WakeUpSound wakeUpSound, int volume)
    {
        if (!Enum.IsDefined(wakeUpSound))
            throw new ArgumentException("The wake-up sound is invalid.", nameof(wakeUpSound));

        SetAlarm(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            volume, SoundDeviceType.WakeUpSound, wakeUpSound, null);
    }

    /// <summary>
    /// Sets and enables an alarm with FM radio at the specified position in the alarm list and configures it with the specified settings.
    /// </summary>
    /// <param name="position">The position of the alarm to set. Value must be between 1 and 16.</param>
    /// <param name="hour">The hour of the alarm to set. Value must be between 0 and 23.</param>
    /// <param name="minute">The minute of the alarm to set. Value must be between 0 and 59.</param>
    /// <param name="powerWakeMinutes">Sets the amount of minutes when the PowerWake should start after the alarm is triggered. Optional. Value must be between 0 and 59.</param>
    /// <param name="repeatDays">The days on which to repeat the alarm. Optional.</param>
    /// <param name="sunriseColors">The type of sunrise colors to show when the alarm is triggered.</param>
    /// <param name="sunriseIntensity">
    /// The intensity of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set to something other than <see cref="ColorScheme.NoLight"/>.
    /// Value must be between 1 and 25.
    /// </param>
    /// <param name="sunriseDuration">
    /// The duration of the sunrise to show when the alarm is triggered.
    /// Optional, but required when <paramref name="sunriseColors"/> is set to something other than <see cref="ColorScheme.NoLight"/>.
    /// Value must be between 5 and 40, with 5 minute steps in between.
    /// </param>
    /// <param name="fmRadioPreset">The preset with the FM frequency of the channel to play when the alarm is triggered. Value must be between 1 and 5.</param>
    /// <param name="volume">The volume of the FM radio that is played. Value must be between 1 and 25.</param>
    /// <exception cref="ArgumentException">Exception thrown when any of the supplied parameters are invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SetAlarmWithFMRadio(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int fmRadioPreset, int volume)
    {
        if (fmRadioPreset < 1 || fmRadioPreset > 5)
            throw new ArgumentException("The FM radio preset must be between 1 and 5.", nameof(fmRadioPreset));

        SetAlarm(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            volume, SoundDeviceType.FMRadio, null, fmRadioPreset);
    }

    private void SetAlarm(int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek>? repeatDays,
        ColorScheme sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int volume,
        // Supplied by overloads:
        SoundDeviceType soundDevice, WakeUpSound? wakeUpSound, int? fmRadioPreset)
    {
        if (position < 1 || position > 16)
            throw new ArgumentException("The position must be between 1 and 16.", nameof(position));
        if (hour < 0 || hour > 23)
            throw new ArgumentException("The hour must be between 0 and 23.", nameof(hour));
        if (minute < 0 || minute > 59)
            throw new ArgumentException("The minute must be between 0 and 23.", nameof(minute));
        if (volume < 1 || volume > 25)
            throw new ArgumentException("The volume must be between 1 and 25.", nameof(volume));
        if (!Enum.IsDefined(sunriseColors))
            throw new ArgumentException("The sunrise color scheme is invalid.", nameof(sunriseColors));
        if (repeatDays != null && repeatDays.Any(rd => !Enum.IsDefined(rd)))
            throw new ArgumentException("One or more repeat days are invalid.", nameof(repeatDays));

        int powerWakeSize = 0;
        int definitivePowerWakeHour = 0;
        int definitivePowerWakeMinute = 0;

        if (powerWakeMinutes.HasValue)
        {
            if (powerWakeMinutes.Value < 0 || powerWakeMinutes.Value > 59)
                throw new ArgumentException("The PowerWake minutes must be between 0 and 59.", nameof(powerWakeMinutes));

            TimeSpan powerWakeTime = new TimeSpan(hour, minute, 0)
                .Add(TimeSpan.FromMinutes(powerWakeMinutes.Value));

            powerWakeSize = 255;
            definitivePowerWakeHour = powerWakeTime.Hours;
            definitivePowerWakeMinute = powerWakeTime.Minutes;
        }

        int definitiveSunriseIntensity = 0;
        int definitiveSunriseDuration = 0;

        if (sunriseColors != ColorScheme.NoLight)
        {
            if (!sunriseIntensity.HasValue || sunriseIntensity.Value < 1 || sunriseIntensity.Value > 25)
                throw new ArgumentException("When the sunrise colors are set to something other than no light, the intensity must be between 1 and 25.", nameof(sunriseIntensity));
            if (!sunriseDuration.HasValue || sunriseDuration.Value < 5 || sunriseDuration.Value > 40 || sunriseDuration.Value % 5 != 0)
                throw new ArgumentException("When the sunrise colors are set to something other than no light, the duration must be between 5 and 40 minutes, with 5 minute steps in between.", nameof(sunriseDuration));
            definitiveSunriseIntensity = sunriseIntensity.Value;
            definitiveSunriseDuration = sunriseDuration.Value;
        }

        int soundChannel = 0;

        switch (soundDevice)
        {
            case SoundDeviceType.WakeUpSound:
                soundChannel = (int)wakeUpSound.GetValueOrDefault();
                break;

            case SoundDeviceType.FMRadio:
                soundChannel = fmRadioPreset.GetValueOrDefault();
                break;
        }

        byte repeatDaysNumber = (byte)EnumHelper.DaysOfWeekToDayFlags(repeatDays);
        int sunruseColorSchemNumber = EnumHelper.GetColorSchemeNumber(sunriseColors);
        string soundDeviceName = EnumHelper.GetEnumMemberValue(soundDevice)!;

        object data = new
        {
            prfnr = position,                   // Position of the alarm to set
            prfen = true,                       // Enable the alarm
            prfvs = true,                       // Add the alarm to the set alarms list
            almhr = hour,                       // The alarm hour
            almmn = minute,                     // The alarm minute
            pwrsz = powerWakeSize,              // The PowerWake to enabled (255) or disabled (0)
            pszhr = definitivePowerWakeHour,    // The PowerWake hour
            pszmn = definitivePowerWakeMinute,  // The PowerWake minute
            ctype = sunruseColorSchemNumber,    // The sunrise ("Sunny day" if curve > 0 or "No light" if curve == 0)
            curve = definitiveSunriseIntensity, // The light level
            durat = definitiveSunriseDuration,  // The sunrise duration
            daynm = repeatDaysNumber,           // The days on which to repeat the alarm
            snddv = soundDeviceName,            // The sound device to play
            sndch = soundChannel.ToString(),    // The wake-up sound or FM radio preset to play
            sndlv = volume                      // The volume level of the sound device to play
        };

        ExecutePutRequest("di/v1/products/1/wualm/prfwu", data);
    }

    /// <summary>
    /// Removes an alarm by it's position in the alarm list and restores the default settings for that position. Removal will fail when only two alarms are left.
    /// </summary>
    /// <param name="position">The position of the alarm to remove. Value must be between 1 and 16.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void RemoveAlarm(int position)
    {
        if (position < 1 || position > 16)
            throw new ArgumentException("The position must be between 1 and 16.", nameof(position));

        object data = new
        {
            prfnr = position,   // Position of the alarm to remove
            prfen = false,      // Disable the alarm
            prfvs = false,      // Remove the alarm from the set alarms list
            almhr = 7,          // Set the default alarm hour
            almmn = 30,         // Set the default alarm minute
            pwrsz = 0,          // Disable the PowerWake
            pszhr = 0,          // Set the default PowerWake hour
            pszmn = 0,          // Set the default PowerWake minute
            ctype = 0,          // Set the default sunrise ("Sunny day" if curve > 0 or "No light" if curve == 0)
            curve = 20,         // Set the default light level
            durat = 30,         // Set the default sunrise duration
            daynm = 254,        // Set the default to repeat every day
            snddv = "wus",      // Set the default sound device to wakeup sound
            snztm = 0           // Set the default snooze time
        };

        ExecutePutRequest("di/v1/products/1/wualm/prfwu", data);
    }

    /// <summary>
    /// Gets the settings of an alarm by it's position in the alarm list.
    /// </summary>
    /// <param name="position">The position of the alarm to retrieve the settings for. Value must be between 1 and 16.</param>
    /// <returns>The settings as an <see cref="AlarmSettings"/> object if the alarm is set; otherwise, <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="position"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public AlarmSettings? GetAlarmSettings(int position)
    {
        if (position < 1 || position > 16)
            throw new ArgumentException("The position must be between 1 and 16.", nameof(position));

        object data = new
        {
            prfnr = position
        };

        var alarmSettings = ExecutePutRequest<AlarmSettings>("di/v1/products/1/wualm", data);

        return alarmSettings.IsSet ? alarmSettings : null;
    }

    /// <summary>
    /// Sets the snooze time in minutes for all alarms.
    /// </summary>
    /// <param name="minutes">The snooze time in minutes. Value must be between 1 and 20.</param>
    /// <exception cref="ArgumentException">Exception thrown when the <paramref name="minutes"/> parameter is invalid.</exception>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void SetSnoozeTime(int minutes)
    {
        if (minutes < 1 || minutes > 16)
            throw new ArgumentException("The minutes must be between 1 and 20.", nameof(minutes));

        object data = new
        {
            snztm = minutes,

            // Don't specify any alarms to update
            aalms = Array.Empty<object>(),
            aenvs = Array.Empty<object>(),
            prfsh = 0
        };

        ExecutePutRequest("di/v1/products/1/wualm", data);
    }

    #endregion

    #region Somneo: Timer

    /// <summary>
    /// Gets the current state of the Somneo's timer, used for the RelaxBreathe and sunset functions.
    /// </summary>
    /// <returns>The timer state as a <see cref="TimerState"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public TimerState GetTimerState() => ExecuteGetRequest<TimerState>("di/v1/products/1/wutmr");

    #endregion

    #region Somneo: Sunset

    /// <summary>
    /// Gets the settings of the Sunset function.
    /// </summary>
    /// <returns>The settings as a <see cref="SunsetSettings"/> object.</returns>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public SunsetSettings GetSunsetSettings() => ExecuteGetRequest<SunsetSettings>("di/v1/products/1/wudsk");


    /// <summary>
    /// Toggles the Sunset function.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the sunset.</param>
    /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
    public void ToggleSunset(bool enabled)
    {
        object data = new
        {
            onoff = enabled
        };

        ExecutePutRequest("di/v1/products/1/wudsk", data);
    }

    #endregion

    #region HTTP requests

    private T ExecuteGetRequest<T>(string resource) => ExecuteRequest<T>(resource, HttpMethod.Get, null);

    private void ExecutePutRequest(string resource, object? data) => ExecuteRequest(resource, HttpMethod.Put, data);

    private T ExecutePutRequest<T>(string resource, object? data) => ExecuteRequest<T>(resource, HttpMethod.Put, data);

    private void ExecuteRequest(string resource, HttpMethod method, object? data)
    {
        using var request = CreateRequest(resource, method, data);
        using var response = ExecuteRequest(request);
    }

    private T ExecuteRequest<T>(string resource, HttpMethod method, object? data)
    {
        using var request = CreateRequest(resource, method, data);
        using var response = ExecuteRequest(request);
        var responseData = _serializer.ReadHttpContent<T>(response.Content);
        return responseData ?? throw new SomneoApiException("The Somneo returned an empty response.");
    }

    private HttpRequestMessage CreateRequest(string resource, HttpMethod method, object? data)
    {
        var request = new HttpRequestMessage(method, resource);

        if (data != null)
            request.Content = _serializer.CreateHttpContent(data);

        return request;
    }

    private HttpResponseMessage ExecuteRequest(HttpRequestMessage request)
    {
        HttpResponseMessage response;

        try
        {
            response = HttpClient.Send(request);
        }
        catch (Exception ex)
        {
            throw new SomneoApiException("Failed to execute the Somneo request.", ex);
        }

        ValidateResponse(response);

        return response;
    }

    private static void ValidateResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        using var contentStream = response.Content.ReadAsStream();
        using var streamReader = new StreamReader(contentStream);
        
        string content = streamReader.ReadToEnd();

        throw new SomneoApiException($"The Somneo returned a response with status code {(int)response.StatusCode}.", response.StatusCode, content);
    }

    #endregion

    #region Disposing

    /// <summary>
    /// Disposes this <see cref="SomneoApiClient"/> instance and it's inner <see cref="System.Net.Http.HttpClient"/> if allowed.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~SomneoApiClient()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        _disposed = true;

        if (disposing)
        {
            if (_disposeHttpClient)
                _httpClient.Dispose();
        }
    }

    #endregion
}
