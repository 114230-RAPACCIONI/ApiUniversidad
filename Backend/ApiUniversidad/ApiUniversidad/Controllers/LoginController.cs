using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiUniversidad.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public LoginController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioQuery loginQuery)
    {
        var response = await _usuarioService.LoginAsync(loginQuery);
        
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
