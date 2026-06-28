using GrindCore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrindCore.Controllers;

[ApiController]
[Route("api/calculator")]
public class CalculatorApiController : ControllerBase
{
    private readonly ICalculationStrategy _calculationStrategy;

    /// <summary>
    /// Inyectamos la estrategia de cálculo a través del constructor.
    /// Esto desacopla el controlador de la implementación específica del cálculo.
    /// </summary>
    public CalculatorApiController(ICalculationStrategy calculationStrategy)
    {
        _calculationStrategy = calculationStrategy;
    }

    [HttpGet("1rm")]
    public ActionResult Calculate([FromQuery] double weight, [FromQuery] int reps, [FromQuery] int rpe)
    {
        // Validaciones de entrada
        if (weight <= 0) 
            return BadRequest(new { message = "Weight must be greater than zero." });
        
        if (reps < 1) 
            return BadRequest(new { message = "Reps must be at least 1." });
        
        if (rpe < 1 || rpe > 10) 
            return BadRequest(new { message = "RPE must be between 1 and 10." });

        // Ejecución de la estrategia inyectada
        // El controlador no conoce la fórmula interna, solo sabe que el objeto tiene el método Calculate.
        var result = _calculationStrategy.Calculate(weight, reps, rpe);

        return Ok(new
        {
            weight,
            reps,
            rpe,
            oneRepMax = Math.Round(result, 2)
        });
    }
}