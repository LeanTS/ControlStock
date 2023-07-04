using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace BE_Datos.Data.Entidades
{
    public class DatosProductos
    {
        DataConexion dc = new DataConexion();

        public int abmProductos(string accion, string cod,string nomprod,string descripcion,string cantidad,string precio)
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Alta")
                orden = "insert into Productos values (" + cod + ",'" + nomprod + "','" + descripcion + "','" + cantidad + " kl/unidad','$" + precio + "');";


            if (accion == "Modificar")
                orden = "update Productos set Producto='" + nomprod + "', Descripcion='" + descripcion + "', Cantidad='"+ cantidad + " kl/unidad', Precio='" + precio + "' where ProdId = "+ cod + "; ";


            if (accion == "Eliminar")
                orden = "delete Productos where ProdId=" + cod;

            SqlCommand cmd = new SqlCommand(orden, dc.conexion);
            try
            {
                dc.Abrirconexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de guardar,borrar o modificar de Productos",e);
            }
            finally
            {
                dc.Cerrarconexion();
                cmd.Dispose();
            }
            return resultado;
        }
        public DataSet ListaDeProductos(string cual)
        {
            string orden = string.Empty;
            if (cual != "Todos")
            {
                orden = "Select * from Productos where ProdId = " + int.Parse(cual) + ";";
            }
            else
                orden = "select * from Productos;";
            SqlCommand cmd = new SqlCommand(orden, dc.conexion);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                dc.Abrirconexion();
                cmd.ExecuteNonQuery();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("Error al listar productos", e);
            }
            finally
            {
                dc.Cerrarconexion();
                cmd.Dispose();
            }
            return ds;
        }
    }
}
