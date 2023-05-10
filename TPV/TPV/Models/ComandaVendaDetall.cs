using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class ComandaVendaDetall
{
    public int IdComandaVenda { get; set; }

    public int IdArticle { get; set; }

    public double QuantitatDemanada { get; set; }

    public double QuantitatServida { get; set; }

    public virtual Article IdArticleNavigation { get; set; } = null!;

    public virtual ComandaVendum IdComandaVendaNavigation { get; set; } = null!;
}
