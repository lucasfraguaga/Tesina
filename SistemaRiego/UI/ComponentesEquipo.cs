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
    public partial class ComponentesEquipo : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Analisis_de_equipo form1;
        Formulario formulario;
        public ComponentesEquipo(Analisis_de_equipo form1, Formulario form)
        {
            InitializeComponent();
            this.form1 = form1;
            formulario = form;
        }
        private bool isInitializingComboBox = true;

        private void ComponentesEquipo_Load(object sender, EventArgs e)
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

            foreach (var item in gestorFormularios.ObtenerTodosLosSensores())
            {
                comboBox3.Items.Add(item);
            }
            if (comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
            foreach (var item in gestorFormularios.ObtenerTodosLosDispositivosAgua())
            {
                comboBox2.Items.Add(item);
            }
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }

        }
        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Usuario";
            label2.Tag = "Roll";
            label5.Tag = "Lenguaje";

            label6.Tag = "DescripcionAgua";
            label7.Tag = "DescripcionZona";
            label8.Tag = "DistanciaCubrir";
            label11.Tag = "DescripcionViabilidad";
            label10.Tag = "Cultivos";
            label13.Tag = "Descripcion sensor";
            label14.Tag = "Tipo sensor";
            label18.Tag = "Cantidad";
            label21.Tag = "Precio";
            label15.Tag = "Sensores precargados";
            label17.Tag = "Cantidad";
            label24.Tag = "Materiales cargados";
            label23.Tag = "Sensores cargados";
            label25.Tag = "Dispositivos de agua cargados";
            label26.Tag = "Cantidad equipos";
            label27.Tag = "Precio equipo";
            label28.Tag = "Conductor agua";
            label29.Tag = "Precio conductor agua";
            label30.Tag = "Comentarios";
            label20.Tag = "Descripcion agua";
            label19.Tag = "Tipo dispositivo agua";
            label9.Tag = "Cantidad";
            label22.Tag = "Precio";
            label16.Tag = "Dispositivos agua precargados";
            label12.Tag = "Cantidad";
            button1.Tag = "Cargar dipositivo agua";
            button2.Tag = "Crear y cargar sensor";
            button3.Tag = "Cargar sensor";
            button5.Tag = "Crear y cargar dipositivo agua";
            button6.Tag = "Cargar materiales";
            button4.Tag = "Volver";



            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);

            objetosConTag.Add(label6);
            objetosConTag.Add(label7);
            objetosConTag.Add(label8);
            objetosConTag.Add(label11);
            objetosConTag.Add(label10);
            objetosConTag.Add(label13);
            objetosConTag.Add(label14);
            objetosConTag.Add(label18);
            objetosConTag.Add(label21);
            objetosConTag.Add(label15);
            objetosConTag.Add(label17);
            objetosConTag.Add(label24);
            objetosConTag.Add(label23);
            objetosConTag.Add(label25);
            objetosConTag.Add(label26);
            objetosConTag.Add(label27);
            objetosConTag.Add(label28);
            objetosConTag.Add(label29);
            objetosConTag.Add(label30);
            objetosConTag.Add(label20);
            objetosConTag.Add(label19);
            objetosConTag.Add(label9);
            objetosConTag.Add(label22);
            objetosConTag.Add(label16);
            objetosConTag.Add(label12);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
            objetosConTag.Add(button3);
            objetosConTag.Add(button4);
            objetosConTag.Add(button5);
            objetosConTag.Add(label2);
            objetosConTag.Add(button6);

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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.actualizarFormularios();
            form1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Verificar si hay un elemento seleccionado en comboBox3
            if (comboBox3.SelectedItem != null)
            {
                Sensor sensorSeleccionado = (Sensor)comboBox3.SelectedItem;
                sensorSeleccionado.cantidad = (int)numericUpDown2.Value;
                // Verificar si el elemento ya está en comboBox2
                bool yaEnComboBox2 = false;
                foreach (Sensor item in comboBox4.Items)
                {
                    if (item.id == sensorSeleccionado.id)
                    {
                        yaEnComboBox2 = true;
                        break;
                    }
                }

                if (!yaEnComboBox2)
                {
                    // Agregar el elemento a comboBox2
                    comboBox4.Items.Add(sensorSeleccionado);

                }
                else
                {
                    MessageBox.Show("Este sensor ya ha sido agregado a la lista.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un sensor.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar si hay un elemento seleccionado en comboBox3
            if (comboBox2.SelectedItem != null)
            {
                DispositivoAgua sensorSeleccionado = (DispositivoAgua)comboBox2.SelectedItem;
                sensorSeleccionado.cantidad = (int)numericUpDown4.Value;
                // Verificar si el elemento ya está en comboBox2
                bool yaEnComboBox2 = false;
                foreach (DispositivoAgua item in comboBox5.Items)
                {
                    if (item.id == sensorSeleccionado.id)
                    {
                        yaEnComboBox2 = true;
                        break;
                    }
                }

                if (!yaEnComboBox2)
                {
                    // Agregar el elemento a comboBox2
                    comboBox5.Items.Add(sensorSeleccionado);

                }
                else
                {
                    MessageBox.Show("Este dispositivo de agua ya ha sido agregado a la lista.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un dispositivo agua.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Materiales materiales = new Materiales();
            materiales.precioConductoAgua = (int)numericUpDown9.Value;
            materiales.conductoAgua = textBox9.Text;
            materiales.cantEquipos = (int)numericUpDown7.Value;
            materiales.precioEquipo = (int)numericUpDown8.Value;
            materiales.comentario = textBox10.Text;
            materiales.idFormulario = formulario.idFormulario;
            gestorFormularios.InsertarNuevoMaterial(materiales);

            foreach (var item in comboBox4.Items)
            {
                Sensor sensor = item as Sensor;
                if (sensor != null)
                {
                    gestorFormularios.CargarFormularioxSensores(formulario, sensor);
                }
            }
            foreach (var item in comboBox5.Items)
            {
                DispositivoAgua agua = item as DispositivoAgua;
                if (agua != null)
                {
                    gestorFormularios.CargarFormularioxDositivosAgua(formulario, agua);
                }
            }
            MessageBox.Show("Materiales cargados correctamente");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DispositivoAgua agua = new DispositivoAgua();
            agua.cantidad = (int)numericUpDown1.Value;
            agua.precio = (int)numericUpDown6.Value;
            agua.descripcion = textBox8.Text;
            agua.nombre = textBox7.Text;
            agua.id = gestorFormularios.CargarDispositivoAgua(agua);
            comboBox5.Items.Add(agua);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sensor sensor = new Sensor();
            sensor.cantidad = (int)numericUpDown3.Value;
            sensor.precio = (int)numericUpDown5.Value;
            sensor.descipcion = textBox6.Text;
            sensor.nombre = textBox4.Text;
            sensor.id = gestorFormularios.CargarSensor(sensor);
            comboBox4.Items.Add(sensor);
        }
    }
}
