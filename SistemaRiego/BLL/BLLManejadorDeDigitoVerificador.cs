using Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using DAL;


namespace BLL
{
    public class BLLManejadorDeDigitoVerificador
    {
        BLLGestorConexiones conexion = new BLLGestorConexiones();
        public void comprobarRegistros()
        {
            List<BE.Bitacora> bitacora = new List<BE.Bitacora>();
            bitacora = conexion.listarBitacora();
            string auxBitacora = "La tabla Bitacora tiene registros corruptos y son: ";
            bool valBitacora = false;
            foreach (var item in bitacora)
            {
                if (item.estado == "Corrupto")
                {
                    valBitacora = true;
                    auxBitacora += item.id.ToString() + " ";
                }
            }
            string auxMensaje = "";
            if (valBitacora)
            {
                auxMensaje += auxBitacora;
            }
            if (valBitacora)
            {
                MessageBox.Show(auxMensaje);
                conexion.insertarBitacora((BLL.BLLSesionManager.GetInstance).Usuario, auxMensaje);
            }
            bitacora.Clear();
        }
        public bool comprobarTablas()
        {
            string resultado = conexion.VerificarIntegridadTabla();
            if(resultado == "La tabla ha sido alterada o se ha eliminado un registro.")
            {
                MessageBox.Show("La tabla bitacora tiene registros alterados o eliminados");
                return true;
            }
            return false;
        }
    }
}
