namespace ApiUniversidad.Dtos;

public class DocentesPorCursoDto
{
    public Guid Id { get; set; }
    public Guid IdCurso { get; set; }
    public Guid IdDocente { get; set; }
    public DateTime FechaAlta { get; set; }
}
