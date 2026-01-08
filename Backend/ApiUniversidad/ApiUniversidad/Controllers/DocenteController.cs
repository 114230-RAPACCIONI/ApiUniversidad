using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUniversidad.Controllers;

[ApiController]
[Authorize]
public class DocenteController : ControllerBase
{
    private readonly IDocenteService _docenteService;

    public DocenteController(IDocenteService docenteService)
    {
        _docenteService = docenteService;
    }

    [HttpGet("/docentes/getAllDocentes")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllDocentes()
    {
        var docentes = await _docenteService.GetAllDocentes();
        return Ok(docentes);
    }

    [HttpGet("/docentes/getDocenteById/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetDocenteById(Guid id)
    {
        var docente = await _docenteService.GetDocenteById(id);
        return Ok(docente);
    }

    [HttpPost("/docente/crearDocente")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<DocenteDto>> CrearDocente([FromBody] NuevoDocenteQuery nuevoDocente)
    {
        return _docenteService.CreateDocente(nuevoDocente);
    }

    [HttpPut("/docente/updateDocente/{id}")]
    [Authorize(Roles = "admin,docente")]
    public Task<ApiResponse<DocenteDto>> UpdateDocente(Guid id, [FromBody] NuevoDocenteQuery nuevoDocenteQuery)
    {
        return _docenteService.UpdateDocente(id, nuevoDocenteQuery);
    }

    [HttpDelete("/docente/deleteDocente/{id}")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<HttpStatusCode>> DeleteDocente(Guid id)
    {
        return _docenteService.DeleteDocente(id);
    }
}
