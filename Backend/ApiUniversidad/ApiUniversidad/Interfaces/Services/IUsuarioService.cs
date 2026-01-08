using ApiUniversidad.Dtos;
using ApiUniversidad.Query;
using ApiUniversidad.Response;

namespace ApiUniversidad.Interfaces.Services;

public interface IUsuarioService
{
    Task<ApiResponse<LoginDto>> LoginAsync(LoginUsuarioQuery loginQuery);
}
