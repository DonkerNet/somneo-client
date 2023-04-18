using Donker.Home.Somneo.DemoApp.Models.Demo;
using Microsoft.AspNetCore.Mvc;

namespace Donker.Home.Somneo.DemoApp.Controllers;

[Route("")]
public class DemoController : Controller
{
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        string apiBaseUrl = $"{Request.Scheme}://{Request.Host}/api/demo";
        var model = new DemoIndexViewModel(apiBaseUrl);
        return View(model);
    }
}
