using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class AlbaraVendum
{
    public int IdAlbara { get; set; }

    public DateTime Data { get; set; }

    public virtual ICollection<AlbaraVendaDetall>? AlbaraVendaDetalls { get; set; } = new List<AlbaraVendaDetall>();

    public virtual ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}
