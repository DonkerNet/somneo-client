using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class LocaleMapper
{
    public static Locale ToModel(LocaleDto dto)
    {
        return new Locale(
            dto.Country,
            dto.Timezone);
    }
}
