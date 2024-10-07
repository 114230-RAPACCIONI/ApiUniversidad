using System;
using System.Collections.Generic;

namespace ApiUniversidad.Models;

public partial class CarrerasUniversidad
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
