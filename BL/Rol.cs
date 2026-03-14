using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AreyesDiciembreContext context = new DL.AreyesDiciembreContext())
                {
                    var listaRoles = context.Rols.FromSqlRaw("EXEC RolGetAll").ToList();

                    if (listaRoles.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var RolBD in listaRoles)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.Nombre = RolBD.Nombre;
                            rol.IdRol = RolBD.IdRol;
                            result.Objects.Add(rol);
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
    }
}
