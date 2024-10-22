using BE;
using BLL;
using Seguridad;
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
    public partial class CreacionUsuarioCliente : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Admin form1;
        ManejadorEncriptado encriptado = new ManejadorEncriptado();
        PedidoCreacion pedidoSeleccionado = new PedidoCreacion();
        Formulario formularioSeleccionado = new Formulario();
        public CreacionUsuarioCliente(Admin form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void CreacionUsuarioCliente_Load(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso a la pantalla CreacionUsuarioCliente");
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView2.DataSource = gestorFormularios.ObtenerTodosLosPediosCreacion();
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

            label6.Tag = "Todos los pedidos";
            label9.Tag = "Formulario seleccionado";
            label7.Tag = "Ingrese el usuario";
            label8.Tag = "Ingrese la contraseña";
            button2.Tag = "Registrar usuario";
            button1.Tag  = "Seleccionar pedido";



            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button7);

            objetosConTag.Add(label6);
            objetosConTag.Add(label9);
            objetosConTag.Add(label7);
            objetosConTag.Add(label8);
            objetosConTag.Add(button2);
            objetosConTag.Add(button1);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Vuelta a la pantalla admin");
            form1.llenarComboLenguaje();
            form1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text)))
            {
                int idUsuario = gestorFormularios.guardarUsuarioConVuelta(textBox1.Text, encriptado.ObtenerHash(textBox2.Text));
                gestorFormularios.ActualizarContraseñaYUsuarioPedido(pedidoSeleccionado,textBox1.Text,textBox2.Text);
                gestorFormularios.ActualizarIdUsuarioCliente(formularioSeleccionado,idUsuario);
                MessageBox.Show("Usuario registrado exitosamente");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Usuario registrado");
                dataGridView2.DataSource = gestorFormularios.ObtenerTodosLosPediosCreacion();
            }
            else
            {
                MessageBox.Show("Algun texbox esta vacio");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Obtener la primera fila seleccionada
                var selectedRow = dataGridView2.SelectedRows[0];

                // Obtener el objeto 'Formulario' de la fila seleccionada
                PedidoCreacion pedido = (PedidoCreacion)selectedRow.DataBoundItem;
                Formulario formulario = gestorFormularios.ObtenerFormularioPorId(pedido.idFormulario);
                formularioSeleccionado = formulario;
                pedidoSeleccionado = pedido;

                List<Formulario> formularios = new List<Formulario>();
                formularios.Add(formulario);
                dataGridView1.DataSource = formularios;

            }
            else
            {
                MessageBox.Show("Por favor, seleccione un formulario de la lista.");
            }
        }
    }
}
