using GrindCore.Domain.Interfaces;

namespace GrindCore.Domain.Services;

public class CalculatorFactory : ICalculatorFactory
{
    public ICalculationStrategy GetStrategy(string formulaType)
    {
        // Aquí centralizas la lógica de creación
        return formulaType.ToLower() switch
        {
            "epley" => new EpleyRpeStrategy(),
            // Cuando crees Lander, solo agregas: "lander" => new LanderRpeStrategy(),
            _ => new EpleyRpeStrategy() 
        };
    }
}