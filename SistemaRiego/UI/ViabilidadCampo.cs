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
    public partial class ViabilidadCampo : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Analista_de_campo form1;
        Formulario formulario;
        public ViabilidadCampo(Analista_de_campo form1, Formulario form)
        {
            InitializeComponent();
            this.form1 = form1;
            formulario = form;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.actualizarFormularios();
            form1.Show();
            this.Close();
        }
        private bool isInitializingComboBox = true;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
        }

        private void ViabilidadEquipo_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            List<Formulario> list = new List<Formulario>();
            list.Add(formulario);
            dataGridView1.DataSource = list;
            textBox1.Text = formulario.descripcionAgua;
            textBox2.Text = formulario.desscipcionZona;
            textBox3.Text = formulario.distanciaCubrir;
            textBox5.Text = formulario.descripcionViabilidad;
            dataGridView2.DataSource = gestorFormularios.ObtenerTodosCultivosXFormulario(formulario.idFormulario);
        }
        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Usuario";
            label2.Tag = "Roll";
            label5.Tag = "Lenguaje";
            button2.Tag = "Rechazar formulario";
            button3.Tag = "Aceptar formulario";
            button4.Tag = "Volver";

            label6.Tag = "DescripcionAgua";
            label7.Tag = "DescripcionZona";
            label8.Tag = "DistanciaCubrir";
            label11.Tag = "DescripcionViabilidad";
            label10.Tag = "Cultivos";
            label9.Tag = "DescripcionViabilidadEquipo";



            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button2);
            objetosConTag.Add(button3);
            objetosConTag.Add(button4);
            objetosConTag.Add(label6);
            objetosConTag.Add(label7);
            objetosConTag.Add(label8);
            objetosConTag.Add(label11);
            objetosConTag.Add(label10);
            objetosConTag.Add(label9);
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

        private void button3_Click(object sender, EventArgs e)
        {
            formulario.viabilidadEquipo = true;
            formulario.descripcionViabilidadEquipo = textBox4.Text;
            gestorFormularios.ActualizarViabilidadEquipo(formulario.idFormulario,textBox4.Text,true);
            MessageBox.Show("Viabilidad aceptada y cargada");
            List<Formulario> list = new List<Formulario>();
            list.Add(formulario);
            dataGridView1.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formulario.viabilidadEquipo = true;
            formulario.descripcionViabilidadEquipo = textBox4.Text;
            gestorFormularios.ActualizarViabilidadEquipo(formulario.idFormulario, textBox4.Text, false);
            MessageBox.Show("Viabilidad no aceptada y cargada");
            List<Formulario> list = new List<Formulario>();
            list.Add(formulario);
            dataGridView1.DataSource = list;
        }
    }
}
