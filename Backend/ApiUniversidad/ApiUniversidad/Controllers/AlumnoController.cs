using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiUniversidad.Controllers;

[ApiController]
public class AlumnoController : ControllerBase
{
    private readonly IAlumnoService _alumnoService;

    public AlumnoController(IAlumnoService alumnoService)
    {
        _alumnoService = alumnoService;
    }

    [HttpGet("/alumnos/getAllAlumnos")]
    public async Task<IActionResult> getAllAlumnos()
    {
        var alumnos = await _alumnoService.getAllAlumnos();
        return Ok(alumnos);
    }
    
    [HttpGet("/alumnos/getAlumnosById/{id}")]
    public async Task<IActionResult> getAlumnosById(Guid id)
    {
        var alumno = await _alumnoService.getAlumnoById(id);
        return Ok(alumno);
    }
    
    [HttpPost("/alumno/crearAlumno")]
    public Task<ApiResponse<AlumnoDto>> crearAlumno([FromBody] NuevoAlumnoQuery nuevoAlumno)
    {
        return _alumnoService.createAlumno(nuevoAlumno);
    }
    
    [HttpPut("/alumno/updateAlumno/{id}")]
    public Task<ApiResponse<AlumnoDto>> updateAlumno(Guid id, [FromBody] NuevoAlumnoQuery nuevoAlumnoQuery)
    {
        return _alumnoService.updateAlumno(id, nuevoAlumnoQuery);
    }

    [HttpDelete("/alumno/deleteAlumno/{id}")]
    public Task<ApiResponse<HttpStatusCode>> deleteAlumno(Guid id)
    {
        return _alumnoService.deleteAlumno(id);
    }
}