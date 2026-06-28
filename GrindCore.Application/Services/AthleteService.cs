using GrindCore.Domain.Interfaces;
using GrindCore.Models;

namespace GrindCore.Application.Services;

public class AthleteService(IAthleteRepository repository) : IAthleteService
{
    public void DeleteAthlete(int id) => repository.DeleteAthlete(id);

    public IEnumerable<Athlete> GetAll() => repository.GetAll();

    public Athlete? GetById(int id) => repository.GetById(id);

    public void AddAthlete(Athlete athlete) => repository.AddAthlete(athlete);

    public void UpdateAthlete(Athlete athlete) => repository.UpdateAthlete(athlete);

    public void AddLog(int athleteId, WorkoutLog log) => repository.AddLog(athleteId, log);

    public IEnumerable<UserRoutine> GetRoutinesByAthlete(int athleteId) =>
        repository.GetRoutinesByAthlete(athleteId);

    public UserRoutine? GetRoutineById(int id) => repository.GetRoutineById(id);

    public void AddRoutine(UserRoutine routine) => repository.AddRoutine(routine);

    public void UpdateExerciseSeries(int exerciseId, int sets, int reps, double weight, double rpe) =>
        repository.UpdateExerciseSeries(exerciseId, sets, reps, weight, rpe);

    public ScheduledSession? GetSessionByDate(int athleteId, DateOnly date) =>
        repository.GetSessionByDate(athleteId, date);

    public void SaveOrUpdateSession(ScheduledSession session) => repository.SaveOrUpdateSession(session);

    public void DeleteSession(int athleteId, DateOnly date) => repository.DeleteSession(athleteId, date);

    public HashSet<DateOnly> GetScheduledDates(int athleteId) => repository.GetScheduledDates(athleteId);

    public IEnumerable<ScheduledSession> GetSessionsByAthlete(int athleteId) =>
        repository.GetSessionsByAthlete(athleteId);
}
