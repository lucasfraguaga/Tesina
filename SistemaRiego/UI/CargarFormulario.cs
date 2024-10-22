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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class CargarFormulario : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Pedido form1;
        Formulario forumularioMain;
        public CargarFormulario(Pedido form1,Formulario formulario)
        {
            InitializeComponent();
            this.form1 = form1;
            forumularioMain = formulario;
            checkBox2.Checked = true;
            checkBox2.Checked = false;
        }

        private void CargarFormulario_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            List<Formulario> list = new List<Formulario>();
            list.Add(forumularioMain);
            dataGridView1.DataSource = list;


            foreach (var item in gestorFormularios.CargarCultivosEnComboBox())
            {
                comboBox3.Items.Add(item);
            }
            if (comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
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

            label11.Tag = "Datos formulario";
            label6.Tag = "DescripcionAgua";
            label12.Tag = "DisponibilidadAgua";
            label7.Tag = "DescripcionZona";
            label8.Tag = "Direccion";
            label9.Tag = "Distancia a cubrir";
            label19.Tag = "DescripcionViabilidad";
            label10.Tag = "Cultivo cargados";
            label20.Tag = "Viabilidad";
            label16.Tag = "Formulario seleccionado";
            label13.Tag = "Descripcion cultivo";
            label14.Tag = "Tipo cultivo";
            label18.Tag = "Cantidad";
            label15.Tag = "Cultivos precargados";
            label17.Tag = "Cantidad";
            button1.Tag = "Actualizar formulario";
            button2.Tag = "Crear y cargar cultivo";
            button3.Tag = "Cargar cultivo";


            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button7);

            objetosConTag.Add(label11);
            objetosConTag.Add(label6);
            objetosConTag.Add(label12);
            objetosConTag.Add(label7);
            objetosConTag.Add(label8);
            objetosConTag.Add(label9);
            objetosConTag.Add(label19);
            objetosConTag.Add(label10);
            objetosConTag.Add(label20);
            objetosConTag.Add(label16);
            objetosConTag.Add(label13);
            objetosConTag.Add(label14);
            objetosConTag.Add(label18);
            objetosConTag.Add(label15);
            objetosConTag.Add(label17);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
            objetosConTag.Add(button3);

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Verificar si hay un elemento seleccionado en comboBox3
            if (comboBox3.SelectedItem != null)
            {
                Cultivo cultivoSeleccionado = (Cultivo)comboBox3.SelectedItem;
                cultivoSeleccionado.cantidad = (int)numericUpDown2.Value;
                // Verificar si el elemento ya está en comboBox2
                bool yaEnComboBox2 = false;
                foreach (Cultivo item in comboBox2.Items)
                {
                    if (item.id == cultivoSeleccionado.id)
                    {
                        yaEnComboBox2 = true;
                        break;
                    }
                }

                if (!yaEnComboBox2)
                {
                    // Agregar el elemento a comboBox2
                    comboBox2.Items.Add(cultivoSeleccionado);

                    // Opcional: Eliminar el elemento de comboBox3
                    // comboBox3.Items.Remove(cultivoSeleccionado);
                }
                else
                {
                    MessageBox.Show("Este cultivo ya ha sido agregado a la lista.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cultivo.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cultivo cultivo = new Cultivo();
            cultivo.descripcion = textBox4.Text;
            cultivo.tipo = textBox5.Text;
            cultivo.cantidad = (int)numericUpDown3.Value;
            cultivo.id = gestorFormularios.CargarCultivo(cultivo);
            comboBox2.Items.Add(cultivo);
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
            forumularioMain.descripcionAgua = textBox1.Text;
            forumularioMain.disponibilidadAgua = checkBox1.Checked;
            forumularioMain.desscipcionZona = textBox2.Text;
            forumularioMain.direccion = textBox3.Text;
            forumularioMain.distanciaCubrir = textBox6.Text;
            forumularioMain.descripcionViabilidad = textBox7.Text;
            forumularioMain.viabilidad = checkBox2.Checked;
            gestorFormularios.ActualizarFormulario(forumularioMain);
            foreach (var item in comboBox2.Items)
            {
                Cultivo cultivo = item as Cultivo;
                if (cultivo != null)
                {
                    gestorFormularios.CargarFormularioxCultivo(forumularioMain,cultivo);
                }
            }
            List<Formulario> list = new List<Formulario>();
            list.Add(forumularioMain);
            dataGridView1.DataSource = list;
            MessageBox.Show("Formulario e cultivos actualizados correctamente");
        }
    }
}
