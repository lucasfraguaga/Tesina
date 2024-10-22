using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class FechaInstalacion
    {
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private int Id_Formulario;

		public int id_Formulario
		{
			get { return Id_Formulario; }
			set { Id_Formulario = value; }
		}

		private DateTime Fecha;

		public DateTime fecha
		{
			get { return Fecha; }
			set { Fecha = value; }
		}
		private string Descripcion;

		public string descripcion
		{
			get { return Descripcion; }
			set { Descripcion = value; }
		}

		private string Estado;

		public string estado
		{
			get { return Estado; }
			set { Estado = value; }
		}

	}
}
