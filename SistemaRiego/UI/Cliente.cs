using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Cliente : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Form1 form1;
        Formulario formulario;
        public Cliente(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView1.DataSource = conexion.ObtenerMensajesPorCliente((BLL.BLLSesionManager.GetInstance).Usuario.Id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            BLL.BLLSesionManager.logaut();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContactoAdministrador form3 = new ContactoAdministrador(this);
            form3.Show();
            this.Hide();
        }
    }
}
