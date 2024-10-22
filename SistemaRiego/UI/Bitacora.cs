using BLL;
using Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;

namespace UI
{
    public partial class Bitacora : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        ManejadorEncriptado encriptado = new ManejadorEncriptado();
        Admin admin;
        public Bitacora(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }
        private void Bitacora_Load(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso a la pantalla bitacora");
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            List<BE.Bitacora> bitacora = conexion.listarBitacora();
            dataGridView1.DataSource = bitacora;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Vuelta a pantalla admin");
            admin.llenarComboLenguaje();
            admin.Show();
            this.Close();
        }
    }
}
