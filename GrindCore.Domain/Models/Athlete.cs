using System;
using System.Collections.Generic;

namespace GrindCore.Models;

public class Athlete
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public double SquatPR { get; set; }
    public double BenchPR { get; set; }
    public double DeadliftPR { get; set; }
    public string ExperienceLevel { get; set; } = string.Empty;
    public List<WorkoutLog> Logs { get; set; } = new();
}

public class WorkoutLog
{
    public DateTime Date { get; set; } = DateTime.Now;
    public string Exercise { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public double Rpe { get; set; }
}