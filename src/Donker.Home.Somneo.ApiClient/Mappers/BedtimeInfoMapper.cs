using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class BedtimeInfoMapper
{
    public static BedtimeInfo ToModel(BedtimeInfoDto dto)
    {
        return new BedtimeInfo(
            dto.Started.GetValueOrDefault(),
            dto.Ended.GetValueOrDefault());
    }
}
