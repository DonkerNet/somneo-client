using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Mappers;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.ApiClient.Serialization;
using Donker.Home.Somneo.ApiClient.Validation;
using System.Net;

namespace Donker.Home.Somneo.ApiClient;

/// <inheritdoc cref="ISomneoApiClient"/>
public sealed class SomneoApiClient : ISomneoApiClient, IDisposable
{
    private readonly SomneoApiSerializer _serializer = new();
    private readonly bool _disposeHttpClient;

    // Somneo does not work well with multiple concurrent HTTP requests,
    // so we use a semaphore to limit the amount of requests made by this client to 1 at a time
    private SemaphoreSlim? _semaphore = new(1);

    private HttpClient? _httpClient;
    private bool _disposed;

    private SemaphoreSlim Semaphore
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _semaphore!;
        }
    }

    private HttpClient HttpClient
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _httpClient!;
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
    public async Task<DeviceDetails> GetDeviceDetailsAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<DeviceDetailsDto>("di/v1/products/1/device", cancellationToken);
        return DeviceDetailsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public async Task<WifiDetails> GetWifiDetailsAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<WifiDetailsDto>("di/v1/products/0/wifi", cancellationToken);
        return WifiDetailsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public async Task<FirmwareDetails> GetFirmwareDetailsAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<FirmwareDetailsDto>("di/v1/products/0/firmware", cancellationToken);
        return FirmwareDetailsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public async Task<Locale> GetLocaleAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<LocaleDto>("di/v1/products/0/locale", cancellationToken);
        return LocaleMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public async Task<Time> GetTimeAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<TimeDto>("di/v1/products/0/time", cancellationToken);
        return TimeMapper.ToModel(dto);
    }

    #endregion

    #region Somneo: Sensors

    /// <inheritdoc/>
    public async Task<SensorData> GetSensorDataAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<SensorDataDto>("di/v1/products/1/wusrd", cancellationToken);
        return SensorDataMapper.ToModel(dto);
    }

    #endregion

    #region Somneo: Light

    /// <inheritdoc/>
    public async Task<LightState> GetLightStateAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<LightStateDto>("di/v1/products/1/wulgt", cancellationToken);
        return LightStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Task ToggleLightAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        var data = new
        {
            onoff = enabled,    // Toggle the light
            tempy = false,      // Specifies NOT to be in preview/temporary mode?
            ngtlt = false       // Disable the night light
        };

        return ExecutePutRequestAsync("di/v1/products/1/wulgt", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetLightLevelAsync(int lightLevel, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.LightLevel.ThrowIfOutOfRange(lightLevel, nameof(lightLevel));

        var data = new
        {
            ltlvl = lightLevel, // Set the level
            onoff = true,       // Enable the light
            tempy = false,      // Specifies NOT to be in preview/temporary mode?
            ngtlt = false       // Disable the night light
        };

        return ExecutePutRequestAsync("di/v1/products/1/wulgt", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task ToggleNightLightAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        var data = new
        {
            onoff = false,  // Disable the regular light
            tempy = false,  // Specifies NOT to be in preview/temporary mode?
            ngtlt = enabled // Enable the night light
        };

        return ExecutePutRequestAsync("di/v1/products/1/wulgt", data, cancellationToken);
    }

    #endregion

    #region Somneo: Display

    /// <inheritdoc/>
    public async Task<DisplayState> GetDisplayStateAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<DisplayStateDto>("di/v1/products/1/wusts", cancellationToken);
        return DisplayStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Task TogglePermanentDisplayAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        var data = new
        {
            dspon = enabled
        };

        return ExecutePutRequestAsync("di/v1/products/1/wusts", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetDisplayLevelAsync(int displayLevel, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.DisplayLevel.ThrowIfOutOfRange(displayLevel, nameof(displayLevel));

        var data = new
        {
            brght = displayLevel
        };

        return ExecutePutRequestAsync("di/v1/products/1/wusts", data, cancellationToken);
    }

    #endregion

    #region Somneo: Wake-up sounds

    /// <inheritdoc/>
    public Task EnableWakeUpSoundPreviewAsync(WakeUpSound wakeUpSound, int volume, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.WakeUpSound.ThrowIfOutOfRange(wakeUpSound, nameof(wakeUpSound));
        SomneoParameterValidators.Volume.ThrowIfOutOfRange(volume, nameof(volume));

        var data = new
        {
            sndss = 1000,   // What is this?
            onoff = true,   // Enable the player
            tempy = true,   // Specifies to be in preview/temporary mode?
            snddv = "wus",  // Set the player to wake-up sound
            sndch = EnumMapper.GetWakeUpSoundValue(wakeUpSound)!.Value.ToString(),
            sdvol = volume
        };

        return ExecutePutRequestAsync("di/v1/products/1/wuply", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task DisableWakeUpSoundPreviewAsync(CancellationToken cancellationToken = default)
    {
        var data = new
        {
            onoff = false,  // Disable the player
            tempy = true,   // Specifies to be in preview/temporary mode?
        };

        return ExecutePutRequestAsync("di/v1/products/1/wuply", data, cancellationToken);
    }

    #endregion

    #region Somneo: FM radio

    /// <inheritdoc/>
    public async Task<FMRadioPresets> GetFMRadioPresetsAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<FMRadioPresetsDto>("di/v1/products/1/wufmp/00", cancellationToken);
        return FMRadioPresetsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public async Task<float> GetFMRadioPresetAsync(int preset, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.FMRadioPreset.ThrowIfOutOfRange(preset, nameof(preset));

        var data = new
        {
            fmcmd = "recall",
            prstn = preset
        };

        var dto = await ExecutePutRequestAsync<FMRadioStateDto>("di/v1/products/1/wufmr", data, cancellationToken);

        return dto.Frequency;
    }

    /// <inheritdoc/>
    public async Task<FMRadioState> GetFMRadioStateAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<FMRadioStateDto>("di/v1/products/1/wufmr", cancellationToken);
        return FMRadioStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Task EnableFMRadioAsync(CancellationToken cancellationToken = default)
    {
        var data = new
        {
            sndss = 0,      // What is this?
            onoff = true,   // Enable the player
            tempy = false,  // Specifies NOT to be in preview/temporary mode?
            snddv = "fmr"   // Set the player to FM radio
        };

        return ExecutePutRequestAsync("di/v1/products/1/wuply", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task EnableFMRadioPresetAsync(int preset, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.FMRadioPreset.ThrowIfOutOfRange(preset, nameof(preset));

        var data = new
        {
            sndss = 0,                  // What is this?
            onoff = true,               // Enable the player
            tempy = false,              // Specifies NOT to be in preview/temporary mode?
            snddv = "fmr",              // Set the player to FM radio
            sndch = preset.ToString()   // Set the "channel" to the preset number
        };

        return ExecutePutRequestAsync("di/v1/products/1/wuply", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SeekFMRadioStationAsync(RadioSeekDirection direction, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.RadioSeekDirection.ThrowIfOutOfRange(direction, nameof(direction));

        var data = new
        {
            fmcmd = EnumMapper.GetRadioSeekDirectionValue(direction)
        };

        return ExecutePutRequestAsync("di/v1/products/1/wufmr", data, cancellationToken);
    }

    #endregion

    #region Somneo: AUX

    /// <inheritdoc/>
    public async Task EnableAUXAsync(CancellationToken cancellationToken = default)
    {
        await DisablePlayerAsync(cancellationToken); // Disable the player first, because AUX does not enable right away for some reason

        var data = new
        {
            sndss = 0,      // What is this?
            onoff = true,   // Enable the player
            tempy = false,  // Specifies NOT to be in preview/temporary mode?
            snddv = "aux"   // Set the player to AUX
        };

        await ExecutePutRequestAsync("di/v1/products/1/wuply", data, cancellationToken);
    }

    #endregion

    #region Somneo: Audio player

    /// <inheritdoc/>
    public async Task<PlayerState> GetPlayerStateAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<PlayerStateDto>("di/v1/products/1/wuply", cancellationToken);
        return PlayerStateMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Task SetPlayerVolumeAsync(int volume, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.Volume.ThrowIfOutOfRange(volume, nameof(volume));

        var data = new
        {
            sdvol = volume
        };

        return ExecutePutRequestAsync("di/v1/products/1/wuply", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task DisablePlayerAsync(CancellationToken cancellationToken = default)
    {
        var data = new
        {
            onoff = false
        };

        return ExecutePutRequestAsync("di/v1/products/1/wuply", data, cancellationToken);
    }

    #endregion

    #region Somneo: Alarms

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Alarm>> GetAlarmsAsync(CancellationToken cancellationToken = default)
    {
        AlarmStatesDto alarmStatesDto = await ExecuteGetRequestAsync<AlarmStatesDto>("di/v1/products/1/wualm/aenvs", cancellationToken);
        AlarmSchedulesDto alarmSchedulesDto = await ExecuteGetRequestAsync<AlarmSchedulesDto>("di/v1/products/1/wualm/aalms", cancellationToken);
        return AlarmMapper.ToModels(alarmStatesDto, alarmSchedulesDto);
    }

    /// <inheritdoc/>
    public Task ToggleAlarmAsync(int position, bool enabled, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.AlarmPosition.ThrowIfOutOfRange(position, nameof(position));

        var data = new
        {
            prfnr = position,
            prfen = enabled
        };

        return ExecutePutRequestAsync("di/v1/products/1/wualm/prfwu", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetAlarmWithWakeUpSoundAsync(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        WakeUpSound wakeUpSound, int volume,
        CancellationToken cancellationToken = default)
    {
        return SetAlarmAsync(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            volume, SoundDeviceType.WakeUpSound, wakeUpSound, null, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetAlarmWithFMRadioAsync(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int fmRadioPreset, int volume,
        CancellationToken cancellationToken = default)
    {
        return SetAlarmAsync(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            volume, SoundDeviceType.FMRadio, null, fmRadioPreset, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetAlarmWithoutSoundAsync(
        int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek> repeatDays,
        ColorScheme sunriseColors, int sunriseIntensity, int sunriseDuration,
        CancellationToken cancellationToken = default)
    {
        return SetAlarmAsync(
            position,
            hour, minute,
            powerWakeMinutes,
            repeatDays,
            sunriseColors, sunriseIntensity, sunriseDuration,
            null, null, null, null, cancellationToken);
    }

    private Task SetAlarmAsync(int position,
        int hour, int minute,
        int? powerWakeMinutes,
        ICollection<DayOfWeek>? repeatDays,
        ColorScheme? sunriseColors, int? sunriseIntensity, int? sunriseDuration,
        int? volume, SoundDeviceType? soundDevice, WakeUpSound? wakeUpSound, int? fmRadioPreset,
        CancellationToken cancellationToken)
    {
        SomneoParameterValidators.AlarmPosition.ThrowIfOutOfRange(position, nameof(position));
        SomneoParameterValidators.AlarmHour.ThrowIfOutOfRange(hour, nameof(hour));
        SomneoParameterValidators.AlarmMinute.ThrowIfOutOfRange(minute, nameof(minute));
        if (repeatDays != null)
            SomneoParameterValidators.AlarmRepeatDay.ThrowIfOutOfRange(repeatDays, nameof(repeatDays));
        if (volume.HasValue)
            SomneoParameterValidators.Volume.ThrowIfOutOfRange(volume.Value, nameof(volume));
        if (fmRadioPreset.HasValue)
            SomneoParameterValidators.FMRadioPreset.ThrowIfOutOfRange(fmRadioPreset.Value, nameof(fmRadioPreset));
        if (wakeUpSound.HasValue)
            SomneoParameterValidators.WakeUpSound.ThrowIfOutOfRange(wakeUpSound.Value, nameof(wakeUpSound));

        int powerWakeSize = 0;
        int definitivePowerWakeHour = 0;
        int definitivePowerWakeMinute = 0;

        if (powerWakeMinutes.HasValue)
        {
            SomneoParameterValidators.PowerWakeMinutes.ThrowIfOutOfRange(powerWakeMinutes.Value, nameof(powerWakeMinutes));

            var powerWakeTime = new TimeSpan(hour, minute, 0)
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
            SomneoParameterValidators.SunriseColors.ThrowIfOutOfRange(sunriseColors.Value, nameof(sunriseColors));
            SomneoParameterValidators.SunriseIntensity.ThrowIfOutOfRange(sunriseIntensity.GetValueOrDefault(), nameof(sunriseColors));
            SomneoParameterValidators.SunriseDuration.ThrowIfOutOfRange(sunriseDuration.GetValueOrDefault(), nameof(sunriseDuration));

            sunriseColorSchemeNumber = EnumMapper.GetColorSchemeValue(sunriseColors)!.Value;
            definitiveSunriseIntensity = sunriseIntensity!.Value;
            definitiveSunriseDuration = sunriseDuration!.Value;
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

        var data = new
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

        return ExecutePutRequestAsync("di/v1/products/1/wualm/prfwu", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task RemoveAlarmAsync(int position, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.AlarmPosition.ThrowIfOutOfRange(position, nameof(position));

        var data = new
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

        return ExecutePutRequestAsync("di/v1/products/1/wualm/prfwu", data, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<AlarmSettings?> GetAlarmSettingsAsync(int position, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.AlarmPosition.ThrowIfOutOfRange(position, nameof(position));

        var data = new
        {
            prfnr = position
        };

        var dto = await ExecutePutRequestAsync<AlarmSettingsDto>("di/v1/products/1/wualm", data, cancellationToken);

        return dto.IsSet ? AlarmSettingsMapper.ToModel(dto) : null;
    }

    /// <inheritdoc/>
    public Task SetSnoozeTimeAsync(int minutes, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.SnoozeMinutes.ThrowIfOutOfRange(minutes, nameof(minutes));

        var data = new
        {
            snztm = minutes
        };

        return ExecutePutRequestAsync("di/v1/products/1/wualm", data, cancellationToken);
    }

    #endregion

    #region Somneo: Timer

    /// <inheritdoc/>
    public async Task<TimerState> GetTimerStateAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<TimerStateDto>("di/v1/products/1/wutmr", cancellationToken);
        return TimerStateMapper.ToModel(dto);
    }

    #endregion

    #region Somneo: Sunrise

    /// <inheritdoc/>
    public Task EnableSunrisePreviewAsync(ColorScheme sunriseColors, int sunriseIntensity, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.SunriseColors.ThrowIfOutOfRange(sunriseColors, nameof(sunriseColors));
        SomneoParameterValidators.SunriseIntensity.ThrowIfOutOfRange(sunriseIntensity, nameof(sunriseIntensity));

        var data = new
        {
            onoff = true,   // Enable the light
            tempy = true,   // Specifies to be in preview/temporary mode?
            ngtlt = false,  // Disable the night light
            ctype = EnumMapper.GetColorSchemeValue(sunriseColors)!.Value,
            ltlvl = sunriseIntensity
        };

        return ExecutePutRequestAsync("di/v1/products/1/wulgt", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task DisableSunrisePreviewAsync(CancellationToken cancellationToken = default)
    {
        var data = new
        {
            onoff = false,  // Disable the light
            tempy = true,   // Specifies to be in preview/temporary mode?
        };

        return ExecutePutRequestAsync("di/v1/products/1/wulgt", data, cancellationToken);
    }

    #endregion

    #region Somneo: Sunset

    /// <inheritdoc/>
    public async Task<SunsetSettings> GetSunsetSettingsAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<SunsetSettingsDto>("di/v1/products/1/wudsk", cancellationToken);
        return SunsetSettingsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Task ToggleSunsetAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        var data = new
        {
            onoff = enabled
        };

        return ExecutePutRequestAsync("di/v1/products/1/wudsk", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetSunsetSettingsWithSunsetSoundAsync(
        ColorScheme sunsetColors, int sunsetIntensity, int sunsetDuration,
        SunsetSound sunsetSound, int volume,
        CancellationToken cancellationToken = default)
    {
        return SetSunsetSettingsAsync(
            sunsetColors, sunsetIntensity, sunsetDuration,
            volume, SoundDeviceType.Sunset, sunsetSound, null, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetSunsetSettingsWithFMRadioAsync(
        ColorScheme sunsetColors, int sunsetIntensity, int sunsetDuration,
        int fmRadioPreset, int volume,
        CancellationToken cancellationToken = default)
    {
        return SetSunsetSettingsAsync(
            sunsetColors, sunsetIntensity, sunsetDuration,
            volume, SoundDeviceType.FMRadio, null, fmRadioPreset, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetSunsetSettingsWithoutSoundAsync(ColorScheme sunsetColors, int sunsetIntensity, int sunsetDuration, CancellationToken cancellationToken = default)
    {
        return SetSunsetSettingsAsync(
            sunsetColors, sunsetIntensity, sunsetDuration,
            null, null, null, null, cancellationToken);
    }

    private Task SetSunsetSettingsAsync(
        ColorScheme sunsetColors, int sunsetIntensity, int sunsetDuration,
        int? volume, SoundDeviceType? soundDevice, SunsetSound? sunsetSound, int? fmRadioPreset,
        CancellationToken cancellationToken)
    {
        SomneoParameterValidators.SunsetColors.ThrowIfOutOfRange(sunsetColors, nameof(sunsetColors));
        SomneoParameterValidators.SunsetIntensity.ThrowIfOutOfRange(sunsetIntensity, nameof(sunsetIntensity));
        SomneoParameterValidators.SunsetDuration.ThrowIfOutOfRange(sunsetDuration, nameof(sunsetDuration));
        if (volume.HasValue)
            SomneoParameterValidators.Volume.ThrowIfOutOfRange(volume.Value, nameof(volume));
        if (fmRadioPreset.HasValue)
            SomneoParameterValidators.FMRadioPreset.ThrowIfOutOfRange(fmRadioPreset.Value, nameof(fmRadioPreset));
        if (sunsetSound.HasValue)
            SomneoParameterValidators.SunsetSound.ThrowIfOutOfRange(sunsetSound.Value, nameof(sunsetSound));

        int sunsetColorSchemeNumber = EnumMapper.GetColorSchemeValue(sunsetColors)!.Value;

        int soundChannel = -1;

        switch (soundDevice)
        {
            case SoundDeviceType.Sunset:
                soundChannel = EnumMapper.GetSunsetSoundValue(sunsetSound)!.Value;
                break;

            case SoundDeviceType.FMRadio:
                soundChannel = fmRadioPreset!.Value;
                break;
        }

        string soundDeviceName = EnumMapper.GetSoundDeviceTypeValue(soundDevice);

        var data = new
        {
            ctype = sunsetColorSchemeNumber,    // The sunset colors
            curve = sunsetIntensity,            // The light level
            durat = sunsetDuration,             // The sunrise duration
            snddv = soundDeviceName,            // The sound device to play
            sndch = soundChannel.ToString(),    // The sunset sound or FM radio preset to play
            sndlv = volume ?? 12                // The volume level of the sound device to play
        };

        return ExecutePutRequestAsync("di/v1/products/1/wudsk", data, cancellationToken);
    }

    #endregion

    #region Somneo: Bedtime

    /// <inheritdoc/>
    public Task StartBedtimeAsync(CancellationToken cancellationToken = default)
    {
        var data = new
        {
            night = true
        };

        return ExecutePutRequestAsync("di/v1/products/1/wungt", data, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<BedtimeInfo> EndBedtimeAsync(CancellationToken cancellationToken = default)
    {
        var data = new
        {
            night = false
        };

        var dto = await ExecutePutRequestAsync<BedtimeInfoDto>("di/v1/products/1/wungt", data, cancellationToken);
        return BedtimeInfoMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public async Task<BedtimeInfo?> GetLastBedtimeInfoAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<BedtimeInfoDto>("di/v1/products/1/wungt", cancellationToken);
        return dto.Started.HasValue ? BedtimeInfoMapper.ToModel(dto) : null;
    }

    #endregion

    #region Somneo: RelaxBreathe

    /// <inheritdoc/>
    public async Task<RelaxBreatheSettings> GetRelaxBreatheSettingsAsync(CancellationToken cancellationToken = default)
    {
        var dto = await ExecuteGetRequestAsync<RelaxBreatheSettingsDto>("di/v1/products/1/wurlx", cancellationToken);
        return RelaxBreatheSettingsMapper.ToModel(dto);
    }

    /// <inheritdoc/>
    public Task ToggleRelaxBreatheAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        var data = new
        {
            onoff = enabled
        };

        return ExecutePutRequestAsync("di/v1/products/1/wurlx", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetRelaxBreatheSettingsWithSoundAsync(int duration, int breathsPerMinuteOption, int volume, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.RelaxBreatheDuration.ThrowIfOutOfRange(duration, nameof(duration));
        SomneoParameterValidators.RelaxBreatheBreathsPerMinute.ThrowIfOutOfRange(breathsPerMinuteOption, nameof(breathsPerMinuteOption));
        SomneoParameterValidators.Volume.ThrowIfOutOfRange(volume, nameof(volume));

        var data = new
        {
            rtype = 1,                          // Sets the type to sound
            durat = duration,                   // The duration of RelaxBreathe
            progr = breathsPerMinuteOption + 1, // The option (1-based index) defining the amount of breaths per second
            sndlv = volume                      // The volume level of the sounds that are played
        };

        return ExecutePutRequestAsync("di/v1/products/1/wurlx", data, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SetRelaxBreatheSettingsWithLightAsync(int duration, int breathsPerMinuteOption, int intensity, CancellationToken cancellationToken = default)
    {
        SomneoParameterValidators.RelaxBreatheDuration.ThrowIfOutOfRange(duration, nameof(duration));
        SomneoParameterValidators.RelaxBreatheBreathsPerMinute.ThrowIfOutOfRange(breathsPerMinuteOption, nameof(breathsPerMinuteOption));
        SomneoParameterValidators.RelaxBreatheLightIntensity.ThrowIfOutOfRange(intensity, nameof(intensity));

        var data = new
        {
            rtype = 1,                          // Sets the type to sound
            durat = duration,                   // The duration of RelaxBreathe
            progr = breathsPerMinuteOption + 1, // The option (1-based index) defining the amount of breaths per second
            intny = intensity                   // The intensity of the light used for the exercises
        };

        return ExecutePutRequestAsync("di/v1/products/1/wurlx", data, cancellationToken);
    }

    #endregion

    #region HTTP requests

    private Task<T> ExecuteGetRequestAsync<T>(string resource, CancellationToken cancellationToken) => ExecuteRequestAsync<T>(resource, HttpMethod.Get, null, cancellationToken);

    private Task ExecutePutRequestAsync(string resource, object? data, CancellationToken cancellationToken) => ExecuteRequestAsync(resource, HttpMethod.Put, data, cancellationToken);

    private Task<T> ExecutePutRequestAsync<T>(string resource, object? data, CancellationToken cancellationToken) => ExecuteRequestAsync<T>(resource, HttpMethod.Put, data, cancellationToken);

    private async Task ExecuteRequestAsync(string resource, HttpMethod method, object? data, CancellationToken cancellationToken)
    {
        using var request = CreateHttpRequest(resource, method, data);

        await Semaphore.WaitAsync(cancellationToken);

        try
        {
            using var response = await GetHttpResponseAsync(request, cancellationToken);
        }
        finally
        {
            Semaphore.Release();
        }
    }

    private async Task<T> ExecuteRequestAsync<T>(string resource, HttpMethod method, object? data, CancellationToken cancellationToken)
    {
        using var request = CreateHttpRequest(resource, method, data);
        T? responseData;

        await Semaphore.WaitAsync(cancellationToken);

        try
        {
            using var response = await GetHttpResponseAsync(request, cancellationToken);
            responseData = await _serializer.ReadHttpContentAsync<T>(response.Content, cancellationToken);
        }
        finally
        {
            Semaphore.Release();
        }

        return responseData ?? throw new SomneoApiException("The Somneo returned an empty response.");
    }

    private HttpRequestMessage CreateHttpRequest(string resource, HttpMethod method, object? data)
    {
        var request = new HttpRequestMessage(method, resource);

        if (data != null)
            request.Content = _serializer.CreateHttpContent(data);

        return request;
    }

    private async Task<HttpResponseMessage> GetHttpResponseAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;

        try
        {
            response = await HttpClient.SendAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new SomneoApiException("Failed to execute the Somneo request.", ex);
        }

        await ValidateHttpResponseAsync(response, cancellationToken);

        return response;
    }

    private static async Task ValidateHttpResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return;

        using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var streamReader = new StreamReader(contentStream);

        string content = await streamReader.ReadToEndAsync(cancellationToken);

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
                _httpClient?.Dispose();

            _semaphore?.Dispose();
        }

        _httpClient = null;
        _semaphore = null;
    }

    #endregion
}
