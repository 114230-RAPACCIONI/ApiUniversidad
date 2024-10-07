using System;
using System.Collections.Generic;

namespace ApiUniversidad.Models;

public partial class DocentesPorCurso
{
    public Guid Id { get; set; }

    public Guid IdCurso { get; set; }

    public Guid IdDocente { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual Curso IdCursoNavigation { get; set; } = null!;

    public virtual Docente IdDocenteNavigation { get; set; } = null!;
}
