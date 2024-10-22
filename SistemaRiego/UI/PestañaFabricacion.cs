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
    public partial class PestañaFabricacion : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Analisis_de_equipo form1;
        Formulario formulario;
        public PestañaFabricacion(Analisis_de_equipo form1, Formulario form)
        {
            InitializeComponent();
            this.form1 = form1;
            formulario = form;
        }

        private void PestañaFabricacion_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            List<Formulario> list = new List<Formulario>();
            list.Add(formulario);
            dataGridView1.DataSource = list;
            List<Materiales> list2 = new List<Materiales>();
            list2.Add((Materiales)gestorFormularios.obtenerMaterialesSegunFormulario(formulario));
            dataGridView2.DataSource = list2;
            dataGridView3.DataSource = gestorFormularios.obtenerSensoresSegunFormulario(formulario);
            dataGridView4.DataSource = gestorFormularios.ObtenerTodosLosDispositivosAguaSegunFormulario(formulario);
        }
        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Usuario";
            label2.Tag = "Roll";
            label5.Tag = "Lenguaje";
            button3.Tag = "Volver";

            label6.Tag = "Formulario";
            label7.Tag = "Materiales";
            label8.Tag = "Sensores";
            label9.Tag = "DipositivosAgua";
            button1.Tag = "Cambiar estado a fabricacion";
            button2.Tag = "Cambiar estado a terminado";


            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button3);

            objetosConTag.Add(label6);
            objetosConTag.Add(label7);
            objetosConTag.Add(label8);
            objetosConTag.Add(label9);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
        }

        private bool isInitializingComboBox = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.actualizarFormularios();
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

        private void button1_Click(object sender, EventArgs e)
        {
            gestorFormularios.CargarEstadoInstalacion(formulario,"en fabricacion");
            formulario.estadoFrabricacion = "en fabricacion";
            List<Formulario> list = new List<Formulario>();
            list.Add(formulario);
            dataGridView1.DataSource = list;
            MessageBox.Show("estado cambiado a en fabricacion");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gestorFormularios.CargarEstadoInstalacion(formulario, "terminado");
            formulario.estadoFrabricacion = "terminado";
            List<Formulario> list = new List<Formulario>();
            list.Add(formulario);
            dataGridView1.DataSource = list;
            MessageBox.Show("estado cambiado a terminado");
        }
    }
}
