using GrindCore.Domain.Services;
using GrindCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GrindCore.Controllers;

[ApiController]
[Route("api/calculator")]
// [ServiceFilter(typeof(ApiAuthFilter))] 
public class CalculatorApiController : ControllerBase
{
    [HttpGet("1rm")]
    public ActionResult Calculate([FromQuery] double weight, [FromQuery] int reps, [FromQuery] int rpe)
    {
        // Validaciones
        if (weight <= 0) return BadRequest(new { message = "Weight must be greater than zero." });
        if (reps < 1) return BadRequest(new { message = "Reps must be at least 1." });
        if (rpe < 1 || rpe > 10) return BadRequest(new { message = "RPE must be between 1 and 10." });

        // Llamada al servicio
        var oneRepMax = OneRepMaxCalculator.CalculateWithRpe(weight, reps, rpe);

        return Ok(new
        {
            weight,
            reps,
            rpe,
            oneRepMax = Math.Round(oneRepMax, 2)
        });
    }
}