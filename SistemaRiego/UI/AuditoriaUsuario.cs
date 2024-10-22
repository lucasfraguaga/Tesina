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
    public partial class AuditoriaUsuario : Form
    {
        BLLGetorIdiomas gestorIdiomas = new BLLGetorIdiomas();
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        ManejadorEncriptado encriptado = new ManejadorEncriptado();
        Admin admin;
        public AuditoriaUsuario(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void AuditoriaUsuario_Load(object sender, EventArgs e)
        {
            conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, "Ingreso a la pantalla AuditoriaUsuario");
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
            dataGridView1.DataSource = conexion.ObtenerUsuarios();          
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que la fila seleccionada sea válida.
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada.
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                // Convertir la fila seleccionada en un objeto UsuarioAuditoria.
                BE.UsuarioDisplay usuario = (BE.UsuarioDisplay)filaSeleccionada.DataBoundItem;

                dataGridView2.DataSource = conexion.ObtenerCambiosUsuarios(usuario.id);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = conexion.ObtenerUsuariosEliminados();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Verificar que haya una fila seleccionada.
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada (la primera si hay varias seleccionadas).
                DataGridViewRow filaSeleccionada = dataGridView2.SelectedRows[0];

                // Convertir la fila seleccionada en un objeto UsuarioDisplay.
                UsuarioAuditoria usuarioSeleccionado = (UsuarioAuditoria)filaSeleccionada.DataBoundItem;

                if(usuarioSeleccionado.tipoOperacion == "DELETE")
                {
                    MessageBox.Show("No se puede recuperar un usuario eliminado");
                }
                else
                {
                    Usuario usu = new Usuario();
                    usu.Id = usuarioSeleccionado.idUsuario;
                    usu.Nombre = usuarioSeleccionado.nombre;
                    usu.Contrasena = usuarioSeleccionado.contrasena;
                    conexion.cambiarUsuarioYContraseña(usu);
                    MessageBox.Show("usuario restaurado correctamente");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario.");
            }
        }
    }
}
