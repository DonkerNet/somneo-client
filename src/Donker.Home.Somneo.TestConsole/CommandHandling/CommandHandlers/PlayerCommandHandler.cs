using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class PlayerCommandHandler : CommandHandlerBase
{
    public PlayerCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("player", "Show the player state.", ShowPlayerState);
        commandRegistry.RegisterCommand("set-player-volume", "[1-25]", "Set the player volume.", SetPlayerVolume);
        commandRegistry.RegisterCommand("disable-player", "Disable the player.", DisablePlayer);
    }

    private void ShowPlayerState(string? args)
    {
        PlayerState playerState = SomneoApiClient.GetPlayerState();

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

    private void SetPlayerVolume(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int volume) && volume >= 1 && volume <= 25)
        {
            SomneoApiClient.SetPlayerVolume(volume);
            Console.WriteLine($"Audio player volume set to {volume}/25.");
            return;
        }

        Console.WriteLine("Specify a volume between 1 and 25.");
    }

    private void DisablePlayer(string? args)
    {
        SomneoApiClient.DisablePlayer();
        Console.WriteLine("Audio player disabled.");
    }
}
