using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.Json.Serialization;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.Usuario _usuario;
        private readonly BL.Rol _rol;
        private readonly BL.Municipio _municipio;
        private readonly BL.Estado _estado;
        private readonly BL.Colonia _colonia;
        private readonly BL.Direccion _direccion;
        public UsuarioController(BL.Usuario usuario, BL.Rol rol, BL.Municipio municipio, BL.Estado estado, BL.Colonia colonia, BL.Direccion direccion)
        {
            _usuario = usuario;
            _rol = rol;
            _municipio = municipio;
            _estado = estado;
            _colonia = colonia;
            _direccion = direccion;
        }
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = _usuario.GetAll();
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

            ML.Result resultRoles = _rol.GetAll();
            if (resultRoles.Correct)
            {
                usuario.Rol.Roles = resultRoles.Objects;
            }
            ML.Result resultEstado = _estado.GetAll();
            if(resultEstado.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }
            if (IdUsuario > 0)
            { 
                ML.Result result = _usuario.GetById(IdUsuario);
                
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
        public IActionResult Formulario(ML.Usuario usuario, IFormFile inptimg)
        {
            if (inptimg != null && inptimg.Length > 0)
            {
                using (Stream stream = inptimg.OpenReadStream())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    usuario.Imagen = memoryStream.ToArray();
                }
            }
            if (usuario.IdUsuario > 0)
            {
                ML.Result result = _usuario.Update(usuario);
                if (!result.Correct)
                {
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                    ML.Result resultRoles = _rol.GetAll();
                    if (resultRoles.Correct)
                    {
                        usuario.Rol.Roles = resultRoles.Objects;
                    }
                    ML.Result resultEstado = _estado.GetAll();
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
                    ML.Result resultDireccion = _direccion.Add(usuario);
                    if (!resultDireccion.Correct)
                    {
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        ML.Result resultRoles = _rol.GetAll();
                        if (resultRoles.Correct)
                        {
                            usuario.Rol.Roles = resultRoles.Objects;
                        }
                        ML.Result resultEstado = _estado.GetAll();
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
                ML.Result result = _usuario.Add(usuario);
                if (!result.Correct)
                {
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    
                    ML.Result resultRoles = _rol.GetAll();
                    if (resultRoles.Correct)
                    {
                        usuario.Rol.Roles = resultRoles.Objects;
                    }
                    ML.Result resultEstado = _estado.GetAll();
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
                    ML.Result resultDireccion = _direccion.Add(usuario);
                    if (!resultDireccion.Correct)
                    {
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        ML.Result resultRoles = _rol.GetAll();
                        if (resultRoles.Correct)
                        {
                            usuario.Rol.Roles = resultRoles.Objects;
                        }
                        ML.Result resultEstado = _estado.GetAll();
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
            ML.Result resultRoles = _rol.GetAll();
            ML.Result resultEstado = _estado.GetAll();
            if (resultEstado.Correct)
            {
                direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }
            if (IdDireccion > 0)
            {
                ML.Result result = _direccion.GetByIdDireccion(IdDireccion.Value);
                if (result.Correct)
                {
                    direccion = (ML.Direccion)result.Object;
                    direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                }
            }
           
                return View(direccion);
        }
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = _usuario.Delete(IdUsuario);

            return RedirectToAction("GetAll");
        }
        public IActionResult DeleteDireccion(int IdDireccion, int IdUsuarioD)
        {
            ML.Result result = _direccion.Delete(IdDireccion);
            return RedirectToAction("Formulario", new { IdUsuario = IdUsuarioD });
        }

        public JsonResult MunicipioGetIdEstado(int IdEstado)
        {
            ML.Result result = _municipio.GetbyIdEstaddo(IdEstado);
            return Json(result);
        }
        public JsonResult ColoniaGetIdMunicipio(int IdMunicipio)
        {
            ML.Result result = _colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result);
        }
        public JsonResult DireccionGetById(int IdDireccion)
        {
            ML.Result result = _direccion.GetByIdDireccion(IdDireccion);
            return Json(result);
        }
        public JsonResult DireccionAdd(ML.Usuario usuario)
        {
            ML.Result result = _direccion.Add(usuario);
            return Json(result);
        }
        public JsonResult DireccionUpdate(ML.Usuario usuario)
        {
            ML.Result result = _direccion.Update(usuario);
            return Json(result);
        }
        public JsonResult UpdateStatus(int IdUsuario, bool Status)
        {
            ML.Result result = BL.Usuario.UpdateStatus(IdUsuario, Status);
            return Json(result);
        }

    }
}
