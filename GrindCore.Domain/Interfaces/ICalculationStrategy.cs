namespace GrindCore.Domain.Interfaces;

public interface ICalculationStrategy
{
    double Calculate(double weight, int reps, int rpe);
}