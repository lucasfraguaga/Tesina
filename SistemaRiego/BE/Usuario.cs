using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        public Usuario()
        {
            _permisos = new List<Componente>();
        }

        List<Componente> _permisos;
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<Componente> Permisos
        {
            get
            {
                return _permisos;
            }
        }

		private string contrasena;

		public string Contrasena
		{
			get { return contrasena; }
			set { contrasena = value; }
		}
        public override string ToString()
        {
            return Nombre;
        }


    }
}
