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

    [HttpGet]
    [Route("light")]
    public async Task<IActionResult> Light()
    {
        var lightState = await _commandRunner.Execute(somneo => somneo.GetLightState());
        return Ok(lightState);
    }

    [HttpPut]
    [Route("light/enabled")]
    public async Task<IActionResult> LightEnabled(PutLightEnabledModel model)
    {
        await _commandRunner.Execute(somneo => somneo.ToggleLight(model.Enabled));
        return Ok();
    }

    [HttpPut]
    [Route("light/intensity")]
    public async Task<IActionResult> LightIntensity(PutLightIntensityModel model)
    {
        await _commandRunner.Execute(somneo => somneo.SetLightLevel(model.Intensity));
        return Ok();
    }

    [HttpGet]
    [Route("bedtime")]
    public async Task<IActionResult> Bedtime()
    {
        var bedtime = await _commandRunner.Execute(somneo => somneo.GetBedtimeInfo());
        return Ok(bedtime);
    }

    [HttpPut]
    [Route("bedtime/enabled")]
    public async Task<IActionResult> BedtimeEnabled(PutBedtimeEnabledModel model)
    {
        if (model.Enabled)
            await _commandRunner.Execute(somneo => somneo.StartBedtime());
        else
            await _commandRunner.Execute(somneo => somneo.EndBedtime());

        return Ok();
    }
}
