using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {
                    var listaUsuarios = context.VwUsuarioGetAlls.FromSqlRaw("EXEC UsuarioGetAll").ToList();

                    if (listaUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var usuarioObj in listaUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            

                            usuario.IdUsuario = usuarioObj.IdUsuario;
                            //usuario.Nombre = usuarioObj.Nombre;
                            usuario.ApellidoPaterno = usuarioObj.ApellidoPaterno;
                            usuario.ApellidoPaterno = usuarioObj.ApellidoPaterno;
                            usuario.FechaNacimiento = usuarioObj.FechaNacimiento;
                            usuario.UserName = usuarioObj.UserName;
                            usuario.Password = usuarioObj.Password;
                            usuario.Email = usuarioObj.Email;
                            usuario.Sexo = usuarioObj.Sexo;
                            usuario.Telefono = usuarioObj.Telefono;
                            usuario.Celular = usuarioObj.Celular;
                            usuario.CURP = usuarioObj.Curp;
                            usuario.Imagen = usuarioObj.Imagen;
                            usuario.Rol = new ML.Rol();
                            //usuario.Rol.Nombre = usuarioObj.RolNombre;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                }
            }catch(Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }

    }
}
