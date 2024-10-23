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

namespace UI
{
    public partial class PedidosPendientes : Form
    {
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private Analisis_de_equipo form1;
        public PedidosPendientes(Analisis_de_equipo form1)
        {
            InitializeComponent();
            this.form1 = form1;
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void PedidosPendientes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = conexion.ObtenerTodosLosPedidosCompra();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.Show();
            this.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];

                var material = (MapperCarritoCompra)selectedRow.DataBoundItem;

                dataGridView2.DataSource = conexion.ObtenerProductosPorIdPedido(material.idPedido);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                var selectedRow = dataGridView1.SelectedRows[0];

                var material1 = (MapperCarritoCompra)selectedRow.DataBoundItem;
                if (material1.estado != "finalizado")
                {
                    var listaCompleta = (List<MapperCarritoCompra>)dataGridView2.DataSource;
                    int idPedido = 0;
                    foreach (var material in listaCompleta)
                    {
                        if (material.estado == "Sensor")
                        {
                            conexion.AjustarStockSensor(material.idProducto, material.cantidad, 1);
                        }
                        if (material.estado == "Equipo")
                        {
                            conexion.AjustarStockEquipo(material.idProducto, material.cantidad, 1);
                        }
                        if (material.estado == "DispositivoAgua")
                        {
                            conexion.AjustarStockDispositivoAgua(material.idProducto, material.cantidad, 1);
                        }
                        idPedido = material.idPedido;
                    }
                    conexion.CambiarEstadoPedido(idPedido, "finalizado");
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = conexion.ObtenerTodosLosPedidosCompra();
                    dataGridView2.DataSource = null;
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        dataGridView2.DataSource = conexion.ObtenerProductosPorIdPedido(material1.idPedido);
                    }
                    MessageBox.Show("Pedido cargado correctamente");
                }
                else
                {
                    MessageBox.Show("Este pedido ya tiene el estado finalizado");
                }
            }
            else
            {
                MessageBox.Show("No hay datos en el DataGridView.");
            }
        }
    }
}
