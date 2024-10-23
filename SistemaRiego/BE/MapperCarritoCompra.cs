using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class MapperCarritoCompra
    {
		private int IdPedido;

		public int idPedido
		{
			get { return IdPedido; }
			set { IdPedido = value; }
		}
		private int IdProducto;

		public int idProducto
		{
			get { return IdProducto; }
			set { IdProducto = value; }
		}
		private int Cantidad;

		public int cantidad
		{
			get { return Cantidad; }
			set { Cantidad = value; }
		}
		private float Precio;

		public float precio
		{
			get { return Precio; }
			set { Precio = value; }
		}
		private string Estado;

		public string estado
		{
			get { return Estado; }
			set { Estado = value; }
		}
	}
}
