using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PermisosDisplay
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }     
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
    }
}
