using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class AlbaraVendaDetall
{
    public int IdAlbaraVenda { get; set; }

    public int IdArticle { get; set; }

    public double Quantitat { get; set; }

    public virtual AlbaraVendum IdAlbaraVendaNavigation { get; set; } = null!;

    public virtual Article IdArticleNavigation { get; set; } = null!;
}
