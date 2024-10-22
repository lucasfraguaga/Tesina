using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PedidoCreacion
    {
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private int IdFormulario;

		public int idFormulario
		{
			get { return IdFormulario; }
			set { IdFormulario = value; }
		}
		private string Estado;

		public string estado
		{
			get { return Estado; }
			set { Estado = value; }
		}
		private string Usuario;

		public string usuario
		{
			get { return Usuario; }
			set { Usuario = value; }
		}
		private string Contraseña;

		public string contraseña
		{
			get { return Contraseña; }
			set { Contraseña = value; }
		}


	}
}
