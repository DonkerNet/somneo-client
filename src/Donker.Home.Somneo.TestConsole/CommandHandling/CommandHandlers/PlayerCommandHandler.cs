using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class PlayerCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("player", "Show the player state.", ShowPlayerStateAsync);
        commandRegistry.RegisterCommand("set-player-volume", "[1-25]", "Set the player volume.", SetPlayerVolumeAsync);
        commandRegistry.RegisterCommand("disable-player", "Disable the player.", DisablePlayerAsync);
    }

    private async Task ShowPlayerStateAsync(string? args)
    {
        var playerState = await SomneoApiClient.GetPlayerStateAsync();

        string soundDevice = playerState.SoundDevice.HasValue ? EnumHelper.GetDescription(playerState.SoundDevice.Value)! : "None";

        string? channelOrPresetState = null;
        switch (playerState.SoundDevice)
        {
            case SoundDeviceType.FMRadio:
                if (playerState.FMRadioPreset.HasValue)
                    channelOrPresetState = $"{Environment.NewLine}  FM-radio preset: {playerState.FMRadioPreset.Value}";
                break;
            case SoundDeviceType.WakeUpSound:
                if (playerState.WakeUpSound.HasValue)
                    channelOrPresetState = $"{Environment.NewLine}  Wake-up sound: {EnumHelper.GetDescription(playerState.WakeUpSound.Value)}";
                break;
            case SoundDeviceType.Sunset:
                if (playerState.SunsetSound.HasValue)
                    channelOrPresetState = $"{Environment.NewLine}  Sunset sound: {EnumHelper.GetDescription(playerState.SunsetSound.Value)}";
                break;
        }

        Console.WriteLine(
$@"Audio player state:
  Enabled: {(playerState.Enabled ? "Yes" : "No")}
  Volume: {playerState.Volume}/25
  Device: {soundDevice}{channelOrPresetState}");
    }

    private async Task SetPlayerVolumeAsync(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int volume) && volume >= 1 && volume <= 25)
        {
            await SomneoApiClient.SetPlayerVolumeAsync(volume);
            Console.WriteLine($"Audio player volume set to {volume}/25.");
            return;
        }

        Console.WriteLine("Specify a volume between 1 and 25.");
    }

    private async Task DisablePlayerAsync(string? args)
    {
        await SomneoApiClient.DisablePlayerAsync();
        Console.WriteLine("Audio player disabled.");
    }
}
