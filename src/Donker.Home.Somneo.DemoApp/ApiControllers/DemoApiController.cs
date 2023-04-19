using Donker.Home.Somneo.DemoApp.ApiModels.Demo;
using Donker.Home.Somneo.DemoApp.CommandRunner;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

    [HttpGet]
    [Route("display")]
    public async Task<IActionResult> Display()
    {
        var displayState = await _commandRunner.Execute(somneo => somneo.GetDisplayState());
        return Ok(displayState);
    }

    [HttpPut]
    [Route("display/permanent")]
    public async Task<IActionResult> DisplayPermanent(PutDisplayPermanentModel model)
    {
        await _commandRunner.Execute(somneo => somneo.TogglePermanentDisplay(model.Permanent));
        return Ok();
    }

    [HttpPut]
    [Route("display/brightness")]
    public async Task<IActionResult> DisplayBrightness(PutDisplayBrightnessModel model)
    {
        await _commandRunner.Execute(somneo => somneo.SetDisplayLevel(model.Brightness));
        return Ok();
    }
}
