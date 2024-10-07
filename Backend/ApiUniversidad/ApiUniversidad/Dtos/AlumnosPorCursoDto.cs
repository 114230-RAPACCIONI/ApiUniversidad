namespace ApiUniversidad.Dtos;

public class AlumnosPorCursoDto
{
    public Guid Id { get; set; }

    public Guid IdCurso { get; set; }

    public Guid IdAlumno { get; set; }

    public DateTime FechaAlta { get; set; }
}