using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        private readonly DL.AreyesDiciembreContext _context;
        public Estado(DL.AreyesDiciembreContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                    var listaEstados = _context.Estados.FromSqlRaw("EXEC EstadoGetAll").ToList();

                    if (listaEstados.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var EstadoBD in listaEstados)
                        {
                            ML.Estado estado = new ML.Estado();
                            
                            estado.IdEstado = EstadoBD.IdEstado;
                            estado.Nombre = EstadoBD.Nombre;
                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Estados";
                    }
                
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
