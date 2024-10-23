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
    public partial class PedidoCompra : Form
    {
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        BLLGestorFormulario gestorFormularios = new BLLGestorFormulario();
        private AnalisisStock form1;
        List<MapperMaterialesRequeridos> carritoCompra = new List<MapperMaterialesRequeridos>();
        public PedidoCompra(AnalisisStock form1)
        {
            InitializeComponent();
            this.form1 = form1;
            label3.Text = (BLL.BLLSesionManager.GetInstance).Usuario.Nombre;
        }

        private void PedidoCompra_Load(object sender, EventArgs e)
        {
            dataGridView4.DataSource = gestorFormularios.ObtenerMaterialesRequeridosConStock();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MapperMaterialesRequeridos material;
            if (dataGridView4.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView4.SelectedRows[0];

                material = (MapperMaterialesRequeridos)selectedRow.DataBoundItem;              
                MapperMaterialesRequeridos materialCarga = new MapperMaterialesRequeridos();
                materialCarga.id = material.id;
                materialCarga.cantidad = (int)numericUpDown1.Value;
                materialCarga.descripcion = material.descripcion;
                materialCarga.tipo = material.tipo;
                materialCarga.precio = material.precio;
                carritoCompra.Add(materialCarga);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = carritoCompra;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un material de la lista.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                MapperMaterialesRequeridos material = (MapperMaterialesRequeridos)selectedRow.DataBoundItem;
                carritoCompra.Remove(material);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = carritoCompra;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un material para eliminar.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float precioTotal = 0;
            foreach (var item in carritoCompra)
            {
                precioTotal += item.precio;
            }
            int idPedido = gestorFormularios.CrearPedidoCompra(precioTotal,"pedido");
            foreach (var item in carritoCompra)
            {
                if(item.tipo == "sensor")
                {
                    gestorFormularios.CrearPedidoSensor(idPedido,item.id,item.cantidad,item.precio);
                }
                if(item.tipo == "dispositivoAgua")
                {
                    gestorFormularios.CrearPedidoDispositivoAgua(idPedido, item.id, item.cantidad, item.precio);
                }
                if(item.tipo == "equipo")
                {
                    gestorFormularios.CrearPedidoEquipo(idPedido, item.id, item.cantidad, item.precio);
                }
            }
            MessageBox.Show("Pedido creado correctamente");
            carritoCompra = new List<MapperMaterialesRequeridos>();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = carritoCompra;
        }
    }
}
