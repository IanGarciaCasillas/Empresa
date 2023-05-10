using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NomCategoria { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
