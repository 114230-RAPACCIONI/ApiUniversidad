namespace ApiUniversidad.Dtos;

public class CursoDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Horarios { get; set; }
    public Guid IdCarrera { get; set; }
    public string? NombreCarrera { get; set; }
}
