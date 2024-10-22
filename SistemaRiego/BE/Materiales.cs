using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Materiales
    {
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private string Comentario;

		public string comentario
		{
			get { return Comentario; }
			set { Comentario = value; }
		}
		private int CantEquipos;

		public int cantEquipos
		{
			get { return CantEquipos; }
			set { CantEquipos = value; }
		}
		private string ConductoAgua;

		public string conductoAgua
		{
			get { return ConductoAgua; }
			set { ConductoAgua = value; }
		}
		private float PrecioConductoAgua;

		public float precioConductoAgua
		{
			get { return PrecioConductoAgua; }
			set { PrecioConductoAgua = value; }
		}
		private float PrecioEquipo;

		public float precioEquipo
		{
			get { return PrecioEquipo; }
			set { PrecioEquipo = value; }
		}
		private int IdFormulario;

		public int idFormulario
		{
			get { return IdFormulario; }
			set { IdFormulario = value; }
		}



	}
}
