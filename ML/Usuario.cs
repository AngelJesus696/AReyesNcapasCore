using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Sexo { get; set; }

        public string? Telefono { get; set; }

        public string? Celular { get; set; }

        public bool Estatus { get; set; }
        
        public string? CURP { get; set; }
        
        public byte[]? Imagen { get; set; }
        
        public List<object>? Usuarios { get; set; }
        
        public object? UsuarioObject { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public ML.Rol? Rol { get; set; }
    }
}
