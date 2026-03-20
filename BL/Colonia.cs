using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        private readonly DL.AreyesDiciembreContext _context;
        public Colonia(DL.AreyesDiciembreContext context)
        {
            _context = context;
        }
        public ML.Result GetByIdMunicipio(int? IdMunicípio)
        {
            ML.Result result = new ML.Result();
            try
            {
                var listaColonias = _context.ColoniaGetByIdMunicipioDTO.FromSqlRaw($"EXEC ColoniaGetByIdMunicipio {IdMunicípio}").ToList();
                if (listaColonias.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var coloniaBD in listaColonias)
                    {
                        ML.Colonia colonia = new ML.Colonia();
                        colonia.IdColonia = coloniaBD.IdColonia;
                        colonia.Nombre = coloniaBD.Nombre;

                        result.Objects.Add(colonia);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception e)
            {
                result.Correct = true;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}
