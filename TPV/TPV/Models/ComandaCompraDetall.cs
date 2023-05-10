using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class ComandaCompraDetall
{
    public int IdComandaCompra { get; set; }

    public int IdArticle { get; set; }

    public double QuantitatDemanada { get; set; }

    public double QuantitatServida { get; set; }

    public int IdProveidor { get; set; }

    public virtual Article IdArticleNavigation { get; set; } = null!;

    public virtual ComandaCompra IdComandaCompraNavigation { get; set; } = null!;

    public virtual Proveidor IdProveidorNavigation { get; set; } = null!;
}
