using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class DispositivoAgua
    {
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
		private string Nombre;

		public string nombre
		{
			get { return Nombre; }
			set { Nombre = value; }
		}

		private float Precio;

		public float precio
		{
			get { return Precio; }
			set { Precio = value; }
		}

		private int Stock;

		public int stock
		{
			get { return Stock; }
			set { Stock = value; }
		}

		public override string ToString()
        {
            return $"{id} ({nombre})"; // Muestra la descripción y el tipo
        }
    }
}
