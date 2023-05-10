using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class TicketDetall
{
    public int IdTicket { get; set; }

    public int NumDocument { get; set; }

    public int IdArticle { get; set; }

    public double? Quantitat { get; set; }

    public double? PreuArticle { get; set; }

    public double? IvaAplicar { get; set; }

    public double? Descompte { get; set; }

    public virtual Article IdArticleNavigation { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
