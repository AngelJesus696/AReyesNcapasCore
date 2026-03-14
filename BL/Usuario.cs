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
                            usuario.ApellidoMaterno = usuarioObj.ApellidoMaterno;
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

                            var listaDIrecciones = context.DireccionByIdUsuarioDTO.FromSqlRaw($"EXEC DireccionByIdUsuario {usuarioObj.IdUsuario}").ToList();
                            if(listaDIrecciones.Count > 0)
                            {
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Direcciones = new List<object>();

                                foreach (var direccionObj in listaDIrecciones)
                                {
                                    ML.Direccion direccion= new ML.Direccion();
                                    direccion.Calle = direccionObj.Calle;
                                    direccion.NumeroExterior = direccionObj.NumeroExterior;
                                    direccion.NumeroInterior = direccionObj.NumeroInterior;

                                    direccion.Colonia = new ML.Colonia();
                                    direccion.Colonia.Nombre = direccionObj.ColoniaNombre;
                                    direccion.Colonia.CodigoPostal = direccionObj.CodigoPostal;

                                    direccion.Colonia.Municipio = new ML.Municipio();
                                    direccion.Colonia.Municipio.Nombre = direccionObj.MunicipioNombre;

                                    direccion.Colonia.Municipio.Estado = new ML.Estado();
                                    direccion.Colonia.Municipio.Estado.Nombre = direccionObj.EstadoNombre;

                                    usuario.Direccion.Direcciones.Add(direccion);
                                }
                            }
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

                        var listaDIrecciones = context.DireccionByIdUsuarioDTO.FromSqlRaw($"EXEC DireccionByIdUsuario {Usuarios.IdUsuario}").ToList();
                        if (listaDIrecciones.Count > 0)
                        {
                            usuario.Direccion = new ML.Direccion();
                            //usuario.Direccion.Direcciones = new List<object>();
                            result.Objects = new List<object>();

                            foreach (var direccionObj in listaDIrecciones)
                            {
                                
                                ML.Direccion direccion = new ML.Direccion();
                                direccion.IdDireccion = direccionObj.IdDireccion;
                                direccion.Calle = direccionObj.Calle;
                                direccion.NumeroExterior = direccionObj.NumeroExterior;
                                direccion.NumeroInterior = direccionObj.NumeroInterior;

                                direccion.Colonia = new ML.Colonia();
                                direccion.Colonia.Nombre = direccionObj.ColoniaNombre;
                                direccion.Colonia.CodigoPostal = direccionObj.CodigoPostal;

                                direccion.Colonia.Municipio = new ML.Municipio();
                                direccion.Colonia.Municipio.Nombre = direccionObj.MunicipioNombre;
                                direccion.Colonia.Municipio.IdMunicipio = direccionObj.IdMunicipio;

                                direccion.Colonia.Municipio.Estado = new ML.Estado();
                                direccion.Colonia.Municipio.Estado.Nombre = direccionObj.EstadoNombre;
                                direccion.Colonia.Municipio.Estado.IdEstado = direccionObj.IdEstado;

                                result.Objects.Add(direccion);
                            }
                        }
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

                    var IdusuarioOUTPUT = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    if(usuario.Estatus == null)
                    {
                        usuario.Estatus = true;
                    }
                    var parametros = new[]
                    {
                          new SqlParameter("@Nombre", usuario.Nombre),
                            new SqlParameter("@ApellidoPaterno", usuario.ApellidoPaterno),
                            new SqlParameter("@ApellidoMaterno", usuario.ApellidoMaterno),
                            new SqlParameter("@FechadeNacimiento", usuario.FechaNacimiento),
                            new SqlParameter("@UserName", usuario.UserName),
                            new SqlParameter("@Password", usuario.Password),
                            new SqlParameter("@Email", usuario.Email),
                            new SqlParameter("@Sexo", usuario.Sexo),
                            new SqlParameter("@Telefono", usuario.Telefono),
                            new SqlParameter("@Celular", usuario.Celular),
                            new SqlParameter("@Estatus", usuario.Estatus.Value ? 1 : 0),
                            new SqlParameter("@Curp", usuario.CURP),
                            new SqlParameter("@IdRol", usuario.Rol?.IdRol),
                            new SqlParameter("@Imagen", usuario.Imagen ?? Array.Empty<byte>()),
                            IdusuarioOUTPUT
                    };
                    context.Database.ExecuteSqlRaw("EXEC UsuariosAdd @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechadeNacimiento,@UserName, @Password, @Email, @Sexo, @Telefono, @Celular, @Estatus, @Curp, @IdRol, @Imagen, @IdUsuario OUTPUT", parametros);

                    if (IdusuarioOUTPUT != null)
                    {
                        result.Object = (int)IdusuarioOUTPUT.Value;
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
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {

                    var filasAfectadas = context.Database.ExecuteSql($"EXEC UsuarioUpdate @IdUsuario={usuario.IdUsuario}, @Nombre={usuario.Nombre}, @ApellidoPaterno={usuario.ApellidoPaterno}, @ApellidoMaterno={usuario.ApellidoMaterno}, @FechadeNacimiento={usuario.FechaNacimiento}, @UserName={usuario.UserName}, @Password={usuario.Password},@Email={usuario.Email}, @Sexo={usuario.Sexo}, @Telefono={usuario.Telefono}, @Celular={usuario.Celular}, @Estatus={usuario.Estatus}, @Curp={usuario.CURP}, @Imagen={usuario.Imagen ?? Array.Empty<byte>()}, @IdRol={usuario.Rol?.IdRol}");

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
