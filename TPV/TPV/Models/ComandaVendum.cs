using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class ComandaVendum
{
    public int IdComanda { get; set; }

    public DateTime DataComanda { get; set; }

    public string EstatComandaVenda { get; set; } = null!;

    public int IdClient { get; set; }

    public virtual ICollection<ComandaVendaDetall> ComandaVendaDetalls { get; set; } = new List<ComandaVendaDetall>();

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
