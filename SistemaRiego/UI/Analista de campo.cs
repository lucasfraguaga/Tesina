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
    public partial class Analista_de_campo : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Form1 form1;
        Formulario formulario;
        public Analista_de_campo(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void Analista_de_campo_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView1.DataSource = gestorFormularios.ObtenerTodosLosFormularios();
        }
        public void actualizarFormularios()
        {
            dataGridView1.DataSource = gestorFormularios.ObtenerTodosLosFormularios();
        }

        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Usuario";
            label2.Tag = "Roll";
            button4.Tag = "Desconectarse";
            label5.Tag = "Lenguaje";
            button5.Tag = "Seleccionar formulario";




            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button4);
            objetosConTag.Add(button5);
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

        private void button4_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            BLL.BLLSesionManager.logaut();
            MessageBox.Show("Sesion desconectada correctamente");
            form1.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Asegúrate de que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la primera fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];

                // Obtener el objeto 'Formulario' de la fila seleccionada
                formulario = (Formulario)selectedRow.DataBoundItem;
                ViabilidadCampo frm = new ViabilidadCampo(this, formulario);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un formulario de la lista.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
