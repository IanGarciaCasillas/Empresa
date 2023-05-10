using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class Article
{
    
    public int IdArticle { get; set; }

    public string NomArticle { get; set; } = null!;

    public string DescripcioArticle { get; set; } = null!;

    public double PreuVenta { get; set; }

    public string TipusUnitat { get; set; } = null!;

    public double Stock { get; set; }

    public double MinimStock { get; set; }

    public double AutoStock { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdProveidorHabitual { get; set; }

    public byte[] FotoArticle { get; set; } = null!;

    public double IvaAplicar { get; set; }

    public int NumVenda { get; set; }

    public virtual ICollection<AlbaraCompraDetall> AlbaraCompraDetalls { get; set; } = new List<AlbaraCompraDetall>();

    public virtual ICollection<AlbaraVendaDetall> AlbaraVendaDetalls { get; set; } = new List<AlbaraVendaDetall>();

    public virtual ICollection<ComandaCompraDetall> ComandaCompraDetalls { get; set; } = new List<ComandaCompraDetall>();

    public virtual ICollection<ComandaVendaDetall> ComandaVendaDetalls { get; set; } = new List<ComandaVendaDetall>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Proveidor? IdProveidorHabitualNavigation { get; set; }

    public virtual ICollection<PreuArticleProveidor> PreuArticleProveidors { get; set; } = new List<PreuArticleProveidor>();

    public virtual ICollection<TicketDetall> TicketDetalls { get; set; } = new List<TicketDetall>();




    public override string ToString()
    {
        return $"{IdArticle}: {NomArticle}, {PreuVenta}";
    }
}
