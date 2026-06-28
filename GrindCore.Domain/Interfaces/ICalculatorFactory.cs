using GrindCore.Domain.Interfaces;

namespace GrindCore.Domain.Interfaces;

public interface ICalculatorFactory
{
    ICalculationStrategy GetStrategy(string formulaType);
}