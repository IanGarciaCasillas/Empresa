using System;
using System.Collections.Generic;

namespace Servidor.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Dni { get; set; } = null!;

    public string NomClient { get; set; } = null!;

    public string Cognom1Client { get; set; } = null!;

    public string Cognom2Client { get; set; } = null!;

    public string CorreuClient { get; set; } = null!;

    public string ContrasenyaClient { get; set; } = null!;

    public string TelefonClient { get; set; } = null!;

    public string DireccioClient { get; set; } = null!;

    public string CodicPostal { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual ICollection<ComandaVendum>? ComandaVenda { get; set; } = new List<ComandaVendum>();

    public virtual ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}
