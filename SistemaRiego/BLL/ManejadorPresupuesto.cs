using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ManejadorPresupuesto
    {
        public float CalcularPresupuesto(Materiales materiales, List<Sensor> sensores, List<DispositivoAgua> aguas)
        {
            //<>
            float porcentaje = 0;
            if (materiales.cantEquipos > 1)
            {
                if(materiales.cantEquipos > 3)
                {
                    porcentaje = 0.70f;
                }
                else
                {
                    porcentaje = 0.50f;
                }
            }
            else
            {
                porcentaje = 0.30f;
            }
            float total = (materiales.precioConductoAgua) + (materiales.cantEquipos * materiales.precioEquipo);
            foreach (var item in sensores)
            {
                total += total + item.precio;
            }
            foreach (var item in aguas)
            {
                total += total + item.precio;
            }
            total += total * porcentaje;
            return total;
        }

        public float CalcularSeña(Materiales materiales, List<Sensor> sensores, List<DispositivoAgua> aguas)
        {
            float total = CalcularPresupuesto(materiales, sensores,aguas);
            total = total * 0.30f;
            return total;
        }
    }
}
