using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.Json.Serialization;

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
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result resultRoles = BL.Rol.GetAll();
            if (resultRoles.Correct)
            {
                usuario.Rol.Roles = resultRoles.Objects;
            }
            ML.Result resultEstado = BL.Estado.GetAll();
            if(resultEstado.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }
            if (IdUsuario > 0)
            { 
                ML.Result result = BL.Usuario.GetById(IdUsuario);
                
                if (result.Objects == null)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRoles.Objects;
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                }
                else
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRoles.Objects;
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Direcciones = result.Objects;
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                }
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
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                    ML.Result resultRoles = BL.Rol.GetAll();
                    if (resultRoles.Correct)
                    {
                        usuario.Rol.Roles = resultRoles.Objects;
                    }
                    ML.Result resultEstado = BL.Estado.GetAll();
                    if (resultEstado.Correct)
                    {
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    }
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Rol.Roles = resultRoles.Objects;
                    return View(usuario);
                }
                if(usuario.Direccion != null)
                {
                    ML.Result resultDireccion = BL.Direccion.Add(usuario);
                    if (!resultDireccion.Correct)
                    {
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        ML.Result resultRoles = BL.Rol.GetAll();
                        if (resultRoles.Correct)
                        {
                            usuario.Rol.Roles = resultRoles.Objects;
                        }
                        ML.Result resultEstado = BL.Estado.GetAll();
                        if (resultEstado.Correct)
                        {
                            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        }
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        usuario.Rol.Roles = resultRoles.Objects;
                        return View(usuario);
                    }
                }
            }
            else
            {
                ML.Result result = BL.Usuario.Add(usuario);
                if (!result.Correct)
                {
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    
                    ML.Result resultRoles = BL.Rol.GetAll();
                    if (resultRoles.Correct)
                    {
                        usuario.Rol.Roles = resultRoles.Objects;
                    }
                    ML.Result resultEstado = BL.Estado.GetAll();
                    if (resultEstado.Correct)
                    {
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    }
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Rol.Roles = resultRoles.Objects;
                    return View(usuario);
                }
                if (result.Object != null)
                {
                    usuario.IdUsuario = (int)result.Object;
                    ML.Result resultDireccion = BL.Direccion.Add(usuario);
                    if (!resultDireccion.Correct)
                    {
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        ML.Result resultRoles = BL.Rol.GetAll();
                        if (resultRoles.Correct)
                        {
                            usuario.Rol.Roles = resultRoles.Objects;
                        }
                        ML.Result resultEstado = BL.Estado.GetAll();
                        if (resultEstado.Correct)
                        {
                            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        }
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        usuario.Rol.Roles = resultRoles.Objects;
                        return View(usuario);
                    }
                }

            }
            return RedirectToAction("GetAll");
        }

        public IActionResult FormularioDireccion(int? IdDireccion)
        {
            ML.Direccion direccion = new ML.Direccion();
            direccion.Colonia = new ML.Colonia();
            direccion.Colonia.Municipio = new ML.Municipio();
            direccion.Colonia.Municipio.Estado = new ML.Estado();
            ML.Result resultRoles = BL.Rol.GetAll();
            ML.Result resultEstado = BL.Estado.GetAll();
            if (resultEstado.Correct)
            {
                direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }
            if (IdDireccion > 0)
            {
                ML.Result result = BL.Direccion.GetByIdDireccion(IdDireccion.Value);
                if (result.Correct)
                {
                    direccion = (ML.Direccion)result.Object;
                    direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                }
            }
           
                return View(direccion);
        }
        [HttpPost]
        public IActionResult FormularioDireccion(ML.Usuario usuario)
        {

            return RedirectToAction("Formulario",usuario.IdUsuario);
        }
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);

            return RedirectToAction("GetAll");
        }
        public IActionResult DeleteDireccion(int IdDireccion, int IdUsuarioD)
        {
            ML.Result result = BL.Direccion.Delete(IdDireccion);
            return RedirectToAction("Formulario", new { IdUsuario = IdUsuarioD });
        }

        public JsonResult MunicipioGetIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetbyIdEstaddo(IdEstado);
            return Json(result);
        }
        public JsonResult ColoniaGetIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result);
        }
        public JsonResult DireccionGetById(int IdDireccion)
        {
            ML.Result result = BL.Direccion.GetByIdDireccion(IdDireccion);
            return Json(result);
        }
        public JsonResult DireccionAdd(ML.Usuario usuario)
        {
            ML.Result result = BL.Direccion.Add(usuario);
            return Json(result);
        }
        public JsonResult DireccionUpdate(ML.Usuario usuario)
        {
            ML.Result result = BL.Direccion.Update(usuario);
            return Json(result);
        }

    }
}
