using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;

namespace ApiUniversidad.Interfaces.Services;

public interface IDocenteService
{
    Task<ApiResponse<List<DocenteDto>>> GetAllDocentes();
    Task<ApiResponse<DocenteDto>> GetDocenteById(Guid id);
    Task<ApiResponse<DocenteDto>> CreateDocente(NuevoDocenteQuery nuevoDocente);
    Task<ApiResponse<DocenteDto>> UpdateDocente(Guid id, NuevoDocenteQuery nuevoDocenteQuery);
    Task<ApiResponse<HttpStatusCode>> DeleteDocente(Guid id);
}
