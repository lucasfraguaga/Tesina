using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguridad;


namespace UI
{
    public partial class crearUsuario : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        ManejadorEncriptado encriptado = new ManejadorEncriptado();
        Admin admin;
        public crearUsuario(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void crearUsuario_Load(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso a la pantalla crearUsuario");
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Vuelta a la pantalla admin");
            admin.llenarComboLenguaje();
            admin.Show();
            this.Close();
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
            label6.Tag = "Ingrese el usuario";
            label7.Tag = "Ingrese la contraseña";
            button2.Tag = "Registrar usuario";


            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button1);
            objetosConTag.Add(label6);
            objetosConTag.Add(label7);
            objetosConTag.Add(button2);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text)))
            {
                conexion.guardarUsuario(textBox1.Text, encriptado.ObtenerHash(textBox2.Text));
                MessageBox.Show("Usuario registrado exitosamente");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Usuario registrado");
            }
            else
            {
                MessageBox.Show("Algun texbox esta vacio");
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
