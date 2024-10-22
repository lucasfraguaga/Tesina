using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Pedido : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Venta form1;
        public Pedido(Venta form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void Pedido_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            dataGridView1.DataSource = gestorFormularios.ObtenerTodosLosFormularios();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.Show();
            this.Close();
        }
        public void actualizarFormularios()
        {
            dataGridView1.DataSource = gestorFormularios.ObtenerTodosLosFormularios();
        }
        public void llenarComboLenguaje()
        {
            List<LenguajeMenu> list = new List<LenguajeMenu>();
            list = conexion.GetLenguages();

            isInitializingComboBox = false;
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "LanguageName";
            comboBox1.ValueMember = "LanguageId";
            LenguajeMenu defaultLanguage = ObserverLenguaje.GetLenguaje.idioma;

            // Buscar el índice del idioma por defecto en la lista
            int index = list.FindIndex(l => l.languageId == defaultLanguage.languageId);

            // Seleccionar el idioma por su índice si se encontró
            if (index >= 0)
            {
                comboBox1.SelectedIndex = index;
            }
            isInitializingComboBox = true;
            tag();
            gestorIdiomas.cambiarIdioma(objetosConTag);
        }
        private bool isInitializingComboBox = true;
        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Usuario";
            label2.Tag = "Roll";
            button7.Tag = "Volver";
            label5.Tag = "Lenguaje";
            button1.Tag = "Crear formulario";
            button2.Tag = "Cargar formulario";     
            
            button6.Tag = "Pedido creacion de usuario";
            button3.Tag = "Pagos de formulario";
            button4.Tag = "Cargar fecha instalacion";

            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
            objetosConTag.Add(button4);
            objetosConTag.Add(button6);
            objetosConTag.Add(button7);
            objetosConTag.Add(button3);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearFormulario frm = new CrearFormulario(this);
            frm.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la primera fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];

                // Obtener el objeto 'Formulario' de la fila seleccionada
                Formulario formulario = (Formulario)selectedRow.DataBoundItem;

                // Crear una nueva instancia de CargarFormulario y pasar el formulario seleccionado
                PedidoCreacionUsuario frm = new PedidoCreacionUsuario(this, formulario);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un formulario de la lista.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Asegúrate de que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la primera fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];

                // Obtener el objeto 'Formulario' de la fila seleccionada
                Formulario formulario = (Formulario)selectedRow.DataBoundItem;

                // Crear una nueva instancia de CargarFormulario y pasar el formulario seleccionado
                VendedorFecha frm = new VendedorFecha(this, formulario);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un formulario de la lista.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Asegúrate de que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la primera fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];

                // Obtener el objeto 'Formulario' de la fila seleccionada
                Formulario formulario = (Formulario)selectedRow.DataBoundItem;

                // Crear una nueva instancia de CargarFormulario y pasar el formulario seleccionado
                PagosFormulario frm = new PagosFormulario(this, formulario);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un formulario de la lista.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Asegúrate de que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la primera fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];

                // Obtener el objeto 'Formulario' de la fila seleccionada
                Formulario formulario = (Formulario)selectedRow.DataBoundItem;

                // Crear una nueva instancia de CargarFormulario y pasar el formulario seleccionado
                CargarFormulario frm = new CargarFormulario(this, formulario);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un formulario de la lista.");
            }
        }
    }
}
