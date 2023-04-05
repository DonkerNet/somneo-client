using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class FirmwareDetailsMapper
{
    public static FirmwareDetails ToModel(FirmwareDetailsDto dto)
    {
        return new FirmwareDetails(
            dto.Name,
            dto.Version,
            dto.State,
            dto.Upgrade,
            dto.Progress,
            dto.StatusMessage,
            dto.CanDownload,
            dto.CanDownload,
            dto.Mandatory);
    }
}
