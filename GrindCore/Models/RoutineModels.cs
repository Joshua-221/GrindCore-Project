using System.Collections.Generic;

namespace GrindCore.Models;

public class ScheduledSession
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public DateOnly Date { get; set; }
    public string Title { get; set; } = string.Empty; // Ej: "Bloque de Fuerza - SBD" o "Día de Accesorios"
    public List<ScheduledExercise> Exercises { get; set; } = new();
}

public class ScheduledExercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Ej: "Sumo Deadlift", "Squat Larsen", "Prensa"
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public double Rpe { get; set; }
    public string Notes { get; set; } = string.Empty; // Para indicaciones extras del coach (ej: "3 seg de pausa")
}


public class UserRoutine
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ExerciseRecord> Exercises { get; set; } = new();
}

public class ExerciseRecord
{
    public int Id { get; set; }
    public int RoutineId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public double Rpe { get; set; }
}