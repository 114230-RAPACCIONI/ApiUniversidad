using System;
using System.Collections.Generic;

namespace ApiUniversidad.Models;

public partial class AlumnosPorCurso
{
    public Guid Id { get; set; }

    public Guid IdCurso { get; set; }

    public Guid IdAlumno { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;

    public virtual Curso IdCursoNavigation { get; set; } = null!;
}
