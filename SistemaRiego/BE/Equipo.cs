using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Equipo
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
		private int Stock;

		public int stock
		{
			get { return Stock; }
			set { Stock = value; }
		}


	}
}
