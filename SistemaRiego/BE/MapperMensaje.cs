using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class MapperMensaje
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
		private int IdComponente;

		public int idComponente
		{
			get { return IdComponente; }
			set { IdComponente = value; }
		}
		private string Tipo;

		public string tipo
		{
			get { return Tipo; }
			set { Tipo = value; }
		}
		private string Mensaje;

		public string mensaje
		{
			get { return Mensaje; }
			set { Mensaje = value; }
		}

		private DateTime Fecha;

		public DateTime fecha
		{
			get { return Fecha; }
			set { Fecha = value; }
		}

	}
}
