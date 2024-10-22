using BE;
using BLL;
using Seguridad;
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

namespace UI
{
    public partial class CrearIdioma : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        Admin admin;
        public CrearIdioma(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Vuelta a pantalla admin");
            admin.llenarComboLenguaje();
            admin.Show();
            this.Close();
        }

        private void CrearIdioma_Load(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso a la pantalla crearIdioma");
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView1.DataSource = gestorIdiomas.GetContent();
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
            label6.Tag = "Codigo de lenguaje";
            label7.Tag = "Nombre de lenguaje";
            button2.Tag = "Crear idioma";



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
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Uno o ambos campos no tienen texto.");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "No se pudo crear un idioma ya que uno o ambos campos no tienen texto");
            }
            else
            {
                var items = new List<ItemIdiomaNuevoDisplay>();
                bool aux = true;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {


                    if (!string.IsNullOrEmpty(row.Cells["traduccion"].Value.ToString()))
                    {
                        // La celda tiene contenido               
                    }
                    else
                    {
                        // La celda está vacía o contiene solo espacios en blanco
                        aux = false;
                    }


                    if (!row.IsNewRow) // Ignorar la fila nueva
                    {
                        var item = new ItemIdiomaNuevoDisplay
                        {
                            id = Convert.ToInt32(row.Cells["Id"].Value),
                            traduccion = row.Cells["traduccion"].Value.ToString(),
                        };
                        items.Add(item);
                    }
                }

                if (aux)
                {
                    gestorIdiomas.CargarLenguaje(textBox1.Text, textBox2.Text);
                    gestorIdiomas.InsertVersionContentFromDotNet(items);
                    llenarComboLenguaje();
                    conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Idioma creado correctamente");
                    MessageBox.Show("Traduccion creada correctamente");
                }
                else
                {
                    conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "No se pudo crear idioma, hay traducciones sin llenar");
                    MessageBox.Show("hay traducciones sin llenar");
                }

                
                
            }
        }
    }
}
