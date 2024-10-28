using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class MapperTarea
    {
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private int IdUsuario;

		public int idUsuario
		{
			get { return IdUsuario; }
			set { IdUsuario = value; }
		}
		private int IdAdministrador;

		public int idAdministrador
		{
			get { return IdAdministrador; }
			set { IdAdministrador = value; }
		}
		private string Tema;

		public string tema
		{
			get { return Tema; }
			set { Tema = value; }
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
