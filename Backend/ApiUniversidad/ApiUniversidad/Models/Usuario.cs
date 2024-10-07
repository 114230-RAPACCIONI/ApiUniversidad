using System;
using System.Collections.Generic;

namespace ApiUniversidad.Models;

public partial class Usuario
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public Guid IdRol { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;
}
