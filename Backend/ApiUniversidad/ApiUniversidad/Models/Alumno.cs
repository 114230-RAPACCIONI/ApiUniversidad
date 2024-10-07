using System;
using System.Collections.Generic;

namespace ApiUniversidad.Models;

public partial class Alumno
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Legajo { get; set; } = null!;

    public Guid IdRol { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual ICollection<AlumnosPorCurso> AlumnosPorCursos { get; set; } = new List<AlumnosPorCurso>();

    public virtual Role IdRolNavigation { get; set; } = null!;
}
