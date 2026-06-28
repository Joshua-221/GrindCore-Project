using GrindCore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrindCore.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthApiController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!_authService.ValidateCredentials(request.Username, request.Password))
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        HttpContext.Session.SetString("IsAuthenticated", "true");
        HttpContext.Session.SetString("Username", request.Username);

        return Ok(new { message = "Authenticated." });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new { message = "Logged out." });
    }
}

public record LoginRequest(string Username, string Password);
