using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GrindCore.Models;

namespace GrindCore.Controllers;

public class HomeController : Controller
{
    private const string SessionKeyUserRoutines = "grindcore.user.routines";

    public IActionResult Index()
    {
        // read user routines from session
        var userRoutines = new List<RoutineSeed>();
        var json = HttpContext.Session.GetString(SessionKeyUserRoutines);
        if (!string.IsNullOrEmpty(json))
        {
            try
            {
                userRoutines = System.Text.Json.JsonSerializer.Deserialize<List<RoutineSeed>>(json) ?? new List<RoutineSeed>();
            }
            catch { }
        }

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
            UserRoutines = userRoutines,
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddRoutine(string routineName, string routineFocus)
    {
        if (string.IsNullOrWhiteSpace(routineName))
        {
            TempData["AddRoutineError"] = "El nombre de la rutina es requerido.";
            return RedirectToAction("Index");
        }

        var json = HttpContext.Session.GetString(SessionKeyUserRoutines);
        var list = new List<RoutineSeed>();
        if (!string.IsNullOrEmpty(json))
        {
            try { list = System.Text.Json.JsonSerializer.Deserialize<List<RoutineSeed>>(json) ?? new List<RoutineSeed>(); } catch { }
        }

        list.Add(new RoutineSeed(routineName, routineFocus ?? string.Empty, new List<ExerciseSeed>()));
        HttpContext.Session.SetString(SessionKeyUserRoutines, System.Text.Json.JsonSerializer.Serialize(list));

        return RedirectToAction("Index");
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
