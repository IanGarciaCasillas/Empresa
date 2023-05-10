using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class Ticket
{
    public int IdTicket { get; set; }

    public int NumDocument { get; set; }

    public DateTime DataTicket { get; set; }

    public int? IdClient { get; set; }

    public int? IdComanda { get; set; }

    public int? IdAlbara { get; set; }

    public virtual AlbaraVendum? IdAlbaraNavigation { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual ComandaVendum? IdComandaNavigation { get; set; }

    public virtual ICollection<TicketDetall> TicketDetalls { get; set; } = new List<TicketDetall>();
}
