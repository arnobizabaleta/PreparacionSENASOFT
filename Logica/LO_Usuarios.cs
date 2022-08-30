using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using prueba.Models;

namespace prueba.Logica
{
    public class LO_Usuarios
    {
        //static string cadena = "Data Source = .; Initial Catalog = prueba; Integrated Security = true";
        string cadena = "Data Source = .; Initial Catalog = SENASOFT; Integrated Security = true";

        public Usuarios EncontrarUsuario(string correo, string clave)
        {
            Usuarios objeto = new Usuarios();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                string query = "SELECT  IdUsuario,Nombres, Correo, Clave, IdRol FROM usuarios WHERE Correo = @Correo and DBO.DESENCRIPTAR(Clave) = @Clave";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 500);
                cmd.Parameters["@Correo"].Value = correo;
                cmd.Parameters["@Clave"].Value = clave;
                cmd.CommandType = CommandType.Text;
                cn.Open();
                //int i = cmd.ExecuteNonQuery();
                //dr is like the registers of reading of executing the query
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objeto = new Usuarios()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombres = dr["Nombres"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            IdRol = (Rol)dr["IdRol"]

                        };
                    }
                }
            }

            return objeto;
        }


        /*
        public Usuarios EncontrarUsuario(string correo, string clave)
        {
            Usuarios objeto = new Usuarios();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                string query = "SELECT IdUsuario,Nombres, Correo, Clave, IdRol FROM usuarios WHERE Correo = @Correo and Clave = @Clave";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 500);
                cmd.Parameters["@Correo"].Value = correo;
                cmd.Parameters["@clave"].Value = clave;
                cmd.CommandType = CommandType.Text;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                //dr is like a registers of reading of executing the query
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objeto = new Usuarios()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombres = dr["Nombres"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            IdRol = (Rol)dr["IdRol"]

                        };
                    }
                }
            }

            return objeto;
        }

         */
    }
}