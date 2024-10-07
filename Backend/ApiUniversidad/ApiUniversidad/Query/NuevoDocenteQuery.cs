namespace ApiUniversidad.Query;

public class NuevoDocenteQuery
{
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; } 

    public string Legajo { get; set; } 

    public Guid IdRol { get; set; }

    public DateTime FechaAlta { get; set; }
}