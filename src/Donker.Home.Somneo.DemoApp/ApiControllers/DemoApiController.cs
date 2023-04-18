using Donker.Home.Somneo.DemoApp.ApiModels.Demo;
using Donker.Home.Somneo.DemoApp.CommandRunner;
using Microsoft.AspNetCore.Mvc;

namespace Donker.Home.Somneo.DemoApp.ApiControllers;

[ApiController]
[Route("api/demo")]
public class DemoApiController : ControllerBase
{
    private readonly ISomneoCommandRunner _commandRunner;

    public DemoApiController(ISomneoCommandRunner commandRunner)
    {
        _commandRunner = commandRunner;
    }

    [HttpGet]
    [Route("details")]
    public async Task<IActionResult> Details()
    {
        var deviceDetails = await _commandRunner.Execute(somneo => somneo.GetDeviceDetails());
        var wifiDetails = await _commandRunner.Execute(somneo => somneo.GetWifiDetails());
        var firmwareDetails = await _commandRunner.Execute(somneo => somneo.GetFirmwareDetails());
        var locale = await _commandRunner.Execute(somneo => somneo.GetLocale());
        var time = await _commandRunner.Execute(somneo => somneo.GetTime());

        var model = new DetailsResponseModel(
           deviceDetails,
           wifiDetails,
           firmwareDetails,
           locale,
           time);

        return Ok(model);
    }
}
