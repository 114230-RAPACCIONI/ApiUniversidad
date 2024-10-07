namespace ApiUniversidad.Query;

public class NuevoAlumnoQuery
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Legajo { get; set; } = null!;

    public Guid IdRol { get; set; }

    public DateTime FechaAlta { get; set; }
    
}