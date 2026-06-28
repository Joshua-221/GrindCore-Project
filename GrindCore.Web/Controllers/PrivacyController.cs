using Microsoft.AspNetCore.Mvc;

namespace GrindCore.Controllers;

public class PrivacyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
