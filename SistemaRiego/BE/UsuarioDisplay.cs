using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioDisplay
    {
        private int Id;

        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private string usuario;

        public string Usu
        {
            get { return usuario; }
            set { usuario = value; }
        }
        private int roll;

        public int Roll
        {
            get { return roll; }
            set { roll = value; }
        }
    }
}
