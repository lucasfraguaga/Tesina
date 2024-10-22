using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cultivo
    {
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private string Descripcion;

		public string descripcion
		{
			get { return Descripcion; }
			set { Descripcion = value; }
		}

		private string Tipo;

		public string tipo
		{
			get { return Tipo; }
			set { Tipo = value; }
		}

		private int Cantidad;

		public int cantidad
		{
			get { return Cantidad; }
			set { Cantidad = value; }
		}

        public override string ToString()
        {
            return $"{id} ({Tipo})"; // Muestra la descripción y el tipo
        }

    }
}
