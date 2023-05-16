using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class TicketDetall
{
    [JsonPropertyName("IdTicket")]
    public int IdTicket { get; set; }
    [JsonPropertyName("NumDocument")]
    public int NumDocument { get; set; }
    [JsonPropertyName("IdArticle")]
    public int IdArticle { get; set; }
    [JsonPropertyName("Quantitat")]
    public double? Quantitat { get; set; }
    [JsonPropertyName("PreuArticle")]
    public double? PreuArticle { get; set; }
    [JsonPropertyName("IvaAplicar")]
    public double? IvaAplicar { get; set; }
    [JsonPropertyName("Descompte")]
    public double? Descompte { get; set; }

    public virtual Article? IdArticleNavigation { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
