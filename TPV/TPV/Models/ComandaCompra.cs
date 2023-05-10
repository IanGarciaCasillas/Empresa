using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class ComandaCompra
{
    public int IdComandaCompra { get; set; }

    public DateTime DataComandaCompra { get; set; }

    public string Estat { get; set; } = null!;

    public virtual ICollection<AlbaraCompra> AlbaraCompras { get; set; } = new List<AlbaraCompra>();

    public virtual ICollection<ComandaCompraDetall> ComandaCompraDetalls { get; set; } = new List<ComandaCompraDetall>();
}
