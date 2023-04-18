using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class FMRadioPresetsMapper
{
    public static FMRadioPresets ToModel(FMRadioPresetsDto dto)
    {
        return new FMRadioPresets(
            dto.Preset1,
            dto.Preset2,
            dto.Preset3,
            dto.Preset4,
            dto.Preset5);
    }
}
