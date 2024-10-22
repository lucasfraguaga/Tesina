using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ObserverLenguaje
    {
        private LenguajeMenu Idioma;

        public LenguajeMenu idioma
        {
            get { return Idioma; }
            set { Idioma = value; }
        }

        private static ObserverLenguaje lenguaje;

        // Constructor estático para inicializar lenguaje
        static ObserverLenguaje()
        {
            lenguaje = new ObserverLenguaje();
        }

        // Propiedad estática para obtener la instancia de lenguaje
        public static ObserverLenguaje GetLenguaje
        {
            get { return lenguaje; }
        }
    }
}
