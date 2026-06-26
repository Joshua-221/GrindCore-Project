using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GrindCore.Models;

namespace GrindCore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = new WorkoutDashboardViewModel
        {
            SuggestedRoutines =
            [
                new RoutineSeed(
                    "Fuerza - sentadilla",
                    "Pierna",
                    [
                        new ExerciseSeed("Sentadilla", 5, 3, 120),
                        new ExerciseSeed("Pause squat", 3, 4, 95),
                        new ExerciseSeed("Peso muerto rumano", 3, 8, 90)
                    ]),
                new RoutineSeed(
                    "Banca volumen",
                    "Empuje",
                    [
                        new ExerciseSeed("Press banca", 4, 6, 85),
                        new ExerciseSeed("Banca con pausa", 3, 5, 75),
                        new ExerciseSeed("Press militar", 3, 8, 45)
                    ])
            ],
            ExerciseSuggestions =
            [
                "Sentadilla",
                "Press banca",
                "Peso muerto",
                "Press militar",
                "Pause squat",
                "Banca con pausa",
                "Peso muerto rumano",
                "Remo con barra"
            ]
        };

        return View(viewModel);
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
