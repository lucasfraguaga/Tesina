using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLUsuarios
    {
        UsuariosRepository _usuarios;
        public BLLUsuarios()
        {
            _usuarios = new UsuariosRepository();
        }

        public List<Usuario> GetAll()
        {
            return _usuarios.GetAll();
        }

        public void GuardarPermisos(Usuario u)
        {
            _usuarios.GuardarPermisos(u);
        }
    }
}
