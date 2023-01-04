using Microsoft.AspNetCore.Mvc;

namespace Klien.Controllers;

public class TestCORSController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
