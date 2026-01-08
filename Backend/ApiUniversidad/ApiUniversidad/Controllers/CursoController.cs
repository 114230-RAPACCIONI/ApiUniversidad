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
public class CursoController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursoController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet("/cursos/getAllCursos")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllCursos()
    {
        var cursos = await _cursoService.GetAllCursos();
        return Ok(cursos);
    }

    [HttpGet("/cursos/getCursoById/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetCursoById(Guid id)
    {
        var curso = await _cursoService.GetCursoById(id);
        return Ok(curso);
    }

    [HttpPost("/curso/crearCurso")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<CursoDto>> CrearCurso([FromBody] NuevoCursoQuery nuevoCurso)
    {
        return _cursoService.CreateCurso(nuevoCurso);
    }

    [HttpPut("/curso/updateCurso/{id}")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<CursoDto>> UpdateCurso(Guid id, [FromBody] NuevoCursoQuery nuevoCursoQuery)
    {
        return _cursoService.UpdateCurso(id, nuevoCursoQuery);
    }

    [HttpDelete("/curso/deleteCurso/{id}")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<HttpStatusCode>> DeleteCurso(Guid id)
    {
        return _cursoService.DeleteCurso(id);
    }

    [HttpGet("/cursos/getAlumnosByCurso/{idCurso}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAlumnosByCurso(Guid idCurso)
    {
        var alumnos = await _cursoService.GetAlumnosByCurso(idCurso);
        return Ok(alumnos);
    }

    [HttpPost("/curso/asignarAlumno")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<AlumnosPorCursoDto>> AsignarAlumnoACurso([FromBody] AsignarAlumnoQuery asignarAlumnoQuery)
    {
        return _cursoService.AsignarAlumnoACurso(asignarAlumnoQuery);
    }

    [HttpDelete("/curso/quitarAlumno/{idCurso}/{idAlumno}")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<HttpStatusCode>> QuitarAlumnoDeCurso(Guid idCurso, Guid idAlumno)
    {
        return _cursoService.QuitarAlumnoDeCurso(idCurso, idAlumno);
    }

    [HttpPost("/curso/asignarDocente")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<DocentesPorCursoDto>> AsignarDocenteACurso([FromBody] AsignarDocenteQuery asignarDocenteQuery)
    {
        return _cursoService.AsignarDocenteACurso(asignarDocenteQuery);
    }

    [HttpDelete("/curso/quitarDocente/{idCurso}/{idDocente}")]
    [Authorize(Roles = "admin")]
    public Task<ApiResponse<HttpStatusCode>> QuitarDocenteDeCurso(Guid idCurso, Guid idDocente)
    {
        return _cursoService.QuitarDocenteDeCurso(idCurso, idDocente);
    }
}
