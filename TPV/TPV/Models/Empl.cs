using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Servidor.Models;

public partial class Empl
{
    
    public int IdEmpl { get; set; }

    public string NomEmpl { get; set; } = null!;

    public string Cognom1Empl { get; set; } = null!;

    public string Cognom2Empl { get; set; } = null!;

    public string DniEmpl { get; set; } = null!;

    public DateTime DataNaixEmpl { get; set; }

    public DateTime DataAltaEmpl { get; set; }

    public DateTime DataBaixaEmpl { get; set; }

    public string SexeEmpl { get; set; } = null!;

    public int TelefonEmpl { get; set; }

    public string NssEmpl { get; set; } = null!;

    public string DireccioEmpl { get; set; } = null!;

    public byte[] FotoEmpl { get; set; } = null!;

    public int IdRol { get; set; }

    public string UserEmpl { get; set; }

    public string PasswordEmpl { get; set; }

    public string JornadaEmpl { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
