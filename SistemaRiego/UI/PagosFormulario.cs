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
    public partial class PagosFormulario : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        ManejadorPresupuesto presupuesto = new ManejadorPresupuesto();
        private Pedido form1;
        Formulario formulario;
        public PagosFormulario(Pedido form1, Formulario form)
        {
            InitializeComponent();
            this.form1 = form1;
            formulario = form;
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

            label6.Tag = "Monto a pagar";
            label8.Tag = "Monto de la seña";
            button1.Tag = "Cargar pago de seña";
            button2.Tag = "Cargar pago total";
            button3.Tag = "Calcular presupuesto";



            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button7);

            objetosConTag.Add(label6);
            objetosConTag.Add(label8);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
            objetosConTag.Add(button3);
        }

        private void PagosFormulario_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            List<Formulario> formularios = new List<Formulario>();
            formularios.Add(formulario);
            dataGridView1.DataSource = formularios;
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            form1.llenarComboLenguaje();
            form1.actualizarFormularios();
            form1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label7.Text = (presupuesto.CalcularPresupuesto(gestorFormularios.obtenerMaterialesSegunFormulario(formulario),gestorFormularios.obtenerSensoresSegunFormulario(formulario),gestorFormularios.ObtenerTodosLosDispositivosAguaSegunFormulario(formulario))).ToString();
            label9.Text = (presupuesto.CalcularSeña(gestorFormularios.obtenerMaterialesSegunFormulario(formulario), gestorFormularios.obtenerSensoresSegunFormulario(formulario), gestorFormularios.ObtenerTodosLosDispositivosAguaSegunFormulario(formulario))).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gestorFormularios.CargarPago(formulario,"señado");
            formulario.estadoPago = "señado";
            List<Formulario> formularios = new List<Formulario>();
            formularios.Add(formulario);
            dataGridView1.DataSource = formularios;
            MessageBox.Show("se cargo la seña");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gestorFormularios.CargarPago(formulario, "pago total");
            formulario.estadoPago = "pago total";
            List<Formulario> formularios = new List<Formulario>();
            formularios.Add(formulario);
            dataGridView1.DataSource = formularios;
            MessageBox.Show("se cargo el pago total");
        }
    }
}
