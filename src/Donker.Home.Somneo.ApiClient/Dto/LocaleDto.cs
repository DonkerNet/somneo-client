namespace Donker.Home.Somneo.ApiClient.Dto;

internal class LocaleDto
{
    public required string Country { get; set; }

    public required string Timezone { get; set; }

    /* Example JSON:
{
    "country": "NL",
    "timezone": "Europe/Amsterdam"
}
     */
}
