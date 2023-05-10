using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class Proveidor
{
    public int IdProveidor { get; set; }

    public string NomProveidor { get; set; } = null!;

    public string DireccioProveidor { get; set; } = null!;

    public string TelefonProveidor { get; set; } = null!;

    public decimal PreuCompra { get; set; }

    public virtual ICollection<AlbaraCompra> AlbaraCompras { get; set; } = new List<AlbaraCompra>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<ComandaCompraDetall> ComandaCompraDetalls { get; set; } = new List<ComandaCompraDetall>();

    public virtual ICollection<FacturaCompra> FacturaCompras { get; set; } = new List<FacturaCompra>();

    public virtual ICollection<PreuArticleProveidor> PreuArticleProveidors { get; set; } = new List<PreuArticleProveidor>();
}
