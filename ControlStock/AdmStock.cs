using BE_Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_Datos.Data.Entidades;

namespace ControlStock
{
    public partial class AdmStock : Form
    {
        public AdmStock()
        {
            InitializeComponent();
            dgv_listaprod.ColumnCount = 5;
            dgv_listaprod.Columns[0].HeaderText = "ProdId";
            dgv_listaprod.Columns[1].HeaderText = "Producto"; 
            dgv_listaprod.Columns[2].HeaderText = "Descripcion";
            dgv_listaprod.Columns[3].HeaderText = "Cantidad";
            dgv_listaprod.Columns[4].HeaderText = "Precio";
            LlenarDGV();
        }
        DataConexion dc = new DataConexion();
        DatosProductos objProd = new DatosProductos();

        private void AdmStock_Load(object sender, EventArgs e)
        {
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            string cod = txb_codprod.Text;
            string nombre = txb_nombreprod.Text;
            string descripcion = cb_descripcion.Text;
            string cantidad = txb_cantidad.Text;
            string precio = txb_precio.Text;
            string orden = "select * from Productos";
            SqlCommand tabprod = new SqlCommand(orden, dc.conexion);
            try
            {
                dc.Abrirconexion();
                orden = "select * from Productos where ProdId=" + cod;
                SqlCommand cmd = new SqlCommand(orden, dc.conexion);
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.Read()) //verifica si el codigo existe
                {
                    MessageBox.Show("El Codigo del Producto ya existe");
                    registro.Close();
                }
                else
                {
                    registro.Close();
                    objProd.abmProductos("Alta", cod, nombre, descripcion, cantidad, precio);
                    LlenarDGV();
                    Limpiar();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al Agregar el producto", ex);
            }
            
        }

        private void dgv_listaprod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Cod = txb_codprod.Text.Length;
            DataSet ds = new DataSet();
            Cod = Convert.ToInt32(dgv_listaprod.CurrentRow.Cells[0].Value);
            ds = objProd.ListaDeProductos(Cod.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                Ds_a_TxtBox(ds);
            }
        }
        private void LlenarDGV() //llena el data grid view
        {
            
            dgv_listaprod.Rows.Clear();

            DataSet ds = new DataSet();
            ds = objProd.ListaDeProductos("Todos");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Se muestra en la tabla

                    dgv_listaprod.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                    dgv_listaprod.Refresh();
                }
            }
            else
                MessageBox.Show("No hay Productos cargados en el sistema");
        }
        private void Limpiar()//limpia los texbox
        {
            txb_codprod.Text = string.Empty;
            txb_nombreprod.Text = string.Empty;
            txb_cantidad.Text = string.Empty;
            txb_precio.Text = string.Empty;
            cb_descripcion.Text = string.Empty;
        }
        private void Ds_a_TxtBox(DataSet ds)
        {
            txb_codprod.Text = ds.Tables[0].Rows[0]["ProdId"].ToString();
            txb_nombreprod.Text = ds.Tables[0].Rows[0]["Producto"].ToString();
            cb_descripcion.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
            txb_cantidad.Text = ds.Tables[0].Rows[0]["Cantidad"].ToString();
            txb_precio.Text = ds.Tables[0].Rows[0]["Precio"].ToString();
            txb_codprod.Enabled = false;
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            string cod = txb_codprod.Text;
            string nombre = txb_nombreprod.Text;
            string descripcion = cb_descripcion.Text;
            string cantidad = txb_cantidad.Text;
            string precio = txb_precio.Text;
            string orden = "select * from Productos";
            SqlCommand tabprod = new SqlCommand(orden, dc.conexion);
            try
            {
                dc.Abrirconexion();
                objProd.abmProductos("Modificar", cod, nombre, descripcion, cantidad, precio);
                LlenarDGV();
                Limpiar();
                txb_codprod.Enabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Modificar el producto", ex);
            }

        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            string cod = txb_codprod.Text;
            string nombre = txb_nombreprod.Text;
            string descripcion = cb_descripcion.Text;
            string cantidad = txb_cantidad.Text;
            string precio = txb_precio.Text;
            string orden = "select * from Productos";
            SqlCommand tabprod = new SqlCommand(orden, dc.conexion);

            try
            {
                dc.Abrirconexion();
                objProd.abmProductos("Eliminar", cod, nombre, descripcion, cantidad, precio);
                LlenarDGV();
                Limpiar();
                txb_codprod.Enabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Eliminar el producto", ex);
            }

        }
    }
}
