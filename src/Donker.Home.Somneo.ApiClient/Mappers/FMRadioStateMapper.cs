using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class FMRadioStateMapper
{
    public static FMRadioState ToModel(FMRadioStateDto dto)
    {
        return new FMRadioState(
            dto.Preset,
            dto.Frequency);
    }
}
