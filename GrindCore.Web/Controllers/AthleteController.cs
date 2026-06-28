using Microsoft.AspNetCore.Mvc;
using GrindCore.Application.Services;
using GrindCore.Models;

namespace GrindCore.Controllers;

public class AthleteController : Controller
{
    private readonly IAthleteService _service;

    public AthleteController(IAthleteService service)
    {
        _service = service;
    }

    public IActionResult Index() => View(_service.GetAll());

    public IActionResult Details(int id)
    {
        var athlete = _service.GetById(id);
        if (athlete == null) return NotFound();
        
        ViewBag.Routines = _service.GetRoutinesByAthlete(id);
        return View(athlete);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Athlete athlete)
    {
        if (!ModelState.IsValid) return View(athlete);
        _service.AddAthlete(athlete); 
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var athlete = _service.GetById(id);
        if (athlete == null) return NotFound();
        return View(athlete);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Athlete athlete)
    {
        if (!ModelState.IsValid) return View(athlete);
        _service.UpdateAthlete(athlete);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult Calendar(int athleteId, string? date)
    {
        var athlete = _service.GetById(athleteId);
        if (athlete == null) return NotFound();

        DateOnly selectedDate = DateOnly.TryParse(date, out var parsedDate) ? parsedDate : DateOnly.FromDateTime(DateTime.Today);
        var session = _service.GetSessionByDate(athleteId, selectedDate) ?? new ScheduledSession
        {
            AthleteId = athleteId,
            Date = selectedDate,
            Title = $"Rutina del día {selectedDate:dd/MM/yyyy}",
            Exercises = new List<ScheduledExercise>()
        };

        ViewBag.AthleteName = athlete.Name;
        ViewBag.SelectedDate = selectedDate.ToDateTime(TimeOnly.MinValue);
        ViewBag.DaysWithWorkouts = _service.GetScheduledDates(athleteId).Select(d => d.ToDateTime(TimeOnly.MinValue)).ToHashSet();

        return View(session);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveCalendarChanges(ScheduledSession session)
    {
        if (session.Exercises == null || !session.Exercises.Any())
        {
            _service.DeleteSession(session.AthleteId, session.Date);
        }
        else
        {
            _service.SaveOrUpdateSession(session);
        }

        return RedirectToAction(nameof(Calendar), new { athleteId = session.AthleteId, date = session.Date.ToString("yyyy-MM-dd") });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LogWorkout(int athleteId, string exercise, int sets, int reps, double weight, double rpe)
    {
        _service.AddLog(athleteId, new WorkoutLog { Exercise = exercise, Sets = sets, Reps = reps, Weight = weight, Rpe = rpe });
        return RedirectToAction(nameof(Details), new { id = athleteId });
    }
    
    [HttpGet]
    public IActionResult ProgrammedRoutine(int athleteId)
    {
        var athlete = _service.GetById(athleteId);
        if (athlete == null) return NotFound();
        ViewBag.AthleteName = athlete.Name;
        return View(_service.GetSessionsByAthlete(athleteId));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _service.DeleteAthlete(id);
        return RedirectToAction(nameof(Index));
    }
}