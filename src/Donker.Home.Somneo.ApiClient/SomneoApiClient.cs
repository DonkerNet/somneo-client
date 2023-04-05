using System.Net;
using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Mappers;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.ApiClient.Serialization;

namespace Donker.Home.Somneo.ApiClient;

/// <inheritdoc cref="ISomneoApiClient"/>
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

    /// <inheritdoc/>
    public Uri? BaseAddress => HttpClient.BaseAddress;

    /// <inheritdoc/>
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
    /// <param name="disposeHttpClient">Whether the HTTP client that is used should also be disposed when <see cref="Dispose()"/> is called.</param>
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
    /// <param name="hostname">The hostname that resolves to the Somneo device to connect with.</param>
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

    /// <inheritdoc/>
    public DeviceDetails GetDeviceDetails()
    {
        var dto = ExecuteGetRequest<DeviceDetailsDto>("di/v1/products/1/device");
        return DeviceDetailsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public WifiDetails GetWifiDetails()
    {
        var dto = ExecuteGetRequest<WifiDetailsDto>("di/v1/products/0/wifi");
        return WifiDetailsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public FirmwareDetails GetFirmwareDetails()
    {
        var dto = ExecuteGetRequest<FirmwareDetailsDto>("di/v1/products/0/firmware");
        return FirmwareDetailsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Locale GetLocale()
    {
        var dto = ExecuteGetRequest<LocaleDto>("di/v1/products/0/locale");
        return LocaleMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Time GetTime()
    {
        var dto = ExecuteGetRequest<TimeDto>("di/v1/products/0/time");
        return TimeMapper.ToModel(dto);
    }

    #endregion

    #region Somneo: Sensors

    /// <inheritdoc/>
    public SensorData GetSensorData()
    {
        var dto = ExecuteGetRequest<SensorDataDto>("di/v1/products/1/wusrd");
        return SensorDataMapper.ToModel(dto);
    }

    #endregion

    #region Somneo: Light

    /// <inheritdoc/>
    public LightState GetLightState()
    {
        var dto = ExecuteGetRequest<LightStateDto>("di/v1/products/1/wulgt");
        return LightStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public void ToggleLight(bool enabled)
    {
        object data = new
        {
            onoff = enabled,    // Toggle the light
            tempy = false,      // Specifies NOT to be in preview/temporary mode?
            ngtlt = false       // Disable the night light
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    /// <inheritdoc/>
    public void SetLightLevel(int lightLevel)
    {
        if (lightLevel < 1 || lightLevel > 25)
            throw new ArgumentException("The level must be between 1 and 25.", nameof(lightLevel));

        object data = new
        {
            ltlvl = lightLevel, // Set the level
            onoff = true,       // Enable the light
            tempy = false,      // Specifies NOT to be in preview/temporary mode?
            ngtlt = false       // Disable the night light
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    /// <inheritdoc/>
    public void ToggleNightLight(bool enabled)
    {
        object data = new
        {
            onoff = false,  // Disable the regular light
            tempy = false,  // Specifies NOT to be in preview/temporary mode?
            ngtlt = enabled // Enable the night light
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    #endregion

    #region Somneo: Display

    /// <inheritdoc/>
    public DisplayState GetDisplayState()
    {
        var dto = ExecuteGetRequest<DisplayStateDto>("di/v1/products/1/wusts");
        return DisplayStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public void TogglePermanentDisplay(bool enabled)
    {
        object data = new
        {
            dspon = enabled
        };

        ExecutePutRequest("di/v1/products/1/wusts", data);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public void EnableWakeUpSoundPreview(WakeUpSound wakeUpSound, int volume)
    {
        if (!Enum.IsDefined(wakeUpSound))
            throw new ArgumentException("The wake-up sound is invalid.", nameof(wakeUpSound));
        if (volume < 1 || volume > 25)
            throw new ArgumentException("The volume must be between 1 and 25.", nameof(volume));

        object data = new
        {
            sndss = 1000,   // What is this?
            onoff = true,   // Enable the player
            tempy = true,   // Specifies to be in preview/temporary mode?
            snddv = "wus",  // Set the player to wake-up sound
            sndch = EnumMapper.GetWakeUpSoundValue(wakeUpSound)!.Value.ToString(),
            sdvol = volume
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    /// <inheritdoc/>
    public void DisableWakeUpSoundPreview()
    {
        object data = new
        {
            onoff = false,  // Disable the player
            tempy = true,   // Specifies to be in preview/temporary mode?
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    #endregion

    #region Somneo: FM radio

    /// <inheritdoc/>
    public FMRadioPresets GetFMRadioPresets()
    {
        var dto = ExecuteGetRequest<FMRadioPresetsDto>("di/v1/products/1/wufmp/00");
        return FMRadioPresetsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public float GetFMRadioPreset(int position)
    {
        if (position < 1 || position > 5)
            throw new ArgumentException("The position must be between 1 and 5.", nameof(position));

        object data = new
        {
            fmcmd = "recall",
            prstn = position
        };

        var dto = ExecutePutRequest<FMRadioStateDto>("di/v1/products/1/wufmr", data);

        return dto.Frequency;
    }

    /// <inheritdoc/>
    public FMRadioState GetFMRadioState()
    {
        var dto = ExecuteGetRequest<FMRadioStateDto>("di/v1/products/1/wufmr");
        return FMRadioStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public void EnableFMRadio()
    {
        object data = new
        {
            sndss = 0,      // What is this?
            onoff = true,   // Enable the player
            tempy = false,  // Specifies NOT to be in preview/temporary mode?
            snddv = "fmr"   // Set the player to FM radio
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    /// <inheritdoc/>
    public void EnableFMRadioPreset(int preset)
    {
        if (preset < 1 || preset > 5)
            throw new ArgumentException("The preset must be between 1 and 5.", nameof(preset));

        object data = new
        {
            sndss = 0,                  // What is this?
            onoff = true,               // Enable the player
            tempy = false,              // Specifies NOT to be in preview/temporary mode?
            snddv = "fmr",              // Set the player to FM radio
            sndch = preset.ToString()   // Set the "channel" to the preset number
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    /// <inheritdoc/>
    public void SeekFMRadioStation(RadioSeekDirection direction)
    {
        if (!Enum.IsDefined(direction))
            throw new ArgumentException("The direction is invalid.", nameof(direction));

        object data = new
        {
            fmcmd = EnumMapper.GetRadioSeekDirectionValue(direction)
        };

        ExecutePutRequest("di/v1/products/1/wufmr", data);
    }

    #endregion

    #region Somneo: AUX

    /// <inheritdoc/>
    public void EnableAUX()
    {
        DisablePlayer(); // Disable the player first, because AUX does not enable right away for some reason

        object data = new
        {
            sndss = 0,      // What is this?
            onoff = true,   // Enable the player
            tempy = false,  // Specifies NOT to be in preview/temporary mode?
            snddv = "aux"   // Set the player to AUX
        };

        ExecutePutRequest("di/v1/products/1/wuply", data);
    }

    #endregion

    #region Somneo: Audio player

    /// <inheritdoc/>
    public PlayerState GetPlayerState()
    {
        var dto = ExecuteGetRequest<PlayerStateDto>("di/v1/products/1/wuply");
        return PlayerStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public IReadOnlyList<Alarm> GetAlarms()
    {
        AlarmStatesDto alarmStatesDto = ExecuteGetRequest<AlarmStatesDto>("di/v1/products/1/wualm/aenvs");
        AlarmSchedulesDto alarmSchedulesDto = ExecuteGetRequest<AlarmSchedulesDto>("di/v1/products/1/wualm/aalms");
        return AlarmMapper.ToModels(alarmStatesDto, alarmSchedulesDto);
    }

    /// <inheritdoc/>
    public void ToggleAlarm(int position, bool enabled)
    {
        if (position < 1 || position > 16)
            throw new ArgumentException("The position must be between 1 and 16.", nameof(position));

        object data = new
        {
            prfnr = position,
            prfen = enabled
        };

        ExecutePutRequest("di/v1/products/1/wualm/prfwu", data);
    }

    /// <inheritdoc/>
    public void SetAlarmWithWakeUpSound(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        WakeUpSound wakeUpSound, int volume)
    {
        SetAlarm(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            volume, SoundDeviceType.WakeUpSound, wakeUpSound, null);
    }

    /// <inheritdoc/>
    public void SetAlarmWithFMRadio(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int fmRadioPreset, int volume)
    {
        SetAlarm(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            volume, SoundDeviceType.FMRadio, null, fmRadioPreset);
    }

    /// <inheritdoc/>
    public void SetAlarmWithoutSound(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme sunriseColors, int sunriseIntensity, int sunriseDuration)
    {
        SetAlarm(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            null, null, null, null);
    }

    private void SetAlarm(int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek>? repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int? volume, SoundDeviceType? soundDevice, WakeUpSound? wakeUpSound, int? fmRadioPreset)
    {
        if (position < 1 || position > 16)
            throw new ArgumentException("The position must be between 1 and 16.", nameof(position));
        if (hour < 0 || hour > 23)
            throw new ArgumentException("The hour must be between 0 and 23.", nameof(hour));
        if (minute < 0 || minute > 59)
            throw new ArgumentException("The minute must be between 0 and 23.", nameof(minute));
        if (repeatDays != null && repeatDays.Any(rd => !Enum.IsDefined(rd)))
            throw new ArgumentException("One or more repeat days are invalid.", nameof(repeatDays));
        if (volume.HasValue && (volume < 1 || volume > 25))
            throw new ArgumentException("The volume must be between 1 and 25.", nameof(volume));
        if (fmRadioPreset.HasValue && (fmRadioPreset < 1 || fmRadioPreset > 5))
            throw new ArgumentException("The FM radio preset must be between 1 and 5.", nameof(fmRadioPreset));
        if (wakeUpSound.HasValue && !Enum.IsDefined(wakeUpSound.Value))
            throw new ArgumentException("The wake-up sound is invalid.", nameof(wakeUpSound));

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

        int sunriseColorSchemeNumber = 0;
        int definitiveSunriseIntensity = 0;
        int definitiveSunriseDuration = 0;

        if (sunriseColors.HasValue)
        {
            if (!Enum.IsDefined(sunriseColors.Value))
                throw new ArgumentException("The sunrise color scheme is invalid.", nameof(sunriseColors));
            if (!sunriseIntensity.HasValue || sunriseIntensity.Value < 1 || sunriseIntensity.Value > 25)
                throw new ArgumentException("When the sunrise colors are set, the intensity must be between 1 and 25.", nameof(sunriseIntensity));
            if (!sunriseDuration.HasValue || sunriseDuration.Value < 5 || sunriseDuration.Value > 40 || sunriseDuration.Value % 5 != 0)
                throw new ArgumentException("When the sunrise colors are set, the duration must be between 5 and 40 minutes, with 5 minute steps in between.", nameof(sunriseDuration));
            sunriseColorSchemeNumber = EnumMapper.GetColorSchemeValue(sunriseColors)!.Value;
            definitiveSunriseIntensity = sunriseIntensity.Value;
            definitiveSunriseDuration = sunriseDuration.Value;
        }

        int soundChannel = -1;

        switch (soundDevice)
        {
            case SoundDeviceType.WakeUpSound:
                soundChannel = EnumMapper.GetWakeUpSoundValue(wakeUpSound)!.Value;
                break;

            case SoundDeviceType.FMRadio:
                soundChannel = fmRadioPreset!.Value;
                break;
        }

        byte repeatDaysNumber = EnumMapper.GetDaysOfWeekValue(repeatDays);
        string soundDeviceName = EnumMapper.GetSoundDeviceTypeValue(soundDevice);

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
            ctype = sunriseColorSchemeNumber,   // The sunrise
            curve = definitiveSunriseIntensity, // The light level
            durat = definitiveSunriseDuration,  // The sunrise duration
            daynm = repeatDaysNumber,           // The days on which to repeat the alarm
            snddv = soundDeviceName,            // The sound device to play
            sndch = soundChannel.ToString(),    // The wake-up sound or FM radio preset to play
            sndlv = volume ?? 12                // The volume level of the sound device to play
        };

        ExecutePutRequest("di/v1/products/1/wualm/prfwu", data);
    }

    /// <inheritdoc/>
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
            ctype = 0,          // Set the default sunrise
            curve = 20,         // Set the default light level
            durat = 30,         // Set the default sunrise duration
            daynm = 0,          // Set the default to never repeat
            snddv = "wus",      // Set the default sound device to wake-up sound
            sndch = "1",        // Set the default wake-up sound,
            sndlv = 12          // Set the default volume
        };

        ExecutePutRequest("di/v1/products/1/wualm/prfwu", data);
    }

    /// <inheritdoc/>
    public AlarmSettings? GetAlarmSettings(int position)
    {
        if (position < 1 || position > 16)
            throw new ArgumentException("The position must be between 1 and 16.", nameof(position));

        object data = new
        {
            prfnr = position
        };

        var dto = ExecutePutRequest<AlarmSettingsDto>("di/v1/products/1/wualm", data);

        return dto.IsSet ? AlarmSettingsMapper.ToModel(dto) : null;
    }

    /// <inheritdoc/>
    public void SetSnoozeTime(int minutes)
    {
        if (minutes < 1 || minutes > 20)
            throw new ArgumentException("The minutes must be between 1 and 20.", nameof(minutes));

        object data = new
        {
            snztm = minutes
        };

        ExecutePutRequest("di/v1/products/1/wualm", data);
    }

    #endregion

    #region Somneo: Timer

    /// <inheritdoc/>
    public TimerState GetTimerState()
    {
        var dto = ExecuteGetRequest<TimerStateDto>("di/v1/products/1/wutmr");
        return TimerStateMapper.ToModel(dto);
    }

    #endregion

    #region Somneo: Sunrise

    /// <inheritdoc/>
    public void EnableSunrisePreview(ColorScheme sunriseColors, int sunriseIntensity)
    {
        if (!Enum.IsDefined(sunriseColors))
            throw new ArgumentException("The sunrise color scheme is invalid.", nameof(sunriseColors));
        if (sunriseIntensity < 1 || sunriseIntensity > 25)
            throw new ArgumentException("The sunrise intensity must be between 1 and 25.", nameof(sunriseIntensity));

        object data = new
        {
            onoff = true,   // Enable the light
            tempy = true,   // Specifies to be in preview/temporary mode?
            ngtlt = false,  // Disable the night light
            ctype = EnumMapper.GetColorSchemeValue(sunriseColors)!.Value,
            ltlvl = sunriseIntensity
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    /// <inheritdoc/>
    public void DisableSunrisePreview()
    {
        object data = new
        {
            onoff = false,  // Disable the light
            tempy = true,   // Specifies to be in preview/temporary mode?
        };

        ExecutePutRequest("di/v1/products/1/wulgt", data);
    }

    #endregion

    #region Somneo: Sunset

    /// <inheritdoc/>
    public SunsetSettings GetSunsetSettings()
    {
        var dto = ExecuteGetRequest<SunsetSettingsDto>("di/v1/products/1/wudsk");
        return SunsetSettingsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="Dispose()"/>
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
