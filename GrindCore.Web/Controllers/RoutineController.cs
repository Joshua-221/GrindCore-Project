using Microsoft.AspNetCore.Mvc;
using GrindCore.Models;
using GrindCore.Application.Services;
using System.Collections.Generic;

namespace GrindCore.Controllers;

public class RoutineController : Controller
{
    private readonly IAthleteService _athleteService;

    public RoutineController(IAthleteService athleteService)
    {
        _athleteService = athleteService;
    }

    [HttpGet]
    public IActionResult Create(int athleteId)
    {
        var athlete = _athleteService.GetById(athleteId);
        if (athlete == null) return NotFound();

        ViewData["AthleteId"] = athlete.Id;
        ViewData["AthleteName"] = athlete.Name;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        [FromForm(Name = "AthleteId")] int athleteId, 
        [FromForm(Name = "Name")] string name, 
        [FromForm(Name = "Description")] string description, 
        [FromForm(Name = "ExerciseName")] List<string> exerciseName, 
        [FromForm(Name = "Sets")] List<int> sets, 
        [FromForm(Name = "Reps")] List<int> reps, 
        [FromForm(Name = "Weight")] List<double> weight, 
        [FromForm(Name = "Rpe")] List<double> rpe)
    {
        if (athleteId == 0) return RedirectToAction("Index", "Athlete");

        var routine = new UserRoutine
        {
            AthleteId = athleteId,
            Name = name,
            Description = description,
            Exercises = new List<ExerciseRecord>()
        };

        if (exerciseName != null)
        {
            for (int i = 0; i < exerciseName.Count; i++)
            {
                routine.Exercises.Add(new ExerciseRecord
                {
                    ExerciseName = exerciseName[i],
                    Sets = sets[i],
                    Reps = reps[i],
                    Weight = weight[i],
                    Rpe = rpe[i]
                });
            }
        }

        _athleteService.AddRoutine(routine);
        return RedirectToAction("Details", "Athlete", new { id = athleteId });
    }
}