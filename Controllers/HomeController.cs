using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using prueba.Permisos;
using prueba.Models;

namespace prueba.Controllers
{
    //Registrindo el acceso a todas las vistas del controlador home sin estar autenticado como usuario
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [PermisosRol(Rol.Administrador)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [PermisosRol(Rol.Administrador)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SinPermisos()
        {
            ViewBag.Message = "Usten No tiene Permisos para acceder a esta página";

            return View();
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut(); //Cerrando la autenticación del user
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Acceso");
        }
    }
}