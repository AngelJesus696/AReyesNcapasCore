using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security;

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
                    var listaUsuarios = context.UsuarioGetAllDTO.FromSqlRaw("EXEC UsuarioGetAll").ToList();

                    if (listaUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var usuarioObj in listaUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();


                            usuario.IdUsuario = usuarioObj.IdUsuario;
                            usuario.Nombre = usuarioObj.UsuarioNombre;
                            usuario.ApellidoPaterno = usuarioObj.ApellidoPaterno;
                            usuario.ApellidoPaterno = usuarioObj.ApellidoPaterno;
                            usuario.FechaNacimiento = usuarioObj.FechaNacimiento;
                            usuario.UserName = usuarioObj.UserName;
                            usuario.Password = usuarioObj.Password;
                            usuario.Email = usuarioObj.Email;
                            usuario.Sexo = usuarioObj.Sexo;
                            usuario.Telefono = usuarioObj.Telefono;
                            usuario.Celular = usuarioObj.Celular;
                            usuario.CURP = usuarioObj.CURP;
                            usuario.Imagen = usuarioObj.Imagen;
                            usuario.Estatus = usuarioObj.Estatus;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.Nombre = usuarioObj.RolNombre;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result GetById(int? IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {
                    var Usuarios = context.UsuarioGetByIdDTO.FromSql($"EXEC UsuarioGetById @IdUsuario= {IdUsuario}")
                        .AsEnumerable()
                        .SingleOrDefault();

                    if (Usuarios != null)
                    {
                        result.Objects = new List<object>();

                        ML.Usuario usuario = new ML.Usuario();


                        usuario.IdUsuario = Usuarios.IdUsuario;
                        usuario.Nombre = Usuarios.UsuarioNombre;
                        usuario.ApellidoPaterno = Usuarios.ApellidoPaterno;
                        usuario.ApellidoMaterno = Usuarios.ApellidoMaterno;
                        usuario.FechaNacimiento = Usuarios.FechaNacimiento;
                        usuario.UserName = Usuarios.UserName;
                        usuario.Password = Usuarios.Password;
                        usuario.Email = Usuarios.Email;
                        usuario.Sexo = Usuarios.Sexo;
                        usuario.Telefono = Usuarios.Telefono;
                        usuario.Celular = Usuarios.Celular;
                        usuario.CURP = Usuarios.CURP;
                        usuario.Imagen = Usuarios.Imagen;
                        usuario.Estatus = Usuarios.Estatus;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = Usuarios.IdRol.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = Usuarios.IdDireccion;
                        usuario.Direccion.Calle = Usuarios.Calle;
                        usuario.Direccion.NumeroExterior = Usuarios.NumeroExterior;
                        usuario.Direccion.NumeroInterior = Usuarios.NumeroInterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = Usuarios.IdColonia;
                        usuario.Direccion.Colonia.CodigoPostal = Usuarios.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = Usuarios.IdMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Usuarios.IdEstado;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {
                    int filasAfectadas = context.Database.ExecuteSql($"EXEC UsuarioDelete @IdUsuario={IdUsuario}");

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puedo agregar";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {

                    var filasAfectadas = context.Database.ExecuteSqlInterpolated($"EXEC UsuarioAdd @Nombre={usuario.Nombre}, @ApellidoPaterno={usuario.ApellidoPaterno}, @ApellidoMaterno={usuario.ApellidoMaterno}, @FechadeNacimiento={usuario.FechaNacimiento}, @UserName={usuario.UserName}, @Password={usuario.Password},@Email={usuario.Email}, @Sexo={usuario.Sexo}, @Telefono={usuario.Telefono}, @Celular={usuario.Celular}, @Estatus={usuario.Estatus}, @Curp={usuario.CURP}, @Imagen={usuario.Imagen ?? Array.Empty<byte>()}, @IdRol={usuario.Rol?.IdRol}, @Calle={usuario.Direccion?.Calle}, @NumeroExterior={usuario.Direccion?.NumeroExterior}, @NumeroInterior={usuario.Direccion?.NumeroInterior}, @IdColonia={usuario.Direccion?.Colonia?.IdColonia}");

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puedo agregar";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {

                    var filasAfectadas = context.Database.ExecuteSql($"EXEC UsuarioUpdate @IdUsuario={usuario.IdUsuario}, @Nombre={usuario.Nombre}, @ApellidoPaterno={usuario.ApellidoPaterno}, @ApellidoMaterno={usuario.ApellidoMaterno}, @FechadeNacimiento={usuario.FechaNacimiento}, @UserName={usuario.UserName}, @Password={usuario.Password},@Email={usuario.Email}, @Sexo={usuario.Sexo}, @Telefono={usuario.Telefono}, @Celular={usuario.Celular}, @Estatus={usuario.Estatus}, @Curp={usuario.CURP}, @Imagen={usuario.Imagen ?? Array.Empty<byte>()}, @IdRol={usuario.Rol?.IdRol},@IdDireccion={usuario.Direccion?.IdDireccion}, @Calle={usuario.Direccion?.Calle}, @NumeroExterior={usuario.Direccion?.NumeroExterior}, @NumeroInterior={usuario.Direccion?.NumeroInterior}, @IdColonia={usuario.Direccion?.Colonia?.IdColonia}");

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puedo agregar";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}
