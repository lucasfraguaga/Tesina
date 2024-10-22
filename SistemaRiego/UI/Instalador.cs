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
    public partial class Instalador : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        private Form1 form1;
        public Instalador(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;

        }

        private void Instalador_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            label1.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            BLL.BLLSesionManager.logaut();
            MessageBox.Show("Sesion desconectada correctamente");
            form1.Show();
            this.Close();
        }
        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label5.Tag = "Usuario";
            label2.Tag = "Roll";
            button4.Tag = "Desconectarse";
            label3.Tag = "Lenguaje";

            button1.Tag = "Ver fechas instalacion";

            objetosConTag.Add(label3);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button4);
            objetosConTag.Add(button1);
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
            FechasIntalador frm = new FechasIntalador(this);
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
