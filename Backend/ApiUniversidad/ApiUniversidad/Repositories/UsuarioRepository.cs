using ApiUniversidad.Interfaces;
using ApiUniversidad.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversidad.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UniversidadContext _context;

    public UsuarioRepository(UniversidadContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
    {
        return await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> GetUsuarioByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(u => u.Email == email && u.Contrasena == password);
    }

    public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
    {
        if (usuario == null)
        {
            throw new Exception("Usuario es nulo");
        }

        await _context.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }
}
