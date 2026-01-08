namespace ApiUniversidad.Query;

public class NuevoCursoQuery
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = null!;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public string Horarios { get; set; } = null!;
    public Guid IdCarrera { get; set; }
}
