using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cliente
    {
		private string Nombre;

		public string nombre
		{
			get { return Nombre; }
			set { Nombre = value; }
		}

		private string Apellido;

		public string apellido
		{
			get { return Apellido; }
			set { Apellido = value; }
		}

		private int DNI;

		public int dni
		{
			get { return DNI; }
			set { DNI = value; }
		}
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private string Mail;

		public string mail
		{
			get { return Mail; }
			set { Mail = value; }
		}
		private int Telefono;

		public int telefono
		{
			get { return Telefono; }
			set { Telefono = value; }
		}



	}
}
