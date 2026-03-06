using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        public IActionResult Formulario(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario > 0)
            { 

                ML.Result result = BL.Usuario.GetById(IdUsuario);
                usuario = (ML.Usuario)result.Object;
                
            }
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Formulario(ML.Usuario usuario)
        {
            if (usuario.IdUsuario > 0)
            {
                ML.Result result = BL.Usuario.Update(usuario);
                if (!result.Correct)
                {
                    return View(usuario);
                }
            }
            else
            {
                ML.Result result = BL.Usuario.Add(usuario);
                if (!result.Correct)
                {
                    return View(usuario);
                }
            }
            return RedirectToAction("GetAll");
        }
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);

            return RedirectToAction("GetAll");
        }
    }
}
