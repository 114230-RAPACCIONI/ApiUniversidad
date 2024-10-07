using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Query;
using ApiUniversidad.Response;

namespace ApiUniversidad.Interfaces.Services;

public interface IAlumnoService
{
    Task<ApiResponse<List<AlumnoDto>>> getAllAlumnos();
    Task<ApiResponse<AlumnoDto>> getAlumnoById(Guid id);
    Task<ApiResponse<AlumnoDto>> createAlumno(NuevoAlumnoQuery nuevoAlumno);
    Task<ApiResponse<AlumnoDto>> updateAlumno(Guid id, NuevoAlumnoQuery nuevoAlumnoQueryUpdate);
    Task<ApiResponse<HttpStatusCode>> deleteAlumno(Guid id);
}