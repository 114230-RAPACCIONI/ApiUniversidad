namespace ApiUniversidad.Dtos;

public class DocenteDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }

    public string Apellido { get; set; } 

    public string Legajo { get; set; }

    public RoleDto Role { get; set; }

    public DateTime FechaAlta { get; set; }
}