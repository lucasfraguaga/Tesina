using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;

namespace Seguridad
{
    public class ManejadorEncriptado
    {
        public string ObtenerHash(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena);
        }

        public bool ValidarContraseña(string contrasena, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(contrasena, hash);
        }
    }
}
