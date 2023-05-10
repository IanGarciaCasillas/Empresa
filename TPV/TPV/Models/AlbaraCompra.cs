using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class AlbaraCompra
{
    public int IdAlbaraCompra { get; set; }

    public DateTime Data { get; set; }

    public int IdProveidor { get; set; }

    public int? IdAlbaraProveidor { get; set; }

    public int IdComandaCompra { get; set; }

    public virtual ICollection<AlbaraCompraDetall> AlbaraCompraDetalls { get; set; } = new List<AlbaraCompraDetall>();

    public virtual ComandaCompra IdComandaCompraNavigation { get; set; } = null!;

    public virtual Proveidor IdProveidorNavigation { get; set; } = null!;

    public virtual ICollection<FacturaCompra> IdFacturaCompras { get; set; } = new List<FacturaCompra>();
}
