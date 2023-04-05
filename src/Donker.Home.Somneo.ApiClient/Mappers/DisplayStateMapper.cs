using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class DisplayStateMapper
{
    public static DisplayState ToModel(DisplayStateDto dto)
    {
        return new DisplayState(
            dto.Permanent,
            dto.Brightness);
    }
}
