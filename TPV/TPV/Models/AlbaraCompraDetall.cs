using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class AlbaraCompraDetall
{
    public int IdAlbaraCompra { get; set; }

    public int IdArticle { get; set; }

    public double Quantitat { get; set; }

    public virtual AlbaraCompra IdAlbaraCompraNavigation { get; set; } = null!;

    public virtual Article IdArticleNavigation { get; set; } = null!;
}
