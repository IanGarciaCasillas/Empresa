using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class PreuArticleProveidor
{
    public int IdArticle { get; set; }

    public int IdProveidor { get; set; }

    public double PreuCompra { get; set; }

    public virtual Article IdArticleNavigation { get; set; } = null!;

    public virtual Proveidor IdProveidorNavigation { get; set; } = null!;
}
