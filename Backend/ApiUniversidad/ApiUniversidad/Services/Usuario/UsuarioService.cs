using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;
using Microsoft.IdentityModel.Tokens;

namespace ApiUniversidad.Services.Usuario;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<ApiResponse<LoginDto>> LoginAsync(LoginUsuarioQuery loginQuery)
    {
        // Validar datos
        if (string.IsNullOrEmpty(loginQuery.Email) || string.IsNullOrEmpty(loginQuery.NombreUsuario))
        {
            return new ApiResponse<LoginDto>
            {
                Success = false,
                ErrorMessage = "Email y nombre de usuario son requeridos."
            };
        }

        // Buscar usuario por email y nombre de usuario (sin contraseña por ahora)
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(loginQuery.Email);

        if (usuario == null)
        {
            return new ApiResponse<LoginDto>
            {
                Success = false,
                ErrorMessage = "Usuario no encontrado."
            };
        }

        // Generar token JWT con todos los datos del usuario incluyendo el rol
        var token = GenerateJwtToken(usuario);

        var loginDto = new LoginDto
        {
            NombreUsuario = usuario.Email, // Usando email como nombre de usuario
            Email = usuario.Email,
            Token = token
        };

        return new ApiResponse<LoginDto>
        {
            Success = true,
            Data = loginDto
        };
    }

    private string GenerateJwtToken(Models.Usuario usuario)
    {
        var key = _configuration["JwtSettings:SecretKey"];
        var keyBytes = Encoding.UTF8.GetBytes(key!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Email),
            new Claim("IdRol", usuario.IdRol.ToString()),
            new Claim("NombreRol", usuario.IdRolNavigation.Nombre),
            new Claim("DescripcionRol", usuario.IdRolNavigation.Descripcion),
            new Claim("FechaAlta", usuario.FechaAlta.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
