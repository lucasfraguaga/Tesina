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
    public partial class AnalisisStock : Form
    {
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Analisis_de_equipo form1;
        public AnalisisStock(Analisis_de_equipo form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void AnalisisStock_Load(object sender, EventArgs e)
        {
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView1.DataSource = gestorFormularios.ObtenerTodosLosSensores();
            dataGridView2.DataSource = gestorFormularios.ObtenerTodosLosDispositivosAgua();
            dataGridView3.DataSource = gestorFormularios.ObtenerTodosLosEquipos();
            dataGridView4.DataSource = gestorFormularios.ObtenerMaterialesRequeridos();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.actualizarFormularios();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PedidoCompra frm = new PedidoCompra(this);
            frm.Show();
            this.Hide();
        }
    }
}
