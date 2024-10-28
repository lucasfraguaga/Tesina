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


namespace UI
{
    public partial class Admin : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLManejadorDeDigitoVerificador digitoVerificador = new BLLManejadorDeDigitoVerificador();
        private Form1 form1;
        public Admin(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso a la pantalla admin");
            llenarComboLenguaje();
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            MostrarPermisos(BLLSesionManager.GetInstance.Usuario);
            digitoVerificador.comprobarRegistros();
        }
        void MostrarPermisos(Usuario u)
        {
            this.treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(u.Nombre);

            foreach (var item in u.Permisos)
            {
                LlenarTreeView(root, item);
            }

            this.treeView1.Nodes.Add(root);
            this.treeView1.ExpandAll();
        }
        void LlenarTreeView(TreeNode padre, Componente c)
        {
            TreeNode hijo = new TreeNode(c.Nombre);
            hijo.Tag = c;
            padre.Nodes.Add(hijo);

            foreach (var item in c.Hijos)
            {
                LlenarTreeView(hijo, item);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Desconexion de usuario");
            form1.llenarComboLenguaje();
            BLL.BLLSesionManager.logaut();          
            MessageBox.Show("Sesion desconectada correctamente");
            form1.Show();
            this.Close();
        }

        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Usuario";
            label2.Tag = "Roll";
            button1.Tag = "Desconectarse";
            label5.Tag = "Lenguaje";
            crearUsuarioToolStripMenuItem.Tag = "Crear usuario";
            modificarUsuarioToolStripMenuItem.Tag = "Modificar usuario";
            gestionesToolStripMenuItem.Tag = "Gestiones";
            crearIdiomaToolStripMenuItem.Tag = "Crear idioma";
            patenteYFamiliasToolStripMenuItem.Tag = "Patente y Familias";
            rolesUsuariosToolStripMenuItem.Tag = "Roles Usuarios";
            pedidoCreacionClienteToolStripMenuItem.Tag = "Pedido creacion cliente";



            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label5);
            objetosConTag.Add(button1);
            objetosConTag.Add(crearUsuarioToolStripMenuItem);
            objetosConTag.Add(modificarUsuarioToolStripMenuItem);
            objetosConTag.Add(gestionesToolStripMenuItem);
            objetosConTag.Add(crearIdiomaToolStripMenuItem);
            objetosConTag.Add(patenteYFamiliasToolStripMenuItem);
            objetosConTag.Add(rolesUsuariosToolStripMenuItem);
            objetosConTag.Add(pedidoCreacionClienteToolStripMenuItem);
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox)
            {
                ObserverLenguaje.GetLenguaje.idioma = (LenguajeMenu)comboBox1.SelectedItem;
                gestorIdiomas.cambiarIdioma(objetosConTag);
            }       
        }

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crearUsuario childForm = new crearUsuario(this);
            childForm.Show();
            this.Hide();
        }

        private void modificarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModificarUsuario childForm = new ModificarUsuario(this);
            childForm.Show();
            this.Hide();
        }

        private void patenteYFamiliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPatentesFamilias frm = new frmPatentesFamilias(this);
            frm.Show();
            this.Hide();
        }

        private void rolesUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuarios frm = new frmUsuarios(this);
            frm.Show();
            this.Hide();
        }

        private void crearIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearIdioma frm = new CrearIdioma(this);
            frm.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pedidoCreacionClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreacionUsuarioCliente frm = new CreacionUsuarioCliente(this);
            frm.Show();
            this.Hide();
        }

        private void hacerBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conexion.generarBackup())
            {
                MessageBox.Show("Backup exitoso");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "se genero un backup de la base de datos");
            }
            else
            {
                MessageBox.Show("Backup no exitoso");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "no se genero un backup de la base de datos");
            }
        }

        private void restaurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conexion.restaurarBase())
            {
                MessageBox.Show("Restore realizado correctamente");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "se restauro la base a la ultima version disponible");
            }
            else
            {
                MessageBox.Show("Error al realizar el Restore");
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "no se pudo restaurar la base");
            }
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitacora frm = new Bitacora(this);
            frm.Show();
            this.Hide();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuditoriaUsuario frm = new AuditoriaUsuario(this);
            frm.Show();
            this.Hide();
        }

        private void gestionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void anlisisContactoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalisisContacto frm = new AnalisisContacto(this);
            frm.Show();
            this.Hide();
        }
    }
}
