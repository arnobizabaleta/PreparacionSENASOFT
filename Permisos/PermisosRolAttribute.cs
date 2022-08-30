using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prueba.Models;

namespace prueba.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        private Rol Id_Rol;
        public  PermisosRolAttribute(Rol _IdRol){
            Id_Rol = _IdRol;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                Usuarios usuario = HttpContext.Current.Session["Usuario"] as Usuarios;

                if (usuario.IdRol != this.Id_Rol)
                {
                    filterContext.Result = new RedirectResult("~/Home/SinPermisos");
                }
            }
            base.OnActionExecuting(filterContext);
        }




    }
}