using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using prueba.Logica;
using prueba.Models;

using System.Data;
using System.Data.SqlClient;

namespace prueba.Controllers
{
    public class AccesoController : Controller
    {
        // static string cadena = "Data Source = .; Initial Catalog = prueba; Integrated Security = true";
        string cadena = "Data Source = .; Initial Catalog = SENASOFT; Integrated Security = true";
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            
            LO_Usuarios lo = new LO_Usuarios();
            Usuarios user = lo.EncontrarUsuario(correo, clave);

            if (user.Nombres != null)
            {
                FormsAuthentication.SetAuthCookie(user.Correo, false);
                Session["Usuario"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Correo o contraseña Incorrecta";
                return View();
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Registrar( Usuarios User)
        {
            bool registrado;
            string mensaje;

            if(User.Clave == User.confirmarClave)
            {
                User.Clave = User.confirmarClave;

            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", con);
                cmd.Parameters.AddWithValue("@Nombres", User.Nombres);
                cmd.Parameters.AddWithValue("@Correo", User.Correo);
                cmd.Parameters.AddWithValue("@Clave", User.Clave);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                
                cmd.ExecuteNonQuery();
                //UNa vez ejecuta el query con el comando
                //Ya tengo acceso a los parametros de salida de un procdimeinto de almacenado

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();

            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                return View();
            }



        }

       
    }
}