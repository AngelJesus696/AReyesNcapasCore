using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {
                    var parametros = new[]
                    {
                            new SqlParameter("@Calle",usuario.Direccion?.Calle),
                            new SqlParameter("@NumeroExterior",usuario.Direccion?.NumeroExterior),
                            new SqlParameter("@NumeroInterior",usuario.Direccion?.NumeroInterior),
                            new SqlParameter("@IdColonis",usuario.Direccion?.Colonia?.IdColonia),
                            new SqlParameter("@IdUsuario",usuario.IdUsuario)
                    };

                    int filasAfectadas = context.Database.ExecuteSqlRaw("EXEC DireccionADD @Calle, @NumeroExterior, @NumeroInterior, @IdColonis, @IdUsuario", parametros);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "No se pudo insertar la direccion";
                        result.Correct = false;
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
                    var parametros = new[]
                    {
                            new SqlParameter("@IdDireccion",usuario.Direccion?.IdDireccion),
                            new SqlParameter("@Calle",usuario.Direccion?.Calle),
                            new SqlParameter("@NumeroExterior",usuario.Direccion?.NumeroExterior),
                            new SqlParameter("@NumeroInterior",usuario.Direccion?.NumeroInterior),
                            new SqlParameter("@IdColonia",usuario.Direccion?.Colonia?.IdColonia)
                    };

                    int filasAfectadas = context.Database.ExecuteSqlRaw("EXEC DireccionUpdate @IdDireccion, @Calle, @NumeroExterior, @NumeroInterior, @IdColonia", parametros);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "No se pudo insertar la direccion";
                        result.Correct = false;
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
        public static ML.Result GetByIdDireccion(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AreyesDiciembreContext context =  new DL.AreyesDiciembreContext())
                {
                    var direccion = context.DireccionGetByIdDTO.FromSqlRaw($"EXEC DireccionGetById {IdDireccion}").AsEnumerable().SingleOrDefault();
                    if (direccion != null)
                    {
                        ML.Direccion direccion1 = new ML.Direccion();
                        direccion1.Colonia = new ML.Colonia();
                        direccion1.Colonia.Municipio = new ML.Municipio();
                        direccion1.Colonia.Municipio.Estado = new ML.Estado();

                        direccion1.NumeroExterior = direccion.NumeroExterior;
                        direccion1.Calle = direccion.Calle;
                        direccion1.NumeroInterior = direccion.NumeroInterior;

                        direccion1.Colonia.Nombre = direccion.ColoniaNombre;
                        direccion1.Colonia.IdColonia = direccion.IdColonia;
                        direccion1.Colonia.CodigoPostal = direccion.CodigoPostal;

                        direccion1.Colonia.Municipio.IdMunicipio = direccion.IdMunicipio;
                        direccion1.Colonia.Municipio.Nombre = direccion.MunicipioNombre;

                        direccion1.Colonia.Municipio.Estado.Nombre = direccion.EstadoNombre;
                        direccion1.Colonia.Municipio.Estado.IdEstado = direccion.IdEstado;

                        result.Object = direccion;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro la direccion";
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
        public static ML.Result Delete(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {
                    int filasAfectadas = context.Database.ExecuteSql($"EXEC DireccionDelete @IdDireccion={IdDireccion}");
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro la direccion";
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
