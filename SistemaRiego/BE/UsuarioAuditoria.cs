using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioAuditoria
    {
		private int IdAuditoria;

		public int idAuditoria
		{
			get { return IdAuditoria; }
			set { IdAuditoria = value; }
		}

		private int IdUsuario;

		public int idUsuario
		{
			get { return IdUsuario; }
			set { IdUsuario = value; }
		}
		private string Nombre;

		public string nombre
		{
			get { return Nombre; }
			set { Nombre = value; }
		}
		private string Contrasena;

		public string contrasena
		{
			get { return Contrasena; }
			set { Contrasena = value; }
		}
		private string TipoOperacion;

		public string tipoOperacion
		{
			get { return TipoOperacion; }
			set { TipoOperacion = value; }
		}

		private DateTime FechaCambio;

		public DateTime fechaCambio
		{
			get { return FechaCambio; }
			set { FechaCambio = value; }
		}


	}
}
