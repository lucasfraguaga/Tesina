using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class MapperMaterialesRequeridos
    {
		private string Tipo;

		public string tipo
		{
			get { return Tipo; }
			set { Tipo = value; }
		}
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private int Cantidad;

		public int cantidad
		{
			get { return Cantidad; }
			set { Cantidad = value; }
		}
		private string Descripcion;

		public string descripcion
		{
			get { return Descripcion; }
			set { Descripcion = value; }
		}
		private int Stock;

		public int stock
		{
			get { return Stock; }
			set { Stock = value; }
		}
		private float Precio;

		public float precio
		{
			get { return Precio; }
			set { Precio = value; }
		}

	}
}
