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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class ModificarUsuario : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        ManejadorEncriptado encriptado = new ManejadorEncriptado();
        Admin admin;
        public ModificarUsuario(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void ModificarUsuario_Load(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso a la pantalla modificarUsuario");
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView1.DataSource = conexion.ObtenerUsuarios();
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
            button1.Tag = "Volver";
            label5.Tag = "Lenguaje";
            label7.Tag = "Ingrese la contraseña";
            button2.Tag = "Recetear contraseña";
            label6.Tag = "Ingrese el usuario";
            button3.Tag = "Cambiar usuario";


            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button1);
            objetosConTag.Add(label7);
            objetosConTag.Add(button2);
            objetosConTag.Add(label6);
            objetosConTag.Add(button3);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Vuelta a la pantalla admin");
            admin.llenarComboLenguaje();
            admin.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !(string.IsNullOrWhiteSpace(textBox2.Text)))
            {
                // Obtén la primera fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Obtén el valor de la celda en la columna 3
                var selectedCellValue = selectedRow.Cells[0].Value;

                // Muestra o maneja el valor de la celda
                conexion.cambiarContraseña(int.Parse(selectedCellValue.ToString()), encriptado.ObtenerHash(textBox2.Text));
                dataGridView1.DataSource = conexion.ObtenerUsuarios();
                MessageBox.Show("Contraseña cambiada exitosamente");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Contraseña de usuario cambiada");
            }
            else
            {
                MessageBox.Show("la contraseña esta vacia");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !(string.IsNullOrWhiteSpace(textBox1.Text)))
            {
                // Obtén la primera fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Obtén el valor de la celda en la columna 3
                var selectedCellValue = selectedRow.Cells[0].Value;

                // Muestra o maneja el valor de la celda
                conexion.cambiarUsuario(int.Parse(selectedCellValue.ToString()), textBox1.Text);
                dataGridView1.DataSource = conexion.ObtenerUsuarios();
                MessageBox.Show("El usuario se cambio exitosamente");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Usuario cambiado");
            }
            else
            {
                MessageBox.Show("el usuario esta vacio");
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
    }
}
