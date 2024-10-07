using System;
using System.Collections.Generic;

namespace ApiUniversidad.Models;

public partial class Curso
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string Horarios { get; set; } = null!;

    public Guid IdCarrera { get; set; }

    public virtual ICollection<AlumnosPorCurso> AlumnosPorCursos { get; set; } = new List<AlumnosPorCurso>();

    public virtual ICollection<DocentesPorCurso> DocentesPorCursos { get; set; } = new List<DocentesPorCurso>();

    public virtual CarrerasUniversidad IdCarreraNavigation { get; set; } = null!;
}
