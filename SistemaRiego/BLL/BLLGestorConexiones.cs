using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLGestorConexiones
    {
        Conexion conexion = new Conexion();

        public void guardarUsuario(string usuario, string contrasena)
        {
            conexion.guardarUsuario(usuario, contrasena);
        }

        public Usuario ValidarUsuario(string nombre, string contrasena)
        {
            return conexion.validadUsuario(nombre, contrasena);
        }
        public List<ItemIdioma> TraerLenguaje(int id)
        {
            return conexion.TraerLenguaje(id);
        }

        public List<UsuarioDisplay> ObtenerUsuarios()
        {
            return conexion.ObtenerUsuarios();
        }
        public void cambiarContraseña(int id, string con)
        {
            conexion.cambiarContraseña(id,con);
        }
        public void cambiarUsuario(int id, string con)
        {
            conexion.cambiarUsuario(id, con);
        }
        public List<LenguajeMenu> GetLenguages()
        {
            return conexion.GetLenguages();
        }
        public bool generarBackup()
        {
            return conexion.generarBackup();
        }
        public bool restaurarBase()
        {
            return conexion.restaurarBase();
        }
        public void insertarBitacora(Usuario usu, string mensaje)
        {
            conexion.InsertarBitacora(usu, mensaje);
        }
        public List<Bitacora> listarBitacora()
        {
            return conexion.listarBitacora();
        }
        public string VerificarIntegridadTabla()
        {
            return conexion.VerificarIntegridadTabla();
        }
        public List<UsuarioAuditoria> ObtenerCambiosUsuarios(int id)
        {
            return conexion.ObtenerCambiosUsuarios(id);
        }
        public List<UsuarioAuditoria> ObtenerUsuariosEliminados()
        {
            return conexion.ObtenerUsuariosEliminados();
        }
        public void cambiarUsuarioYContraseña(Usuario usu)
        {
            conexion.cambiarUsuarioYContraseña(usu);
        }
        public List<MapperCarritoCompra> ObtenerTodosLosPedidosCompra()
        {
            return conexion.ObtenerTodosLosPedidosCompra();
        }
        public List<MapperCarritoCompra> ObtenerProductosPorIdPedido(int id)
        {
            return conexion.ObtenerProductosPorIdPedido(id);
        }
        public void AjustarStockSensor(int id, int cantidad, int operacion)
        {
            conexion.AjustarStockSensor(id,cantidad,operacion);
        }
        public void AjustarStockDispositivoAgua(int id, int cantidad, int operacion)
        {
            conexion.AjustarStockDispositivoAgua(id, cantidad, operacion);
        }
        public void AjustarStockEquipo(int id, int cantidad, int operacion)
        {
            conexion.AjustarStockEquipo(id, cantidad, operacion);
        }
        public void CambiarEstadoPedido(int id, string estado)
        {
            conexion.CambiarEstadoPedido(id, estado);
        }
    }
}
