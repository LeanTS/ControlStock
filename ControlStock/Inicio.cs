using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_Datos;
using ControlStock;

namespace FE_Presentacion
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }
        DataConexion dc = new DataConexion();
        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            string Usuario = txb_usuario.Text;
            string Contra = txb_contraseña.Text;
            string orden = string.Empty;

            //validacion
            try
            {
                dc.Abrirconexion();
                orden = "select Id from Usuarios where Usuario='" + Usuario + "' and Contraseña='" + Contra + "'"; 
                SqlCommand cmd = new SqlCommand(orden, dc.conexion);
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.Read())
                {
                    AdmStock AS = new AdmStock();
                    Inicio ini = new Inicio();
                    ini.Hide();
                    AS.Show();
                    dc.Cerrarconexion();
                    cmd.Dispose();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrecta");
                }
            }

            catch (Exception a)
            {
                throw new Exception("Error al buscar usuario", a);
            }
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }



        public void VerificarUsuario()
        {

        }
    }


}
