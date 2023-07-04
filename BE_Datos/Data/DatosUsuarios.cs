using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE_Datos.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BE_Datos.Data
{
    public class DatosUsuarios : AplicationDbContext
    {
        DataConexion dc = new DataConexion();
        public int abmUsuarios(string accion, Usuarios objUsuario)
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Alta")
            {
                orden = "inser into Usuarios values (" + objUsuario.Id + ",'" + objUsuario.Usuario + "'.'" + objUsuario.Contraseña + "','" + objUsuario.Nombre + "','" + objUsuario.Apellido + "','" + objUsuario.Cargo + "');";

            }
            if (accion == "Modificar")
            {
                orden = "update Usuarios set Contraseña='" + objUsuario.Contraseña + "', Nombre='" + objUsuario.Nombre + "', Apellido='" + objUsuario.Apellido + "';";
            }
            SqlCommand cmd = new SqlCommand(orden, dc.conexion);

            try
            {
                dc.Abrirconexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de guardar,borrar o modificar de Producto", e);
            }
            finally
            {
                dc.Cerrarconexion();
                cmd.Dispose();
            }
            return resultado;

        }
    }

}
