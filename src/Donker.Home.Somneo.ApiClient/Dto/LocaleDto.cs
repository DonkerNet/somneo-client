namespace Donker.Home.Somneo.ApiClient.Dto;

internal class LocaleDto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string Country { get; set; }

    public string Timezone { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /* Example JSON:
{
  "country": "NL",
  "timezone": "Europe/Amsterdam"
}
     */
}
