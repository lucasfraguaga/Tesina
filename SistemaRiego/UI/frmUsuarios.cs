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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class frmUsuarios : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLUsuarios repo;
        BLLpermisos permisosRepo;
        Usuario seleccion;
        Usuario tmp;
        Admin admin;
        public frmUsuarios(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
            repo = new BLLUsuarios();
            permisosRepo = new BLLpermisos();
            this.cboUsuarios.DataSource = repo.GetAll();
            this.cboFamilias.DataSource = permisosRepo.GetAllFamilias();
            this.cboPatentes.DataSource = permisosRepo.GetAllPatentes();
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
        private void cmdGuardarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                repo.GuardarPermisos(tmp);
                MessageBox.Show("Usuario guardado correctamente");
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar el usuario");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var flia = (Familia)cboFamilias.SelectedItem;
                if (flia != null)
                {
                    var esta = false;
                    //verifico que ya no tenga el permiso. TODO: Esto debe ser parte de otra capa.
                    foreach (var item in tmp.Permisos)
                    {
                        if (permisosRepo.Existe(item, flia.Id))
                        {
                            esta = true;
                        }
                    }

                    if (esta)
                        MessageBox.Show("El usuario ya tiene la familia indicada");
                    else
                    {
                        {
                            permisosRepo.FillFamilyComponents(flia);

                            tmp.Permisos.Add(flia);
                            MostrarPermisos(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un usuario");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var patente = (Patente)cboPatentes.SelectedItem;
                if (patente != null)
                {
                    var esta = false;

                    foreach (var item in tmp.Permisos)
                    {
                        if (permisosRepo.Existe(item, patente.Id))
                        {
                            esta = true;
                            break;
                        }
                    }
                    if (esta)
                        MessageBox.Show("El usuario ya tiene la patente indicada");
                    else
                    {
                        {
                            tmp.Permisos.Add(patente);
                            MostrarPermisos(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un usuario");
        }

        private void cmdConfigurar_Click(object sender, EventArgs e)
        {
            seleccion = (Usuario)this.cboUsuarios.SelectedItem;

            //hago una copia del objeto para no modificr el que esta en el combo.
            tmp = new Usuario();
            tmp.Id = seleccion.Id;
            tmp.Nombre = seleccion.Nombre;
            permisosRepo.FillUserComponents(tmp);

            MostrarPermisos(tmp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin.llenarComboLenguaje();
            admin.Show();
            this.Close();
        }
        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Agregar patentes";
            label2.Tag = "Todos los usuarios";
            label3.Tag = "Agregar Familias";         
            label5.Tag = "Lenguaje";
            button1.Tag = "Agregar >>";
            button2.Tag = "Agregar >>";
            button3.Tag = "Volver";
            cmdGuardarFamilia.Tag = "Guardar cambios";
            cmdConfigurar.Tag = "Configurar";
            grpPatentes.Tag = "Usuarios";



            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label3);
            objetosConTag.Add(label5);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
            objetosConTag.Add(button3);
            objetosConTag.Add(cmdGuardarFamilia);
            objetosConTag.Add(cmdConfigurar);
            objetosConTag.Add(grpPatentes);
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
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
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
