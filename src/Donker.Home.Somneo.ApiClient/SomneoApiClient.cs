using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.ApiClient.Serialization;
using RestSharp;

namespace Donker.Home.Somneo.ApiClient
{
    /// <summary>
    /// Client that provides communication with the Philips Somneo API.
    /// </summary>
    public sealed class SomneoApiClient : ISomneoApiClient
    {
        private readonly ISomneoApiSerializer _serializer;
        private readonly IRestClient _restClient;

        #region Public properties

        /// <summary>
        /// Gets the hostname of the Somneo device.
        /// </summary>
        public string Host => _restClient.BaseUrl?.Host;

        /// <summary>
        /// Gets or sets the maximum request timeout in milliseconds.
        /// </summary>
        public int Timeout
        {
            get => _restClient.Timeout;
            set => _restClient.Timeout = value < 0 ? 0 : value;
        }

        #endregion

        #region Constructors

        internal SomneoApiClient(IRestClient restClient, string host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host), "The host cannot be null.");
            if (host.Length == 0)
                throw new ArgumentException("The host cannot be empty.", nameof(host));
            if (!Uri.TryCreate($"https://{host}", UriKind.Absolute, out Uri baseUri))
                throw new ArgumentException("The host is not valid.", nameof(host));

            _serializer = SomneoApiSerializer.Instance;

            _restClient = restClient;
            _restClient.BaseUrl = baseUri;
            _restClient.ClearHandlers();
            _restClient.AddHandler(_serializer.ContentType, () => _serializer);

            // Ignore SSL errors, as the Somneo device uses a self signed certificate
            _restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SomneoApiClient"/> using the specified hostname.
        /// </summary>
        /// <param name="host">The hostname of the Somneo device to connect with.</param>
        /// <exception cref="ArgumentNullException">The host is null.</exception>
        /// <exception cref="ArgumentException">The host is empty or in an invalid format.</exception>
        public SomneoApiClient(string host)
            : this(new RestClient(), host)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SomneoApiClient"/> using the hostname of the specified URI.
        /// </summary>
        /// <param name="host">The URI with the hostname of the Somneo device to connect with.</param>
        /// <exception cref="ArgumentNullException">The URI with the hostname is null.</exception>
        /// <exception cref="InvalidOperationException">The URI with the hostname is not an absolute URL.</exception>
        public SomneoApiClient(Uri host)
            : this(host?.Host)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SomneoApiClient"/> using the specified IP address as host.
        /// </summary>
        /// <param name="host">The host IP address of the Somneo device to connect with.</param>
        /// <exception cref="ArgumentNullException">The host IP address is null.</exception>
        public SomneoApiClient(IPAddress host)
            : this(host?.ToString())
        {
        }

        #endregion

        #region General

        /// <summary>
        /// Retrieves details about the Somneo device itself.
        /// </summary>
        /// <returns>The details of the device as a <see cref="DeviceDetails"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public DeviceDetails GetDeviceDetails()
        {
            var response = ExecuteGetRequest<DeviceDetails>("di/v1/products/1/device");
            return response.Data;
        }

        /// <summary>
        /// Retrieves details about the Somneo's wifi connection.
        /// </summary>
        /// <returns>The details of the wifi connection as a <see cref="WifiDetails"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public WifiDetails GetWifiDetails()
        {
            var response = ExecuteGetRequest<WifiDetails>("di/v1/products/0/wifi");
            return response.Data;
        }

        /// <summary>
        /// Retrieves details about the Somneo's firmware.
        /// </summary>
        /// <returns>The firmware details as a <see cref="FirmwareDetails"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public FirmwareDetails GetFirmwareDetails()
        {
            var response = ExecuteGetRequest<FirmwareDetails>("di/v1/products/0/firmware");
            return response.Data;
        }

        /// <summary>
        /// Retrieves details about the locale set for the Somneo device.
        /// </summary>
        /// <returns>The locale details as a <see cref="Locale"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public Locale GetLocale()
        {
            var response = ExecuteGetRequest<Locale>("di/v1/products/0/locale");
            return response.Data;
        }

        /// <summary>
        /// Retrieves details about the time set for the Somneo device.
        /// </summary>
        /// <returns>The time details as a <see cref="Time"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public Time GetTime()
        {
            var response = ExecuteGetRequest<Time>("di/v1/products/0/time");
            return response.Data;
        }

        #endregion

        #region Sensors

        /// <summary>
        /// Retrieves the Somneo's sensor data, containing the temperature, light level, sound level and humidity.
        /// </summary>
        /// <returns>The sensor data as a <see cref="SensorData"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public SensorData GetSensorData()
        {
            var response = ExecuteGetRequest<SensorData>("di/v1/products/1/wusrd");
            return response.Data;
        }

        #endregion

        #region Light

        /// <summary>
        /// Retrieves the current light settings.
        /// </summary>
        /// <returns>The light settings as a <see cref="LightSettings"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public LightSettings GetLightSettings()
        {
            var response = ExecuteGetRequest<LightSettings>("di/v1/products/1/wulgt");
            return response.Data;
        }

        /// <summary>
        /// Toggles the normal light.
        /// </summary>
        /// <param name="enabled">Whether to enable or disable the light.</param>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public void ToggleLight(bool enabled)
        {
            object data;

            if (enabled)
            {
                data = new
                {
                    onoff = true,   // Enable the light
                    tempy = false,  // Disable sunrise preview
                    ngtlt = false   // Disable the night light
                };
            }
            else
            {
                data = new
                {
                    onoff = false   // Disable the light
                };
            }

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
            object data;

            if (enabled)
            {
                data = new
                {
                    onoff = false,  // Disable the regular light
                    tempy = false,  // Disable sunrise preview
                    ngtlt = true    // Enable the night light
                };
            }
            else
            {
                data = new
                {
                    ngtlt = false    // Disable the night light
                };
            }

            ExecutePutRequest("di/v1/products/1/wulgt", data);
        }

        /// <summary>
        /// Toggles the sunrise preview mode.
        /// </summary>
        /// <param name="enabled">Whether to enable or disable the sunrise preview.</param>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public void ToggleSunrisePreview(bool enabled)
        {
            object data;

            if (enabled)
            {
                data = new
                {
                    onoff = true,   // Enable the light
                    tempy = true,   // Enable sunrise preview
                    ngtlt = false   // Disable the night light
                };
            }
            else
            {
                data = new
                {
                    onoff = false,  // Disable the light
                    tempy = false   // Disable sunrise preview
                };
            }

            ExecutePutRequest("di/v1/products/1/wulgt", data);
        }

        #endregion

        #region Display

        /// <summary>
        /// Retrieves the current settings of the display.
        /// </summary>
        /// <returns>The display settings as a <see cref="LightSettings"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public DisplaySettings GetDisplaySettings()
        {
            var response = ExecuteGetRequest<DisplaySettings>("di/v1/products/1/wusts");
            return response.Data;
        }

        /// <summary>
        /// Toggles whether the display should always be shown or if it should disable automatically after a period of time.
        /// </summary>
        /// <param name="enabled">Whether to enable or disable the display permanently.</param>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public void TogglePermanentDisplay(bool enabled)
        {
            object data = new
            {
                dspon = enabled // Enable the display
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
                brght = brightnessLevel // Set the level
            };

            ExecutePutRequest("di/v1/products/1/wusts", data);
        }

        #endregion

        #region FM radio

        /// <summary>
        /// Retrieves the configured presets of FM radio frequencies.
        /// </summary>
        /// <returns>The FM radio presets ad a <see cref="FMRadioPresets"/> object.</returns>
        /// <exception cref="SomneoApiException">Exception thrown when a request to the Somneo device has failed.</exception>
        public FMRadioPresets GetFMRadioPresets()
        {
            var response = ExecuteGetRequest<FMRadioPresets>("di/v1/products/1/wufmp/00");
            return response.Data;
        }

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

            object data = new Dictionary<string, string>
            {
                { position.ToString(), frequency.ToString("0.00", NumberFormatInfo.InvariantInfo) }
            };

            ExecutePutRequest("di/v1/products/1/wufmp/00", data);
        }

        #endregion

        #region Private methods

        private IRestResponse<T> ExecuteGetRequest<T>(string resource)
            where T : new()
        {
            IRestRequest request = new RestRequest
            {
                Resource = resource,
                Method = Method.GET
            };

            return ExecuteRequest<T>(request);
        }

        private IRestResponse ExecutePostRequest(string resource, object data) => ExecuteRequestWithBody(resource, Method.POST, data);

        private IRestResponse ExecutePutRequest(string resource, object data) => ExecuteRequestWithBody(resource, Method.PUT, data);

        private IRestResponse ExecuteRequestWithBody(string resource, Method method, object data)
        {
            IRestRequest request = new RestRequest
            {
                Resource = resource,
                Method = method,
                RequestFormat = DataFormat.Json,
                JsonSerializer = _serializer
            };

            if (data != null)
                request.AddJsonBody(data);

            return ExecuteRequest(request);
        }

        private IRestResponse ExecuteRequest(IRestRequest request)
        {
            IRestResponse response = _restClient.Execute(request);
            ValidateResponse(response);
            return response;
        }

        private IRestResponse<T> ExecuteRequest<T>(IRestRequest request)
            where T : new()
        {
            IRestResponse<T> response = _restClient.Execute<T>(request);
            ValidateResponse(response);
            return response;
        }

        private void ValidateResponse(IRestResponse response)
        {
            int statusCode = (int)response.StatusCode;

            if (statusCode == 0)
                throw new SomneoApiException("Failed to execute the request.", response.ErrorException);

            if (statusCode < 200 || statusCode >= 300)
                throw new SomneoApiException($"The response returned status code {statusCode}, with the following data: {response.Content}", response.ErrorException, response.StatusCode, response.Content);
        }

        #endregion
    }
}
