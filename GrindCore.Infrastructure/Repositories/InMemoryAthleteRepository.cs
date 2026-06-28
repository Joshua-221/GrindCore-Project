using GrindCore.Domain.Interfaces;
using GrindCore.Models;

namespace GrindCore.Infrastructure.Repositories;

public class InMemoryAthleteRepository : IAthleteRepository
{
    private static readonly List<Athlete> Athletes = new()
    {
        new Athlete { Id = 1, Name = "Joshua Cruz", Category = "-66kg", SquatPR = 180, BenchPR = 110, DeadliftPR = 210, ExperienceLevel = "Avanzado" }
    };

    private static readonly List<UserRoutine> Routines = new();
    private static readonly List<ScheduledSession> ScheduledSessions = new();

    public IEnumerable<Athlete> GetAll() => Athletes;

    public Athlete? GetById(int id) => Athletes.FirstOrDefault(a => a.Id == id);

    public void AddAthlete(Athlete athlete)
    {
        athlete.Id = Athletes.Count > 0 ? Athletes.Max(a => a.Id) + 1 : 1;
        Athletes.Add(athlete);
    }

    public void UpdateAthlete(Athlete athlete)
    {
        var existing = GetById(athlete.Id);
        if (existing != null)
        {
            existing.Name = athlete.Name;
            existing.Category = athlete.Category;
            existing.SquatPR = athlete.SquatPR;
            existing.BenchPR = athlete.BenchPR;
            existing.DeadliftPR = athlete.DeadliftPR;
            existing.ExperienceLevel = athlete.ExperienceLevel;
        }
    }

    public void DeleteAthlete(int id)
    {
        var athlete = Athletes.FirstOrDefault(a => a.Id == id);
        if (athlete != null)
        {
            Athletes.Remove(athlete);
        }
    }

    public void AddLog(int athleteId, WorkoutLog log) => GetById(athleteId)?.Logs.Insert(0, log);

    public IEnumerable<UserRoutine> GetRoutinesByAthlete(int athleteId) =>
        Routines.Where(r => r.AthleteId == athleteId);

    public UserRoutine? GetRoutineById(int id) => Routines.FirstOrDefault(r => r.Id == id);

    public void AddRoutine(UserRoutine routine)
    {
        routine.Id = Routines.Count > 0 ? Routines.Max(r => r.Id) + 1 : 1;
        Routines.Add(routine);
    }

    public void UpdateExerciseSeries(int exerciseId, int sets, int reps, double weight, double rpe)
    {
    }

    public ScheduledSession? GetSessionByDate(int athleteId, DateOnly date) =>
        ScheduledSessions.FirstOrDefault(s => s.AthleteId == athleteId && s.Date == date);

    public void SaveOrUpdateSession(ScheduledSession session)
    {
        var existing = GetSessionByDate(session.AthleteId, session.Date);
        if (existing != null)
        {
            existing.Title = session.Title;
            existing.Exercises = session.Exercises;
        }
        else
        {
            session.Id = ScheduledSessions.Count > 0 ? ScheduledSessions.Max(s => s.Id) + 1 : 1;
            ScheduledSessions.Add(session);
        }
    }

    public void DeleteSession(int athleteId, DateOnly date) =>
        ScheduledSessions.RemoveAll(s => s.AthleteId == athleteId && s.Date == date);

    public HashSet<DateOnly> GetScheduledDates(int athleteId) =>
        ScheduledSessions.Where(s => s.AthleteId == athleteId).Select(s => s.Date).ToHashSet();

    public IEnumerable<ScheduledSession> GetSessionsByAthlete(int athleteId) =>
        ScheduledSessions.Where(s => s.AthleteId == athleteId).OrderBy(s => s.Date).ToList();
}
