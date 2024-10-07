using ApiUniversidad.Models;

namespace ApiUniversidad.Interfaces;

public interface IRoleRepository
{
    Task<List<Role>> getAllRoles();
}