using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Response;

namespace ApiUniversidad.Services.Rol;

public class RolService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RolService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ApiResponse<List<RoleDto>>> getAllRoles()
    {
        var roles = await _roleRepository.getAllRoles();
        var rolesDto = roles.Select(r =>
            new RoleDto
            {
                Id = r.Id,
                Descripcion = r.Descripcion
            }).ToList();

        return new ApiResponse<List<RoleDto>>
        {
            Data = rolesDto
        };
    }
}