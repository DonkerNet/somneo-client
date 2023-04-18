using System.Text.Json;

namespace Donker.Home.Somneo.DemoApp.Models.Demo;

public class DemoIndexViewModel
{
    public string ApiBaseUrl { get; }

    public DemoIndexViewModel(string apiBaseUrl)
    {
        ApiBaseUrl = apiBaseUrl;
    }

    public string GetConfigJson()
    {
        return JsonSerializer.Serialize(new
        {
            apiBaseUrl = ApiBaseUrl
        });
    }
}
