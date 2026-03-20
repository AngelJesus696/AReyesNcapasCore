using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BL
{
    public class Municipio
    {
        private readonly DL.AreyesDiciembreContext _context;
        public Municipio(DL.AreyesDiciembreContext context)
        {
            _context = context;
        }
        public ML.Result GetbyIdEstaddo(int? IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                var listaMunicipios = _context.MunicipioByIdEstadoDTO.FromSqlRaw($"EXEC MunicipioGetByIdEstado {IdEstado}").ToList();

                if (listaMunicipios.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var MunicipioBD in listaMunicipios)
                    {
                        ML.Municipio municipio = new ML.Municipio();

                        municipio.IdMunicipio = MunicipioBD.IdMunicipio;
                        municipio.Nombre = MunicipioBD.Nombre;

                        result.Objects.Add(municipio);
                    }
                    result.Correct = true;
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
