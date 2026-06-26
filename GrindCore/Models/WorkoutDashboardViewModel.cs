namespace GrindCore.Models;

public sealed class WorkoutDashboardViewModel
{
    public IReadOnlyList<RoutineSeed> SuggestedRoutines { get; init; } = [];

    public IReadOnlyList<RoutineSeed> UserRoutines { get; init; } = [];

    public IReadOnlyList<string> ExerciseSuggestions { get; init; } = [];
}

public sealed record RoutineSeed(
    string Name,
    string Focus,
    IReadOnlyList<ExerciseSeed> Exercises);

public sealed record ExerciseSeed(
    string Name,
    int Sets,
    int Reps,
    decimal LoadKg);
