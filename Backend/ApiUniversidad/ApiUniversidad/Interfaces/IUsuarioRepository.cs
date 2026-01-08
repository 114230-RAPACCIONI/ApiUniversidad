using ApiUniversidad.Models;

namespace ApiUniversidad.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetUsuarioByEmailAsync(string email);
    Task<Usuario?> GetUsuarioByEmailAndPasswordAsync(string email, string password);
    Task<Usuario> CreateUsuarioAsync(Usuario usuario);
}
