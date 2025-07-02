using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class SensorCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("sensor", "Show sensor data.", ShowSensorDataAsync);
    }

    private async Task ShowSensorDataAsync(string? args)
    {
        var sensorData = await SomneoApiClient.GetSensorDataAsync();

        Console.WriteLine(
$@"Sensor data:
  Temperature: {sensorData.CurrentTemperature} °C (avg: {sensorData.AverageTemperature} °C)
  Light: {sensorData.CurrentLight} lux (avg: {sensorData.AverageLight} lux)
  Sound: {sensorData.CurrentSound} dB (avg: {sensorData.AverageSound} dB)
  Humidity: {sensorData.CurrentHumidity} % (avg: {sensorData.AverageHumidity} %)");
    }
}
