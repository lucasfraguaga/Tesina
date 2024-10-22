using BE;
using BLL;
using System;
using System.Collections;
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
    public partial class Venta : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        private Form1 form1;
        public Venta(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void Venta_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();          
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;    
            crearPedidoPremiumToolStripMenuItem.Enabled = false;
            roles(BLLSesionManager.GetInstance.Usuario.Permisos);
        }

        private void roles(IList<BE.Componente> lista)
        {
            foreach (var item in lista)
            {
                if (item.Id == 11)
                {
                    crearPedidoPremiumToolStripMenuItem.Enabled = true;
                }
                if (item is BE.Familia permisos)
                {
                    roles(permisos.Hijos);
                }
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
            button1.Tag = "Desconectarse";
            label5.Tag = "Lenguaje";
            crearPedidoPremiumToolStripMenuItem.Tag = "Crear pedido premium";
            toolStripMenuItem1.Tag = "Pedidos";


            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button1);
            objetosConTag.Add(crearPedidoPremiumToolStripMenuItem);
            objetosConTag.Add(toolStripMenuItem1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BLL.BLLSesionManager.logaut();
            //MessageBox.Show("Sesion desconectada correctamente");
            form1.llenarComboLenguaje();
            form1.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pedido form3 = new Pedido(this);
            form3.Show();
            this.Hide();
        }
    }
}
