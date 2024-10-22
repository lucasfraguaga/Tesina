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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class frmPatentesFamilias : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLpermisos repo;
        Familia seleccion;
        Admin admin;
        public frmPatentesFamilias(Admin admin)
        {
            InitializeComponent();
            repo = new BLLpermisos();
            this.admin = admin;
            //cargo los permisos "Atómicos"
            this.cboPermisos.DataSource = repo.GetAllPermission();
        }
        private void LlenarPatentesFamilias()
        {
            llenarComboLenguaje();
            this.cboPatentes.DataSource = repo.GetAllPatentes();
            this.cboFamilias.DataSource = repo.GetAllFamilias();
        }

        private void frmPatentesFamilias_Load(object sender, EventArgs e)
        {
            llenarComboLenguaje();
            LlenarPatentesFamilias();
        }

        List<object> objetosConTag = new List<object>();
        private void tag()
        {
            label1.Tag = "Permiso";
            label2.Tag = "Todas las patentes";           
            label3.Tag = "Nombre";
            label4.Tag = "Todas las familias";
            label5.Tag = "Nombre";
            label6.Tag = "Lenguaje";
            button1.Tag = "Guardar";
            button2.Tag = "Volver";
            cmdAgregarPatente.Tag = "Agregar >>";
            btnGuardarPatente.Tag = "Guardar";
            cmdSeleccionar.Tag = "Configurar";
            cmdAgregarFamilia.Tag = "Agregar >>";
            cmdGuardarFamilia.Tag = "Guardar familia";
            groupBox1.Tag = "Nueva";
            groupBox2.Tag = "Familia";
            groupBox3.Tag = "Nueva";
            groupBox4.Tag = "Configurar Familia";
            grpPatentes.Tag = "Patentes";
            

            objetosConTag.Add(label1);
            objetosConTag.Add(label2);
            objetosConTag.Add(label3);
            objetosConTag.Add(label4);
            objetosConTag.Add(label5);
            objetosConTag.Add(label6);
            objetosConTag.Add(button1);
            objetosConTag.Add(button2);
            objetosConTag.Add(cmdAgregarPatente);
            objetosConTag.Add(btnGuardarPatente);
            objetosConTag.Add(cmdSeleccionar);
            objetosConTag.Add(cmdAgregarFamilia);
            objetosConTag.Add(cmdGuardarFamilia);
            objetosConTag.Add(groupBox1);
            objetosConTag.Add(groupBox2);
            objetosConTag.Add(groupBox3);
            objetosConTag.Add(groupBox4);
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

        private void cmdAgregarPatente_Click(object sender, EventArgs e)
        {
            if (seleccion != null)
            {
                var patente = (Patente)cboPatentes.SelectedItem;
                if (patente != null)
                {
                    var esta = repo.Existe(seleccion, patente.Id);
                    if (esta)
                        MessageBox.Show("ya exsite la patente indicada");
                    else
                    {

                        {
                            seleccion.AgregarHijo(patente);
                            MostrarFamilia(false);
                        }
                    }
                }
            }
        }

        private void btnGuardarPatente_Click(object sender, EventArgs e)
        {
            Patente p = new Patente()
            {
                Nombre = this.txtNombrePatente.Text,
                Permiso = (TipoPermiso)this.cboPermisos.SelectedItem

            };

            repo.GuardarComponente(p, false);
            LlenarPatentesFamilias();

            MessageBox.Show("Patente guardada correctamente");
        }

        private void cmdSeleccionar_Click(object sender, EventArgs e)
        {
            var tmp = (Familia)this.cboFamilias.SelectedItem;
            seleccion = new Familia();
            seleccion.Id = tmp.Id;
            seleccion.Nombre = tmp.Nombre;

            MostrarFamilia(true);
        }

        private void cmdAgregarFamilia_Click(object sender, EventArgs e)
        {
            if (seleccion != null)
            {
              
                var familia = (Familia)cboFamilias.SelectedItem;
                if (familia != null)
                {
                    if (validarRecursividad(familia, seleccion))
                    {
                        var esta = repo.Existe(seleccion, familia.Id);
                        if (esta)
                            MessageBox.Show("ya exsite la familia indicada");
                        else
                        {
                            repo.FillFamilyComponents(familia);
                            seleccion.AgregarHijo(familia);
                            MostrarFamilia(false);
                        }
                    }
                    else
                    {
                        MessageBox.Show("problemas de recursividad");
                    }
                }
            }
        }
        private bool validarRecursividad(Familia familia, Familia padre)
        {
            repo.FillFamilyComponents(familia);
            return auxValidarRecursividad(familia, padre);
        }

        private bool auxValidarRecursividad(Familia familia, Familia padre)
        {
            foreach (var item in familia.Hijos)
            {
                if (item.Id == padre.Id)
                {
                    return false;
                }
                if (item is Familia childFamilia)
                {
                    if (!auxValidarRecursividad(childFamilia, padre))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Familia p = new Familia()
            {
                Nombre = this.txtNombreFamilia.Text

            };




            repo.GuardarComponente(p, true);
            LlenarPatentesFamilias();
            MessageBox.Show("Familia guardada correctamente");
        }

        private void cmdGuardarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                repo.GuardarFamilia(seleccion);
                MessageBox.Show("Familia guardada correctamente");
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar la familia");
            }
        }

        void MostrarFamilia(bool init)
        {
            if (seleccion == null) return;


            IList<Componente> flia = null;
            if (init)
            {
                //traigo los hijos de la base
                flia = repo.GetAll("=" + seleccion.Id);


                foreach (var i in flia)
                    seleccion.AgregarHijo(i);
            }
            else
            {
                flia = seleccion.Hijos;
            }

            this.treeConfigurarFamilia.Nodes.Clear();

            TreeNode root = new TreeNode(seleccion.Nombre);
            root.Tag = seleccion;
            this.treeConfigurarFamilia.Nodes.Add(root);

            foreach (var item in flia)
            {
                MostrarEnTreeView(root, item);
            }

            treeConfigurarFamilia.ExpandAll();
        }


        void MostrarEnTreeView(TreeNode tn, Componente c)
        {
            TreeNode n = new TreeNode(c.Nombre);
            tn.Tag = c;
            tn.Nodes.Add(n);
            if (c.Hijos != null)
                foreach (var item in c.Hijos)
                {
                    MostrarEnTreeView(n, item);
                }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin.llenarComboLenguaje();
            admin.Show();
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
    }
}
