using System;
using System.Collections.Generic;

namespace ApiUniversidad.Models;

public partial class Docente
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Legajo { get; set; } = null!;

    public Guid IdRol { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual ICollection<DocentesPorCurso> DocentesPorCursos { get; set; } = new List<DocentesPorCurso>();

    public virtual Role IdRolNavigation { get; set; } = null!;
}
