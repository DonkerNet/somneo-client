using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class DeviceDetailsMapper
{
    public static DeviceDetails ToModel(DeviceDetailsDto dto)
    {
        return new DeviceDetails(
            dto.AssignedName,
            dto.TypeNumber,
            dto.Serial,
            dto.ProductId,
            dto.ProductName,
            dto.ModelId);
    }
}
