using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    
        public class UsuarioGetAllDTO
        {
            public int IdUsuario { get; set; }

            public string UsuarioNombre { get; set; } = null!;

            public string ApellidoPaterno { get; set; } = null!;

            public string? ApellidoMaterno { get; set; }

            public DateTime FechaNacimiento { get; set; }

            public string UserName { get; set; } = null!;

            public string Password { get; set; } = null!;

            public string Email { get; set; } = null!;

            public string Sexo { get; set; } = null!;

            public string Telefono { get; set; } = null!;

            public string? Celular { get; set; }

            public bool? Estatus { get; set; }

            public string? CURP { get; set; }

            public byte[]? Imagen { get; set; }

            public string RolNombre { get; set; } = null!;
        }
        public class UsuarioGetByIdDTO
        {
            public int IdUsuario { get; set; }

            public string UsuarioNombre { get; set; } = null!;

            public string ApellidoPaterno { get; set; } = null!;

            public string? ApellidoMaterno { get; set; }

            public DateTime FechaNacimiento { get; set; }

            public string UserName { get; set; } = null!;

            public string Password { get; set; } = null!;

            public string Email { get; set; } = null!;

            public string Sexo { get; set; } = null!;

            public string Telefono { get; set; } = null!;
            public string? Celular { get; set; }
            public bool? Estatus { get; set; }
            public string? CURP { get; set; }
            public byte[]? Imagen { get; set; }
            public int? IdRol { get; set; }
            public int? IdDireccion { get; set; }
            public string? Calle { get; set; }
            public string? NumeroExterior { get; set; }
            public string? NumeroInterior { get; set; }
            public int? IdColonia { get; set; }
            public string? CodigoPostal { get; set; }
            public int? IdMunicipio { get; set; }
            public int? IdEstado { get; set; }
        }
    
}
