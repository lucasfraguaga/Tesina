using BE;
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

namespace UI
{
    public partial class AnalisisContacto : Form
    {
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        Admin admin;
        public AnalisisContacto(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void AnalisisContacto_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = conexion.ObtenerTodasLasTareas();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin.llenarComboLenguaje();
            admin.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MapperTarea mensaje;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];

                mensaje = (MapperTarea)selectedRow.DataBoundItem;
                textBox1.Text = mensaje.tema;
                textBox2.Text = mensaje.descripcion;
                textBox3.Text = mensaje.estado;
                textBox4.Text = mensaje.idUsuario.ToString();
                label11.Text = mensaje.id.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un mensaje de la lista.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(label11.Text != "")
            {
                conexion.ActualizarEstadoTarea(int.Parse(label11.Text),"aceptado", (BLL.BLLSesionManager.GetInstance).Usuario.Id);
                MessageBox.Show("Estado cambiado correctamente a aceptado");
                textBox3.Text = "aceptado";
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = conexion.ObtenerTodasLasTareas();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label11.Text != "")
            {
                conexion.ActualizarEstadoTarea(int.Parse(label11.Text), "rechazado", (BLL.BLLSesionManager.GetInstance).Usuario.Id);
                MessageBox.Show("Estado cambiado correctamente a rechazado");
                textBox3.Text = "rechazado";
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = conexion.ObtenerTodasLasTareas();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label11.Text != "")
            {
                conexion.ActualizarEstadoTarea(int.Parse(label11.Text), "finalizado", (BLL.BLLSesionManager.GetInstance).Usuario.Id);
                MessageBox.Show("Estado cambiado correctamente a finalizado");
                textBox3.Text = "finalizado";
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = conexion.ObtenerTodasLasTareas();
            }
        }
    }
}
