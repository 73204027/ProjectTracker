using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Models;

namespace ProjectTracking.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var home_vm = new HomeViewModel()

        const data_request = Json.SerializeObject()

        using (var api = helpers.ApiHelper() )
        {
            const response = api.Get(request, ),
        }

        return View(home_vm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
