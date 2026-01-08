using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;

namespace ApiUniversidad.Interfaces.Services;

public interface ICursoService
{
    Task<ApiResponse<List<CursoDto>>> GetAllCursos();
    Task<ApiResponse<CursoDto>> GetCursoById(Guid id);
    Task<ApiResponse<CursoDto>> CreateCurso(NuevoCursoQuery nuevoCurso);
    Task<ApiResponse<CursoDto>> UpdateCurso(Guid id, NuevoCursoQuery nuevoCursoQuery);
    Task<ApiResponse<HttpStatusCode>> DeleteCurso(Guid id);
    Task<ApiResponse<List<AlumnoDto>>> GetAlumnosByCurso(Guid idCurso);
    Task<ApiResponse<AlumnosPorCursoDto>> AsignarAlumnoACurso(AsignarAlumnoQuery asignarAlumnoQuery);
    Task<ApiResponse<HttpStatusCode>> QuitarAlumnoDeCurso(Guid idCurso, Guid idAlumno);
    Task<ApiResponse<DocentesPorCursoDto>> AsignarDocenteACurso(AsignarDocenteQuery asignarDocenteQuery);
    Task<ApiResponse<HttpStatusCode>> QuitarDocenteDeCurso(Guid idCurso, Guid idDocente);
}
