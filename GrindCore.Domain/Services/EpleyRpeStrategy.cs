using GrindCore.Domain.Services;
using GrindCore.Domain.Interfaces;

namespace GrindCore.Domain.Services;

public class EpleyRpeStrategy : ICalculationStrategy
{
    public double Calculate(double weight, int reps, int rpe)
    {
        double epley1RM = weight * (1 + reps / 30.0);
        
        double rpeFactor = rpe switch
        {
            10 => 1.00,
            9  => 0.99,
            8  => 0.96,
            7  => 0.93,
            6  => 0.90,
            _  => 0.85
        };

        return epley1RM * rpeFactor;
    }
}