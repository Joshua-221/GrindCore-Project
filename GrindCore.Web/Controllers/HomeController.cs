using Microsoft.AspNetCore.Mvc;

namespace GrindCore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}