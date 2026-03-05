using System;
using System.Collections.Generic;

namespace DL;

public partial class VwUsuarioGetAll
{
    public int IdUsuario { get; set; }

    public string UsuarioNombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public bool? Estatus { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Curp { get; set; }

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public byte[]? Imagen { get; set; }

    public string NombreRol { get; set; } = null!;

    public string Calle { get; set; } = null!;

    public string NumeroExterior { get; set; } = null!;

    public string? NumeroInterior { get; set; }

    public string ColoniaNombre { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public string MunicipioNombre { get; set; } = null!;

    public string EstadoNombre { get; set; } = null!;
}
