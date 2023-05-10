using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string NomRol { get; set; } = null!;

    public virtual ICollection<Empl> Empls { get; set; } = new List<Empl>();
}
