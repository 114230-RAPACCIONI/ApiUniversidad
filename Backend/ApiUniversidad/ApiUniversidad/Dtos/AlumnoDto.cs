namespace ApiUniversidad.Dtos;

public class AlumnoDto
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Legajo { get; set; } = null!;

    public RoleDto Role { get; set; }

    public DateTime FechaAlta { get; set; }

}