using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLGestorFormulario
    {
        Conexion conexion = new Conexion();
        public void CargarCliente(Cliente cliente)
        {
            conexion.CargarCliente(cliente);
        }
        public void CrearFormulario()
        {
            conexion.CrearFormulario();
        }
        public Formulario ObtenerUltimoFormulario()
        {
            return conexion.ObtenerUltimoFormulario();
        }
        public List<Formulario> ObtenerTodosLosFormularios()
        {
            return conexion.ObtenerTodosLosFormularios();
        }
        public List<Cultivo> CargarCultivosEnComboBox()
        {
            return conexion.CargarCultivosEnComboBox();
        }
        public int CargarCultivo(Cultivo cultivo)
        {
            return conexion.CargarCultivo(cultivo);
        }
        public void ActualizarFormulario(Formulario formulario)
        {
            conexion.ActualizarFormulario(formulario);
        }
        public void CargarFormularioxCultivo(Formulario formulario, Cultivo cultivo)
        {
            conexion.CargarFormularioxCultivo(formulario,cultivo);
        }
        public List<Cultivo> ObtenerTodosCultivosXFormulario(int id)
        {
            return conexion.ObtenerTodosCultivosXFormulario(id);
        }
        public void ActualizarViabilidadEquipo(int id, string descripcion, bool viabilidad)
        {
            conexion.ActualizarViabilidadEquipo(id,descripcion,viabilidad);
        }
        public List<Sensor> ObtenerTodosLosSensores()
        {
            return conexion.ObtenerTodosLosSensores();
        }
        public List<DispositivoAgua> ObtenerTodosLosDispositivosAgua()
        {
            return conexion.ObtenerTodosLosDispositivosAgua();
        }
        public void InsertarNuevoMaterial(Materiales materiales)
        {
            conexion.InsertarNuevoMaterial(materiales);
        }
        public void CargarFormularioxSensores(Formulario formulario, Sensor sensor)
        {
            conexion.CargarFormularioxSensores(formulario, sensor);
        }
        public void CargarFormularioxDositivosAgua(Formulario formulario, DispositivoAgua agua)
        {
            conexion.CargarFormularioxDositivosAgua(formulario,agua);
        }
        public int CargarSensor(Sensor sensor)
        {
            return conexion.CargarSensor(sensor);
        }
        public int CargarDispositivoAgua(DispositivoAgua agua)
        {
            return conexion.CargarDispositivoAgua(agua);
        }
        public List<Sensor> obtenerSensoresSegunFormulario(Formulario formulario)
        {
            return conexion.obtenerSensoresSegunFormulario(formulario);
        }
        public List<DispositivoAgua> ObtenerTodosLosDispositivosAguaSegunFormulario(Formulario formulario)
        {
            return conexion.ObtenerTodosLosDispositivosAguaSegunFormulario(formulario);
        }
        public Materiales obtenerMaterialesSegunFormulario(Formulario formulario)
        {
            return conexion.obtenerMaterialesSegunFormulario(formulario);
        }
        public void CargarPago(Formulario formulario, string estado)
        {
            conexion.CargarPago(formulario,estado);
        }
        public void CargarEstadoInstalacion(Formulario formulario, string estado)
        {
            conexion.CargarEstadoInstalacion(formulario, estado);
        }
        public void CargarFechaInstalacionVendedor(Formulario formulario, FechaInstalacion fecha)
        {
            conexion.CargarFechaInstalacionVendedor(formulario, fecha);
        }
        public FechaInstalacion ObtenerFechasInstalacionPorFormulario(Formulario fomrulario)
        {
            return conexion.ObtenerFechasInstalacionPorFormulario(fomrulario);
        }
        public void RecargarFechaInstalacion(Formulario formulario, FechaInstalacion fecha)
        {
            conexion.RecargarFechaInstalacion(formulario, fecha);
        }
        public List<FechaInstalacion> ObtenerFechasInstalacionPorEstado(string estadoo)
        {
            return conexion.ObtenerFechasInstalacionPorEstado(estadoo);
        }
        public void ActualizarEstadoFecha(Formulario formulario, string estado)
        {
            conexion.ActualizarEstadoFecha(formulario, estado);
        }
        public void CrearPedidoCreacionUsuario(Formulario formulario, string estado)
        {
            conexion.CrearPedidoCreacionUsuario(formulario, estado);
        }
        public List<PedidoCreacion> ObtenerTodosLosPediosCreacion()
        {
            return conexion.ObtenerTodosLosPediosCreacion();
        }
        public Formulario ObtenerFormularioPorId(int id)
        {
            return conexion.ObtenerFormularioPorId(id);
        }
        public void ActualizarContraseñaYUsuarioPedido(PedidoCreacion pedido, string usuario, string contraseña)
        {
            conexion.ActualizarContraseñaYUsuarioPedido(pedido, usuario, contraseña);
        }
        public void ActualizarIdUsuarioCliente(Formulario formulario, int id)
        {
            conexion.ActualizarIdUsuarioCliente(formulario, id);
        }
        public int guardarUsuarioConVuelta(string nom, string con)
        {
            return conexion.guardarUsuarioConVuelta(nom,con);
        }
    }
}
