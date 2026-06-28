using GrindCore.Models;

namespace GrindCore.Domain.Interfaces;

public interface IAthleteRepository
{
    void DeleteAthlete(int id);
    IEnumerable<Athlete> GetAll();
    Athlete? GetById(int id);
    void AddAthlete(Athlete athlete);
    void UpdateAthlete(Athlete athlete);
    void AddLog(int athleteId, WorkoutLog log);

    IEnumerable<UserRoutine> GetRoutinesByAthlete(int athleteId);
    UserRoutine? GetRoutineById(int id);
    void AddRoutine(UserRoutine routine);
    void UpdateExerciseSeries(int exerciseId, int sets, int reps, double weight, double rpe);

    ScheduledSession? GetSessionByDate(int athleteId, DateOnly date);
    void SaveOrUpdateSession(ScheduledSession session);
    void DeleteSession(int athleteId, DateOnly date);
    HashSet<DateOnly> GetScheduledDates(int athleteId);
    IEnumerable<ScheduledSession> GetSessionsByAthlete(int athleteId);
}
