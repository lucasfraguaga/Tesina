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
    public partial class FechasIntalador : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Instalador form1;
        public FechasIntalador(Instalador form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void FechasIntalador_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            dataGridView1.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("propuesto");
            dataGridView2.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("aceptado");
            dataGridView3.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("finalizado");
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
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

            label8.Tag = "Propuesto";
            label9.Tag = "Aceptado";
            label10.Tag = "Finalizado";
            button2.Tag = "Cambiar fecha propuesta";
            button3.Tag = "Cambiar a aceptado";
            button1.Tag = "Cambiar a finalizado";
            label6.Tag = "Descripcion";
            label7.Tag = "Fecha";


            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button7);

            objetosConTag.Add(label8);
            objetosConTag.Add(label9);
            objetosConTag.Add(label10);
            objetosConTag.Add(button2);
            objetosConTag.Add(button3);
            objetosConTag.Add(button1);
            objetosConTag.Add(label6);
            objetosConTag.Add(label7);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FechaInstalacion fecha = new FechaInstalacion();
            fecha.descripcion = textBox1.Text;
            fecha.fecha = monthCalendar1.SelectionRange.Start;
            fecha.estado = "propuesto";

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int idFormularioSeleccionado = Convert.ToInt32(selectedRow.Cells["id_Formulario"].Value);
                Formulario formulario = new Formulario();
                formulario.idFormulario = idFormularioSeleccionado;
                gestorFormularios.RecargarFechaInstalacion(formulario, fecha);
                MessageBox.Show("Fecha recargada correctamente en estado propuesto");
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna fila.");
            }

            dataGridView1.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("propuesto");
            dataGridView2.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("aceptado");
            dataGridView3.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("finalizado");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                int idFormularioSeleccionado = Convert.ToInt32(selectedRow.Cells["id_Formulario"].Value);
                Formulario formulario = new Formulario();
                formulario.idFormulario = idFormularioSeleccionado;
                gestorFormularios.ActualizarEstadoFecha(formulario, "finalizado");
                MessageBox.Show("Fecha de formulario cambiado a finalizado");
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna fila.");
            }

            dataGridView1.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("propuesto");
            dataGridView2.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("aceptado");
            dataGridView3.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("finalizado");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int idFormularioSeleccionado = Convert.ToInt32(selectedRow.Cells["id_Formulario"].Value);
                Formulario formulario = new Formulario();
                formulario.idFormulario = idFormularioSeleccionado;
                gestorFormularios.ActualizarEstadoFecha(formulario, "aceptado");
                MessageBox.Show("Fecha de formulario cambiado a aceptado");
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna fila.");
            }


            dataGridView1.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("propuesto");
            dataGridView2.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("aceptado");
            dataGridView3.DataSource = gestorFormularios.ObtenerFechasInstalacionPorEstado("finalizado");
        }
    }
}
