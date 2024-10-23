using BE;
using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public List<Equipo> ObtenerTodosLosEquipos()
        {
            return conexion.ObtenerTodosLosEquipos();
        }
        public List<MapperMaterialesRequeridos> ObtenerMaterialesRequeridos()
        {
            List<MapperMaterialesRequeridos> materialesRequeridos = new List<MapperMaterialesRequeridos>();
            List<Formulario> formularios = conexion.ObtenerTodosLosFormularios();
            foreach (var item in formularios)
            {
                if (item.estadoFrabricacion == "en fabricacion")
                {
                    Materiales material = obtenerMaterialesSegunFormulario(item);
                    MapperMaterialesRequeridos elementoExistente = materialesRequeridos.FirstOrDefault(e => e.id == 1 && e.tipo == "equipo");
                    if (elementoExistente != null)
                    {
                        elementoExistente.cantidad += material.cantEquipos;
                        elementoExistente = null;
                    }
                    else
                    {
                        elementoExistente = new MapperMaterialesRequeridos();
                        elementoExistente.id = 1;
                        elementoExistente.cantidad = material.cantEquipos;
                        elementoExistente.tipo = "equipo";
                        materialesRequeridos.Add(elementoExistente);
                        elementoExistente = null;
                    }
                    List<Sensor> sensores = obtenerSensoresSegunFormulario(item);
                    foreach (var item1 in sensores)
                    {
                        elementoExistente = materialesRequeridos.FirstOrDefault(e => e.id == item1.id && e.tipo == "sensor");
                        if (elementoExistente != null)
                        {
                            elementoExistente.cantidad += item1.cantidad;
                            elementoExistente = null;
                        }
                        else
                        {
                            elementoExistente = new MapperMaterialesRequeridos();
                            elementoExistente.id = item1.id;
                            elementoExistente.cantidad = item1.cantidad;
                            elementoExistente.tipo = "sensor";
                            materialesRequeridos.Add(elementoExistente);
                            elementoExistente = null;
                        }
                    }
                    List<DispositivoAgua> agua = ObtenerTodosLosDispositivosAguaSegunFormulario(item);
                    foreach (var item2 in agua)
                    {
                        elementoExistente = materialesRequeridos.FirstOrDefault(e => e.id == item2.id && e.tipo == "dispositivoAgua");
                        if (elementoExistente != null)
                        {
                            elementoExistente.cantidad += item2.cantidad;
                            elementoExistente = null;
                        }
                        else
                        {
                            elementoExistente = new MapperMaterialesRequeridos();
                            elementoExistente.id = item2.id;
                            elementoExistente.cantidad = item2.cantidad;
                            elementoExistente.tipo = "dispositivoAgua";
                            materialesRequeridos.Add(elementoExistente);
                            elementoExistente = null;
                        }
                    }
                }
            }
            return materialesRequeridos;
        }
        public List<MapperMaterialesRequeridos> ObtenerMaterialesRequeridosConStock()
        {
            List<MapperMaterialesRequeridos> materialesRequeridos = new List<MapperMaterialesRequeridos>();
            List<Formulario> formularios = conexion.ObtenerTodosLosFormularios();
            List<Sensor> sensoresTotal = ObtenerTodosLosSensores();
            foreach (var item in sensoresTotal)
            {
                MapperMaterialesRequeridos elemento = materialesRequeridos.FirstOrDefault(e => e.id == item.id && e.tipo == "sensor");
                    elemento = materialesRequeridos.FirstOrDefault(e => e.id == item.id && e.tipo == "sensor");
                    if (elemento != null)
                    {
                        elemento.stock += item.stock;
                        elemento = null;
                    }
                    else
                    {
                        elemento = new MapperMaterialesRequeridos();
                        elemento.id = item.id;
                        elemento.stock = item.stock;
                        elemento.tipo = "sensor";
                        elemento.descripcion = item.descipcion;
                        elemento.precio = item.precio;
                        materialesRequeridos.Add(elemento);
                        elemento = null;
                    }
            }
            List<DispositivoAgua> aguaTotal = ObtenerTodosLosDispositivosAgua();
            foreach (var item2 in aguaTotal)
            {
                MapperMaterialesRequeridos elemento1 = materialesRequeridos.FirstOrDefault(e => e.id == item2.id && e.tipo == "dispositivoAgua");
                if (elemento1 != null)
                {
                    elemento1 = null;
                }
                else
                {
                    elemento1 = new MapperMaterialesRequeridos();
                    elemento1.id = item2.id;
                    elemento1.stock = item2.stock;
                    elemento1.tipo = "dispositivoAgua";
                    elemento1.descripcion = item2.descripcion;
                    elemento1.precio = item2.precio;
                    materialesRequeridos.Add(elemento1);
                    elemento1 = null;
                }
            }
            List<Equipo> material1 = ObtenerTodosLosEquipos();
            foreach (var item2 in material1)
            {
                MapperMaterialesRequeridos elemento1 = materialesRequeridos.FirstOrDefault(e => e.id == item2.id && e.tipo == "equipo");
                if (elemento1 != null)
                {
                    elemento1 = null;
                }
                else
                {
                    elemento1 = new MapperMaterialesRequeridos();
                    elemento1.id = item2.id;
                    elemento1.stock = item2.stock;
                    elemento1.tipo = "equipo";
                    elemento1.descripcion = item2.descripcion;
                    elemento1.precio = 100;
                    materialesRequeridos.Add(elemento1);
                    elemento1 = null;
                }
            }


            foreach (var item in formularios)
            {
                if (item.estadoFrabricacion == "en fabricacion")
                {
                    Materiales material = obtenerMaterialesSegunFormulario(item);
                    MapperMaterialesRequeridos elementoExistente = materialesRequeridos.FirstOrDefault(e => e.id == 1 && e.tipo == "equipo");
                    if (elementoExistente != null)
                    {
                        elementoExistente = null;
                    }
                    else
                    {
                        elementoExistente = new MapperMaterialesRequeridos();
                        elementoExistente.id = 1;
                        elementoExistente.cantidad = material.cantEquipos;
                        elementoExistente.tipo = "equipo";
                        materialesRequeridos.Add(elementoExistente);
                        elementoExistente = null;
                    }
                    List<Sensor> sensores = obtenerSensoresSegunFormulario(item);
                    foreach (var item1 in sensores)
                    {
                        elementoExistente = materialesRequeridos.FirstOrDefault(e => e.id == item1.id && e.tipo == "sensor");
                        if (elementoExistente != null)
                        {
                            elementoExistente.cantidad += item1.cantidad;
                            elementoExistente = null;
                        }
                        else
                        {
                            elementoExistente = new MapperMaterialesRequeridos();
                            elementoExistente.id = item1.id;
                            elementoExistente.cantidad = item1.cantidad;
                            elementoExistente.tipo = "sensor";
                            materialesRequeridos.Add(elementoExistente);
                            elementoExistente = null;
                        }
                    }
                    List<DispositivoAgua> agua = ObtenerTodosLosDispositivosAguaSegunFormulario(item);
                    foreach (var item2 in agua)
                    {
                        elementoExistente = materialesRequeridos.FirstOrDefault(e => e.id == item2.id && e.tipo == "dispositivoAgua");
                        if (elementoExistente != null)
                        {
                            elementoExistente.cantidad += item2.cantidad;
                            elementoExistente = null;
                        }
                        else
                        {
                            elementoExistente = new MapperMaterialesRequeridos();
                            elementoExistente.id = item2.id;
                            elementoExistente.cantidad = item2.cantidad;
                            elementoExistente.tipo = "dispositivoAgua";
                            materialesRequeridos.Add(elementoExistente);
                            elementoExistente = null;
                        }
                    }
                }
            }
            return materialesRequeridos;
        }
        public int CrearPedidoCompra(float precio, string estado)
        {
            return conexion.CrearPedidoCompra(precio, estado);
        }
        public int CrearPedidoSensor(int idPedido, int idSensor, int cantidad, float precio)
        {
            return conexion.CrearPedidoSensor(idPedido, idSensor, cantidad, precio);
        }
        public int CrearPedidoEquipo(int idPedido, int idEquipo, int cantidad, float precio)
        {
            return conexion.CrearPedidoEquipo(idPedido,idEquipo,cantidad,precio);
        }
        public int CrearPedidoDispositivoAgua(int idPedido, int idDispositivoAgua, int cantidad, float precio)
        {
            return conexion.CrearPedidoDispositivoAgua(idPedido,idDispositivoAgua,cantidad,precio);
        }
    }
}
