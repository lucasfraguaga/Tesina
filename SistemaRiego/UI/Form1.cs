using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Seguridad;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace UI
{
    public partial class Form1 : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        ManejadorEncriptado manejadorEncriptado = new ManejadorEncriptado();
        BLLManejadorDeDigitoVerificador digitoVerificador = new BLLManejadorDeDigitoVerificador();
        public Form1()
        {
            InitializeComponent(); 
            llenarComboLenguajeEspañol();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usuario usu = conexion.ValidarUsuario(textBox1.Text,textBox2.Text);
            if (usu is null)
            {
                //MessageBox.Show("usuario no encontrado");
            }
            else
            {
                BLLSesionManager.login(usu);
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso de usuario");
                switch (usu.Permisos[0].Id)
                    {
                        case 1:
                            Admin form2 = new Admin(this);
                            form2.Show();
                            this.Hide();
                            break;
                        case 3:
                            Venta form3 = new Venta(this);
                            form3.Show();
                            this.Hide();
                            break;
                        case 10:
                            Venta form4 = new Venta(this);
                            form4.Show();
                            this.Hide();
                            break;
                        case 12:
                            Venta form5 = new Venta(this);
                            form5.Show();
                            this.Hide();
                            break;
                        case 5:
                            Analisis_de_equipo form6 = new Analisis_de_equipo(this);
                            form6.Show();
                            this.Hide();
                            break;
                        case 6:
                            Analista_de_campo form7 = new Analista_de_campo(this);
                            form7.Show();
                            this.Hide();
                            break;
                        case 4:
                            Instalador form8 = new Instalador(this);
                            form8.Show();
                            this.Hide();
                            break;
                        default:
                            MessageBox.Show("Roll no valido");
                            break;
                    }
               
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { textBox2.PasswordChar = '\0'; }
            else {  textBox2.PasswordChar = '*'; }
            
        }

        List<object> objetosConTag = new List<object>();
        private void Form1_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            if(digitoVerificador.comprobarTablas())
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button2.Visible = true;
            }
        }
        private void tag()
        {
            label1.Tag = "Usuario";
            label2.Tag = "Contraseña";
            checkBox1.Tag = "MostrarContraseña";
            button1.Tag = "Conectar";
            label3.Tag = "Lenguaje";

            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(checkBox1);
            objetosConTag.Add(button1);      
            objetosConTag.Add(label3);
        }


        public void llenarComboLenguajeEspañol()
        {
            List<LenguajeMenu> list = new List<LenguajeMenu>();
            list = conexion.GetLenguages();
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
        public void llenarComboLenguaje()
        {
            List<LenguajeMenu> list = new List<LenguajeMenu>();
            list = conexion.GetLenguages();

            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "LanguageName";
            comboBox1.ValueMember = "LanguageId";
            comboBox1.SelectedItem = list.FirstOrDefault(l => l.languageId == ObserverLenguaje.GetLenguaje.idioma.languageId);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Id = 0;
            conexion.insertarBitacora(usuario, "Se forzo solucion de digito verificador vertical");
            button2.Enabled = false;
            button2.Visible = false;
            button1.Enabled = true;
        }

    }

}
