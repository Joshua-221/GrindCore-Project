using GrindCore.Domain.Interfaces;
using GrindCore.Models;
using GrindCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GrindCore.Controllers;

[ApiController]
[Route("api/athletes")]
//[ServiceFilter(typeof(ApiAuthFilter))]
public class AthletesApiController : ControllerBase
{
    private readonly IAthleteRepository _repository;

    public AthletesApiController(IAthleteRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Athlete>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Athlete> GetById(int id)
    {
        var athlete = _repository.GetById(id);
        if (athlete == null)
        {
            return NotFound();
        }

        return Ok(athlete);
    }

    [HttpPost]
    public ActionResult<Athlete> Create([FromBody] Athlete athlete)
    {
        if (string.IsNullOrWhiteSpace(athlete.Name))
        {
            return BadRequest(new { message = "Name is required." });
        }

        _repository.AddAthlete(athlete);
        return CreatedAtAction(nameof(GetById), new { id = athlete.Id }, athlete);
    }
}
