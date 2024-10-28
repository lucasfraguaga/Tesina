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
    public partial class CrearFormulario : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Pedido form1;
        public CrearFormulario(Pedido form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void CrearFormulario_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
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

            label11.Tag = "Datos cliente";
            label6.Tag = "Nombre";
            label7.Tag = "Apellido";
            label8.Tag = "DNI";
            label9.Tag = "Mail";
            label10.Tag = "Telefono";
            button1.Tag = "Crear formulario";



            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button7);

            objetosConTag.Add(label11);
            objetosConTag.Add(label6);
            objetosConTag.Add(label7);
            objetosConTag.Add(label8);
            objetosConTag.Add(label9);
            objetosConTag.Add(label10);
            objetosConTag.Add(button1);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.actualizarFormularios();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BE.Cliente cliente = new BE.Cliente();
            cliente.nombre = textBox1.Text;
            cliente.apellido = textBox2.Text;
            cliente.dni = (int)numericUpDown1.Value;
            cliente.mail = textBox3.Text;
            cliente.telefono = (int)numericUpDown2.Value;
            gestorFormularios.CargarCliente(cliente);
            gestorFormularios.CrearFormulario();

            Formulario formulario = new Formulario();
            formulario = gestorFormularios.ObtenerUltimoFormulario();
            List<Formulario> formularios = new List<Formulario> { formulario };           
            dataGridView1.DataSource = formularios;
            MessageBox.Show("Formulario creado exitosamente");
        }
    }
}
