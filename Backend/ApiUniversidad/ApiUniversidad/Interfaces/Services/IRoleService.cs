using ApiUniversidad.Dtos;
using ApiUniversidad.Response;

namespace ApiUniversidad.Interfaces.Services;

public interface IRoleService
{
    Task<ApiResponse<List<RoleDto>>> getAllRoles();
}