using ApiUniversidad.Interfaces;
using ApiUniversidad.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversidad.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly UniversidadContext _context;

    public RoleRepository(UniversidadContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> getAllRoles()
    {
        return await _context.Roles.ToListAsync();
    }
}