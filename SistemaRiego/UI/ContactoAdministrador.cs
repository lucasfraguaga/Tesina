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
    public partial class ContactoAdministrador : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Cliente form1;
        public ContactoAdministrador(Cliente form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void ContactoAdministrador_Load(object sender, EventArgs e)
        {
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView1.DataSource = conexion.ObtenerTareasPorUsuario((BLL.BLLSesionManager.GetInstance).Usuario.Id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.InsertarTareaSinAdministrador((BLL.BLLSesionManager.GetInstance).Usuario.Id,textBox1.Text,textBox2.Text,"creado");
            MessageBox.Show("Mensaje creado correctamente");
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = conexion.ObtenerTareasPorUsuario((BLL.BLLSesionManager.GetInstance).Usuario.Id);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
