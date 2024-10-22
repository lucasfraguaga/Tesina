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
    public partial class VendedorFecha : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Pedido form1;
        Formulario formulario;
        public VendedorFecha(Pedido form1, Formulario form)
        {
            InitializeComponent();
            this.form1 = form1;
            formulario = form;
        }

        private void VendedorFecha_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            List<Formulario> formularios = new List<Formulario>();
            formularios.Add(formulario);
            dataGridView1.DataSource = formularios;
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            FechaInstalacion fecha = new FechaInstalacion();
            fecha = gestorFormularios.ObtenerFechasInstalacionPorFormulario(formulario);
            if(fecha != null)
            {
                label9.Text = fecha.fecha.ToString();
                label10.Text = fecha.estado;
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = false;
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

            label6.Tag = "Descripcion";
            label7.Tag = "Fecha";
            label8.Tag = "Fecha cargada";
            button1.Tag = "Cargar Fecha";
            button2.Tag = "Cambiar fecha";
            button3.Tag = "Cambiar estado a aceptado";


            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button7);

            objetosConTag.Add(label6);
            objetosConTag.Add(label7);
            objetosConTag.Add(label8);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
            objetosConTag.Add(button3);

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
            FechaInstalacion fecha = new FechaInstalacion();
            fecha.descripcion = textBox1.Text;
            fecha.fecha = monthCalendar1.SelectionRange.Start;
            fecha.estado = "propuesto";
            gestorFormularios.CargarFechaInstalacionVendedor(formulario,fecha);
            MessageBox.Show("Fecha cargada correctamente en estado propuesto");
            label9.Text = fecha.fecha.ToString();
            label10.Text = fecha.estado;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FechaInstalacion fecha = new FechaInstalacion();
            fecha.descripcion = textBox1.Text;
            fecha.fecha = monthCalendar1.SelectionRange.Start;
            fecha.estado = "propuesto";
            gestorFormularios.RecargarFechaInstalacion(formulario, fecha);
            MessageBox.Show("Fecha recargada correctamente en estado propuesto");
            label9.Text = fecha.fecha.ToString();
            label10.Text = fecha.estado;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gestorFormularios.ActualizarEstadoFecha(formulario, "aceptado");
            label10.Text = "aceptado";
            MessageBox.Show("Estado cambiado a aceptado correctamente");
        }
    }
}
