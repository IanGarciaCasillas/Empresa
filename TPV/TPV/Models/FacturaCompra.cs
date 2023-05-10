using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class FacturaCompra
{
    public int IdFactura { get; set; }

    public DateTime Data { get; set; }

    public int IdProveidor { get; set; }

    public virtual Proveidor IdProveidorNavigation { get; set; } = null!;

    public virtual ICollection<AlbaraCompra> IdAlbaraCompras { get; set; } = new List<AlbaraCompra>();
}
