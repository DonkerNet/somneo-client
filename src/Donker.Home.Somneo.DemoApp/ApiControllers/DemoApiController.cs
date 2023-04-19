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
        var device = await _commandRunner.Execute(somneo => somneo.GetDeviceDetails());
        var wifi = await _commandRunner.Execute(somneo => somneo.GetWifiDetails());
        var firmware = await _commandRunner.Execute(somneo => somneo.GetFirmwareDetails());
        var locale = await _commandRunner.Execute(somneo => somneo.GetLocale());
        var time = await _commandRunner.Execute(somneo => somneo.GetTime());

        var model = new {
           device,
           wifi,
           firmware,
           locale,
           time
        };

        return Ok(model);
    }

    [HttpGet]
    [Route("sensors")]
    public async Task<IActionResult> Sensors()
    {
        var sensorData = await _commandRunner.Execute(somneo => somneo.GetSensorData());
        return Ok(sensorData);
    }
}
