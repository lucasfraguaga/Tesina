using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Seguridad;
using System.Windows.Forms;
using System.Data;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DAL
{
    public class Conexion
    {
        ManejadorEncriptado ManejadorEncriptado = new ManejadorEncriptado();
        private string GetConnectionString()
        {
            var cs = new SqlConnectionStringBuilder();
            cs.IntegratedSecurity = true;
            cs.DataSource = @"LACUCA\SQLEXPRESS";
            cs.InitialCatalog = "RiegoAutonomo";
            return cs.ConnectionString;
        }
        private string GetConnectionStringMaster()
        {
            var cs = new SqlConnectionStringBuilder();
            cs.IntegratedSecurity = true;
            cs.DataSource = @"LACUCA\SQLEXPRESS";
            cs.InitialCatalog = "master";
            return cs.ConnectionString;
        }

        public void guardarUsuario(string nom, string con)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("GuardarUsuario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("usuario", nom));
                cmd.Parameters.Add(new SqlParameter("contrasena", con));

                var id = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int guardarUsuarioConVuelta(string nom, string con)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("GuardarUsuario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("usuario", nom));
                cmd.Parameters.Add(new SqlParameter("contrasena", con));

                var id = cmd.ExecuteScalar();
                return (int)id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void cambiarContraseña(int id, string con)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("CambiarContrasenaUsuario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.Parameters.Add(new SqlParameter("nuevaContrasena", con));

                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void cambiarUsuario(int id, string con)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("CambiarUsuario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.Parameters.Add(new SqlParameter("nuevoUsuario", con));

                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        PermisosRepository PermisosRepository = new PermisosRepository();
        public Usuario validadUsuario(string nom, string con)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("BuscarUsuario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("nombre", nom));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nombreUsuario = reader.GetString(0);
                        string contrasena = reader.GetString(1);
                        int id = reader.GetInt32(2);
                        if (nombreUsuario == nom && ManejadorEncriptado.ValidarContraseña(con, contrasena))
                        {
                            Usuario usu = new Usuario();
                            usu.Nombre = nombreUsuario;
                            usu.Contrasena = contrasena;
                            usu.Id = id;
                            PermisosRepository.FillUserComponents(usu);
                            return usu;
                        }
                        else
                        {
                            MessageBox.Show("Contraseña incorrecta");
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemIdioma> TraerLenguaje(int id)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("GetContentByLanguage");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<ItemIdioma> list = new List<ItemIdioma>();
                cmd.Parameters.Add(new SqlParameter("id", id));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ContendId = reader.GetString(0);
                    string Contend = reader.GetString(1);
                    ItemIdioma idioma = new ItemIdioma();
                    idioma.contenido = Contend;
                    idioma.tag = ContendId;
                    list.Add(idioma);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioDisplay> ObtenerUsuarios()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ObtenerTodosLosUsuarios");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<UsuarioDisplay> list = new List<UsuarioDisplay>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string usuario = reader.GetString(2);
                    int id = reader.GetInt32(0);
                    UsuarioDisplay usu = new UsuarioDisplay();
                    usu.id = id;
                    usu.Usu = usuario;
                    list.Add(usu);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<LenguajeMenu> GetLenguages()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("GetLanguages");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<LenguajeMenu> list = new List<LenguajeMenu>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string content = reader.GetString(1);
                    LenguajeMenu lenguaje = new LenguajeMenu();
                    lenguaje.languageId = id;
                    lenguaje.languageName = content;
                    list.Add(lenguaje);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemIdiomaNuevoDisplay> GetContent()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("GetAllContent");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<ItemIdiomaNuevoDisplay> list = new List<ItemIdiomaNuevoDisplay>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string content = reader.GetString(1);
                    ItemIdiomaNuevoDisplay lenguaje = new ItemIdiomaNuevoDisplay();
                    lenguaje.id = id;
                    lenguaje.content = content;
                    lenguaje.traduccion = "";
                    list.Add(lenguaje);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CargarLenguaje(string code, string name)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("InsertLanguage");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("LanguageCode", code));
                cmd.Parameters.Add(new SqlParameter("LanguageName", name));

                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InsertVersionContentFromDotNet(List<ItemIdiomaNuevoDisplay> list)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("InsertVersionContent");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // List of items to insert
                DataTable items = new DataTable();
                items.Columns.Add("Id", typeof(int));
                items.Columns.Add("Traduccion", typeof(string));
                foreach (var item in list)
                {
                    items.Rows.Add(item.id, item.traduccion);
                }

                // Add the table-valued parameter
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Items",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.ItemIdiomaNuevoDisplayType",
                    Value = items
                };
                cmd.Parameters.Add(parameter);

                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CargarCliente(Cliente cliente)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AgregarCliente");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("nombre", cliente.nombre));
                cmd.Parameters.Add(new SqlParameter("apellido", cliente.apellido));
                cmd.Parameters.Add(new SqlParameter("dni", cliente.dni));
                cmd.Parameters.Add(new SqlParameter("mail", cliente.mail));
                cmd.Parameters.Add(new SqlParameter("telefono", cliente.telefono));

                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CrearFormulario()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AgregarFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Formulario ObtenerUltimoFormulario()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ObtenerUltimoFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Formulario formulario = new Formulario();

                var reader = cmd.ExecuteReader();
                reader.Read();
                formulario.idFormulario = reader.GetInt32(0);
                /*formulario.descripcionAgua = reader.GetString(1);
                formulario.desscipcionZona = reader.GetString(2);
                formulario.disponibilidadAgua = reader.GetBoolean(3);
                formulario.direccion = reader.GetString(4);
                formulario.distanciaCubrir = reader.GetInt32(5);
                formulario.estadoInstalacion = reader.GetString(6);
                formulario.estadoPago = reader.GetString(7);
                formulario.viabilidad = reader.GetBoolean(8);*/
                formulario.idCliente = reader.GetInt32(9);


                reader.Close();
                cnn.Close();
                return formulario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Formulario> ObtenerTodosLosFormularios()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ObtenerTodosLosFormularios");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<Formulario> list = new List<Formulario>();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Formulario formulario = new Formulario();


                    formulario.idFormulario = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    formulario.descripcionAgua = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1);
                    formulario.desscipcionZona = reader.IsDBNull(2) ? "Sin zona" : reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        formulario.disponibilidadAgua = reader.GetBoolean(3);
                    formulario.direccion = reader.IsDBNull(4) ? "Sin dirección" : reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        formulario.distanciaCubrir = reader.GetString(5);
                    formulario.estadoFrabricacion = reader.IsDBNull(6) ? "Sin estado" : reader.GetString(6);
                    formulario.estadoPago = reader.IsDBNull(7) ? "Sin estado" : reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        formulario.viabilidad = reader.GetBoolean(8);
                    if (!reader.IsDBNull(9))
                        formulario.idCliente = reader.GetInt32(9);
                    formulario.descripcionViabilidad = reader.IsDBNull(10) ? "Sin estado" : reader.GetString(10);
                    if (!reader.IsDBNull(11))
                        formulario.viabilidadEquipo = reader.GetBoolean(11);
                    formulario.descripcionViabilidadEquipo = reader.IsDBNull(12) ? "Sin estado" : reader.GetString(12);
                    formulario.estadoInstalacion = reader.IsDBNull(13) ? "Sin estado" : reader.GetString(13);

                    list.Add(formulario);
                }
                reader.Close();
                cnn.Close();
                return list;



            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Cultivo> CargarCultivosEnComboBox()
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerTodosLosCultivos", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Cultivo> list = new List<Cultivo>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0); // Columna id
                    string descripcion = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1); // Columna descripcion
                    string tipo = reader.IsDBNull(2) ? "Sin tipo" : reader.GetString(2); // Columna tipo

                    // Crear una instancia de Cultivo y añadirla al ComboBox
                    Cultivo cultivo = new Cultivo();
                    cultivo.id = id;
                    cultivo.descripcion = descripcion;
                    cultivo.tipo = tipo;
                    list.Add(cultivo);
                }

                reader.Close();
                return list;
            }
        }
        public int CargarCultivo(Cultivo cultivo)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AgregarCultivo");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("descripcion", cultivo.descripcion));
                cmd.Parameters.Add(new SqlParameter("tipo", cultivo.tipo));

                int newid = (int)cmd.ExecuteScalar();
                return newid;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ActualizarFormulario(Formulario formulario)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("descripcionAgua", formulario.descripcionAgua));
                cmd.Parameters.Add(new SqlParameter("descripcionZona", formulario.desscipcionZona));
                cmd.Parameters.Add(new SqlParameter("disponibilidadAgua", formulario.disponibilidadAgua));
                cmd.Parameters.Add(new SqlParameter("direccion", formulario.direccion));
                cmd.Parameters.Add(new SqlParameter("distanciaCubrir", formulario.distanciaCubrir));
                cmd.Parameters.Add(new SqlParameter("descripcionViabilidad", formulario.descripcionViabilidad));
                cmd.Parameters.Add(new SqlParameter("viabilidad", formulario.viabilidad));

                cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CargarFormularioxCultivo(Formulario formulario, Cultivo cultivo)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AsignarCultivoAFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("idCultivo", cultivo.id));
                cmd.Parameters.Add(new SqlParameter("idFormulario", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("cantidad", cultivo.cantidad));


                cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Cultivo> ObtenerTodosCultivosXFormulario(int id)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ObtenerCultivosPorFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("idFormulario", id));

                List<Cultivo> list = new List<Cultivo>();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cultivo cultivo = new Cultivo();
                    cultivo.id = reader.GetInt32(0);
                    cultivo.cantidad = reader.GetInt32(1);
                    cultivo.descripcion = reader.GetString(2);
                    cultivo.tipo = reader.GetString(3);
                    list.Add(cultivo);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void ActualizarViabilidadEquipo(int id, string descripcion, bool viabilidad)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarViabilidadEquipo");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.Parameters.Add(new SqlParameter("nuevaDescripcionViabilidadEquipo", descripcion));
                cmd.Parameters.Add(new SqlParameter("nuevaViabilidadEquipo", viabilidad));


                cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Sensor> ObtenerTodosLosSensores()
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerTodosLosSensores", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Sensor> list = new List<Sensor>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0); // Columna id
                    string descripcion = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1); // Columna descripcion
                    string nombre = reader.IsDBNull(2) ? "Sin nombre" : reader.GetString(2); // Columna tipo
                    double precio = reader.GetDouble(3); // Columna tipo
                    int stock   = reader.GetInt32(4);

                    Sensor sensor = new Sensor();
                    sensor.precio = (float)precio;
                    sensor.nombre = nombre;
                    sensor.descipcion = descripcion;
                    sensor.id = id;
                    sensor.stock = stock;

                    list.Add(sensor);
                }

                reader.Close();
                return list;
            }
        }
        public List<DispositivoAgua> ObtenerTodosLosDispositivosAgua()
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerTodosLosDispositivosAgua", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<DispositivoAgua> list = new List<DispositivoAgua>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0); // Columna id
                    string descripcion = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1); // Columna descripcion
                    string nombre = reader.IsDBNull(2) ? "Sin nombre" : reader.GetString(2); // Columna tipo
                    double precio = reader.IsDBNull(3) ? 0 : reader.GetDouble(3); // Columna tipo
                    int stock = reader.GetInt32(4);

                    DispositivoAgua agua = new DispositivoAgua();
                    agua.precio = (float)precio;
                    agua.nombre = nombre;
                    agua.descripcion = descripcion;
                    agua.id = id;
                    agua.stock = stock;

                    list.Add(agua);
                }

                reader.Close();
                return list;
            }
        }
        public void InsertarNuevoMaterial(Materiales materiales)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("InsertarNuevoMaterial");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("comentario", materiales.comentario));
                cmd.Parameters.Add(new SqlParameter("cantEquipos", materiales.cantEquipos));
                cmd.Parameters.Add(new SqlParameter("conductoAgua", materiales.conductoAgua));
                cmd.Parameters.Add(new SqlParameter("precioConducto", materiales.precioConductoAgua));
                cmd.Parameters.Add(new SqlParameter("precioEquipo", materiales.precioEquipo));
                cmd.Parameters.Add(new SqlParameter("idFormulario", materiales.idFormulario));


                cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CargarFormularioxSensores(Formulario formulario, Sensor sensor)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("InsertarNuevoSensorFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("idSensor", sensor.id));
                cmd.Parameters.Add(new SqlParameter("idFormulario", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("cantidad", sensor.cantidad));


                cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CargarFormularioxDositivosAgua(Formulario formulario, DispositivoAgua agua)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("InsertarNuevoDispositivoAguaFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("idDispositivoAgua", agua.id));
                cmd.Parameters.Add(new SqlParameter("idFormulario", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("cantidad", agua.cantidad));


                cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CargarSensor(Sensor sensor)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AgregarSensor");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("descripcion", sensor.descipcion));
                cmd.Parameters.Add(new SqlParameter("nombre", sensor.nombre));
                cmd.Parameters.Add(new SqlParameter("precio", sensor.precio));

                int newid = (int)cmd.ExecuteScalar();
                return newid;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CargarDispositivoAgua(DispositivoAgua agua)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AgregarDispositivoAgua");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("descripcion", agua.descripcion));
                cmd.Parameters.Add(new SqlParameter("nombre", agua.nombre));
                cmd.Parameters.Add(new SqlParameter("precio", agua.precio));

                int newid = (int)cmd.ExecuteScalar();
                return newid;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Sensor> obtenerSensoresSegunFormulario(Formulario fomrulario)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerSensoresConDetalles", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("idFormulario", fomrulario.idFormulario));

                SqlDataReader reader = cmd.ExecuteReader();

                List<Sensor> list = new List<Sensor>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0); // Columna id
                    string descripcion = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1); // Columna descripcion
                    double precio = reader.IsDBNull(2) ? 0 : reader.GetDouble(2); // Columna tipo
                    int cantidad = reader.IsDBNull(3) ? 0 : int.Parse(reader.GetString(3)); // Columna tipo
                    string nombre = reader.IsDBNull(4) ? "Sin nombre" : reader.GetString(4); // Columna descripcion

                    Sensor sensor = new Sensor();
                    sensor.precio = (float)precio;
                    sensor.descipcion = descripcion;
                    sensor.id = id;
                    sensor.cantidad = cantidad;
                    sensor.nombre = nombre;


                    list.Add(sensor);
                }

                reader.Close();
                return list;
            }
        }
        public List<DispositivoAgua> ObtenerTodosLosDispositivosAguaSegunFormulario(Formulario formulario)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerDispositivosAguaConDetalles", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("idFormulario", formulario.idFormulario));
                SqlDataReader reader = cmd.ExecuteReader();

                List<DispositivoAgua> list = new List<DispositivoAgua>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0); // Columna id
                    string descripcion = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1); // Columna descripcion
                    double precio = reader.IsDBNull(2) ? 0 : reader.GetDouble(2); // Columna tipo
                    int cantidad = reader.IsDBNull(3) ? 0 : reader.GetInt32(3); // Columna tipo
                    string nombre = reader.IsDBNull(4) ? "Sin nombre" : reader.GetString(4); // Columna descripcion

                    DispositivoAgua agua = new DispositivoAgua();
                    agua.precio = (float)precio;
                    agua.descripcion = descripcion;
                    agua.id = id;
                    agua.cantidad = cantidad;
                    agua.nombre = nombre;


                    list.Add(agua);
                }

                reader.Close();
                return list;
            }
        }
        public Materiales obtenerMaterialesSegunFormulario(Formulario formulario)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerMaterialPorID", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id", formulario.idFormulario));
                cnn.Open();

                Materiales materiales = new Materiales();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {


                    materiales.id = reader.GetInt32(0); // Columna id
                    materiales.comentario = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1); // Columna descripcion
                    materiales.cantEquipos = reader.IsDBNull(2) ? 0 : reader.GetInt32(2); // Columna tipo
                    materiales.conductoAgua = reader.IsDBNull(3) ? "" : reader.GetString(3); // Columna tipo
                    materiales.precioConductoAgua = (float)(reader.IsDBNull(4) ? 0 : reader.GetDouble(4)); // Columna tipo
                    materiales.precioEquipo = (float)(reader.IsDBNull(5) ? 0 : reader.GetDouble(5)); // Columna tipo
                    materiales.idFormulario = formulario.idFormulario; // Columna tipo
                }

                reader.Close();
                return materiales;
            }
        }
        public void CargarPago(Formulario formulario, string estado)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarEstadoPago");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("estadoPago", estado));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CargarEstadoInstalacion(Formulario formulario, string estado)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarEstadoFabricacion");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("estadoFabricacion", estado));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CargarFechaInstalacionVendedor(Formulario formulario, FechaInstalacion fecha)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AgregarFechaInstalacion");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id_Formulario", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("fecha", fecha.fecha));
                cmd.Parameters.Add(new SqlParameter("descripcion", fecha.descripcion));
                cmd.Parameters.Add(new SqlParameter("estado", fecha.estado));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FechaInstalacion ObtenerFechasInstalacionPorFormulario(Formulario fomrulario)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerFechasInstalacionPorFormulario", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("id_Formulario", fomrulario.idFormulario));

                SqlDataReader reader = cmd.ExecuteReader();

                FechaInstalacion fecha = null;

                while (reader.Read())
                {
                    fecha = new FechaInstalacion();
                    int idInstalacion = reader.GetInt32(reader.GetOrdinal("id_Instalacion"));
                    int idFormularioDb = reader.GetInt32(reader.GetOrdinal("id_Formulario"));
                    DateTime fechaa = reader.GetDateTime(reader.GetOrdinal("fecha"));
                    string descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                    string estado = reader.GetString(reader.GetOrdinal("estado"));

                    fecha.id_Formulario = idFormularioDb;
                    fecha.descripcion = descripcion;
                    fecha.estado = estado;
                    fecha.fecha = fechaa;
                    fecha.id = idInstalacion;
                }

                reader.Close();
                return fecha;
            }
        }
        public void RecargarFechaInstalacion(Formulario formulario, FechaInstalacion fecha)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarEstadoYFechaInstalacion");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id_Formulario", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("nuevaFecha", fecha.fecha));
                cmd.Parameters.Add(new SqlParameter("nuevoEstado", fecha.estado));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<FechaInstalacion> ObtenerFechasInstalacionPorEstado(string estadoo)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerFechasInstalacionPorEstado", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                cmd.Parameters.Add(new SqlParameter("estado", estadoo));

                SqlDataReader reader = cmd.ExecuteReader();

                List<FechaInstalacion> list = new List<FechaInstalacion>();
                FechaInstalacion fecha;

                while (reader.Read())
                {
                    fecha = new FechaInstalacion();
                    int idInstalacion = reader.GetInt32(reader.GetOrdinal("id_Instalacion"));
                    int idFormularioDb = reader.GetInt32(reader.GetOrdinal("id_Formulario"));
                    DateTime fechaa = reader.GetDateTime(reader.GetOrdinal("fecha"));
                    string descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                    string estado = reader.GetString(reader.GetOrdinal("estado"));

                    fecha.id_Formulario = idFormularioDb;
                    fecha.descripcion = descripcion;
                    fecha.estado = estado;
                    fecha.fecha = fechaa;
                    fecha.id = idInstalacion;
                    list.Add(fecha);

                }

                reader.Close();
                return list;
            }
        }
        public void ActualizarEstadoFecha(Formulario formulario, string estado)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarEstadoFechaInstalacion");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id_Formulario", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("nuevoEstado", estado));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CrearPedidoCreacionUsuario(Formulario formulario, string estado)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AgregarPedidoCreacion");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("idFormulario", formulario.idFormulario));
                cmd.Parameters.Add(new SqlParameter("estado", estado));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<PedidoCreacion> ObtenerTodosLosPediosCreacion()
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerTodosLosPedidosCreacion", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<PedidoCreacion> list = new List<PedidoCreacion>();
                PedidoCreacion pedido;

                while (reader.Read())
                {
                    pedido = new PedidoCreacion();
                    pedido.id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); // Columna descripcion
                    pedido.idFormulario = reader.IsDBNull(1) ? 0 : reader.GetInt32(1); // Columna tipo
                    pedido.contraseña = reader.IsDBNull(2) ? "" : reader.GetString(2); // Columna tipo
                    pedido.usuario = reader.IsDBNull(3) ? "" : reader.GetString(3); // Columna tipo
                    pedido.estado = reader.IsDBNull(4) ? "" : reader.GetString(4); // Columna tipo


                    list.Add(pedido);

                }

                reader.Close();
                return list;
            }
        }
        public Formulario ObtenerFormularioPorId(int id)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ObtenerFormulario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id", id));
                Formulario formulario = new Formulario();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {



                    formulario.idFormulario = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    formulario.descripcionAgua = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1);
                    formulario.desscipcionZona = reader.IsDBNull(2) ? "Sin zona" : reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        formulario.disponibilidadAgua = reader.GetBoolean(3);
                    formulario.direccion = reader.IsDBNull(4) ? "Sin dirección" : reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        formulario.distanciaCubrir = reader.GetString(5);
                    formulario.estadoFrabricacion = reader.IsDBNull(6) ? "Sin estado" : reader.GetString(6);
                    formulario.estadoPago = reader.IsDBNull(7) ? "Sin estado" : reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        formulario.viabilidad = reader.GetBoolean(8);
                    if (!reader.IsDBNull(9))
                        formulario.idCliente = reader.GetInt32(9);
                    formulario.descripcionViabilidad = reader.IsDBNull(10) ? "Sin estado" : reader.GetString(10);
                    if (!reader.IsDBNull(11))
                        formulario.viabilidadEquipo = reader.GetBoolean(11);
                    formulario.descripcionViabilidadEquipo = reader.IsDBNull(12) ? "Sin estado" : reader.GetString(12);
                    formulario.estadoInstalacion = reader.IsDBNull(13) ? "Sin estado" : reader.GetString(13);

                }
                reader.Close();
                cnn.Close();
                return formulario;



            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void ActualizarContraseñaYUsuarioPedido(PedidoCreacion pedido, string usuario, string contraseña)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarContraseñaYUsuario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("idPedido", pedido.id));
                cmd.Parameters.Add(new SqlParameter("contrasena", contraseña));
                cmd.Parameters.Add(new SqlParameter("usuario", usuario));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void ActualizarIdUsuarioCliente(Formulario formulario, int id)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarIdUsuarioCliente");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("idCliente", formulario.idCliente));
                cmd.Parameters.Add(new SqlParameter("idUsuario", id));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool generarBackup()
        {
            string backupPath = @"C:\Users\lucas\Desktop\campo\SistemaRiego\RiegoAutonomoBackup.bak"; //direccion de guardado de backup
            string connectionString = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("BackupBD", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RutaBackup", backupPath);

                    // Aumentar el tiempo de espera del comando a 600 segundos (10 minutos) ya que puede tirar time out
                    command.CommandTimeout = 600;

                    try
                    {
                        connection.Open();


                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                }
            }
        }
        public bool restaurarBase()
        {
            string backupPath = @"C:\Users\lucas\Desktop\campo\SistemaRiego\RiegoAutonomoBackup.bak"; // direccion donde esta el backup
            string connectionString = GetConnectionStringMaster();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("RestoreRiegoAutonomo", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RutaBackup", backupPath);

                    // Aumentar el tiempo de espera del comando a 600 segundos (10 minutos) por posible time out
                    command.CommandTimeout = 600;

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                }
            }
        }
        public void InsertarBitacora(Usuario usu, string mensaje)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("InsertarBitacora");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("idUsuario", usu.Id));
                cmd.Parameters.Add(new SqlParameter("mensaje", mensaje));
                cmd.Parameters.Add(new SqlParameter("fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));


                var id = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Bitacora> listarBitacora()
        {
            List<Bitacora> listBit = new List<Bitacora>();
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ListarRegistrosBitacora");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bitacora bitacora = new Bitacora();
                        bitacora.id = reader.GetInt32(0);
                        bitacora.idUsuario = reader.GetInt32(1);
                        bitacora.fecha = reader.GetDateTime(2);
                        bitacora.mensaje = reader.GetString(3);
                        bitacora.estado = reader.GetString(4);
                        listBit.Add(bitacora);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return listBit;
        }
        public string VerificarIntegridadTabla()
        {
            string resultado = "";
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("VerificarIntegridadTabla");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                resultado = cmd.ExecuteScalar().ToString();
                
            }
            catch (Exception e)
            {

            }
            return resultado;
        }
        public List<UsuarioAuditoria> ObtenerCambiosUsuarios(int id)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("MostrarHistorialUsuario");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id_usuario", id));


                List<UsuarioAuditoria> list = new List<UsuarioAuditoria>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UsuarioAuditoria usu = new UsuarioAuditoria();
                    usu.idAuditoria = reader.GetInt32(0);
                    usu.idUsuario = reader.GetInt32(1);
                    usu.nombre = reader.GetString(2);
                    usu.contrasena = reader.GetString(3);
                    usu.tipoOperacion = reader.GetString(4);
                    usu.fechaCambio = reader.GetDateTime(5);
                    list.Add(usu);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<UsuarioAuditoria> ObtenerUsuariosEliminados()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ListarUsuariosEliminados");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                List<UsuarioAuditoria> list = new List<UsuarioAuditoria>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UsuarioAuditoria usu = new UsuarioAuditoria();
                    usu.idAuditoria = reader.GetInt32(0);
                    usu.idUsuario = reader.GetInt32(1);
                    usu.nombre = reader.GetString(2);
                    usu.contrasena = reader.GetString(3);
                    usu.fechaCambio = reader.GetDateTime(4);
                    usu.tipoOperacion = "DELETE";
                    list.Add(usu);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void cambiarUsuarioYContraseña(Usuario usu)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ActualizarUsuarioYContrasena");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("id_usuario", usu.Id));
                cmd.Parameters.Add(new SqlParameter("nuevo_nombre", usu.Nombre));
                cmd.Parameters.Add(new SqlParameter("nueva_contrasena", usu.Contrasena));

                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Equipo> ObtenerTodosLosEquipos()
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ObtenerTodoElEquipo", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Equipo> list = new List<Equipo>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0); // Columna id
                    string descripcion = reader.IsDBNull(1) ? "Sin descripción" : reader.GetString(1); // Columna descripcion
                    int stock = reader.GetInt32(2);

                    Equipo equipo = new Equipo();
                    equipo.stock = stock;
                    equipo.id = id;
                    equipo.descripcion = descripcion;

                    list.Add(equipo);
                }

                reader.Close();
                return list;
            }
        }
        public int CrearPedidoCompra(float precio,string estado)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("CrearPedidoCompra");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("precio", precio));
                cmd.Parameters.Add(new SqlParameter("estado", estado));

                var idParameter = new SqlParameter("@nuevoId", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(idParameter);

                cmd.ExecuteNonQuery(); 

                return (int)idParameter.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CrearPedidoSensor(int idPedido, int idSensor, int cantidad, float precio)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("CrearPedidoSensor");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("idPedidoCompra", idPedido));
                cmd.Parameters.Add(new SqlParameter("idSensor", idSensor));
                cmd.Parameters.Add(new SqlParameter("cantidad", cantidad));
                cmd.Parameters.Add(new SqlParameter("precio", precio));

                var idParameter = new SqlParameter("@nuevoId", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(idParameter);

                cmd.ExecuteNonQuery();

                return (int)idParameter.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CrearPedidoEquipo(int idPedido, int idEquipo, int cantidad, float precio)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("CrearPedidoEquipo");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("idPedidoCompra", idPedido));
                cmd.Parameters.Add(new SqlParameter("idEquipo", idEquipo));
                cmd.Parameters.Add(new SqlParameter("cantidad", cantidad));
                cmd.Parameters.Add(new SqlParameter("precio", precio));

                var idParameter = new SqlParameter("@nuevoId", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(idParameter);

                cmd.ExecuteNonQuery();

                return (int)idParameter.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CrearPedidoDispositivoAgua(int idPedido, int idDispositivoAgua, int cantidad, float precio)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("CrearPedidoDispositivoAgua");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("idPedidoCompra", idPedido));
                cmd.Parameters.Add(new SqlParameter("idDispositivoAgua", idDispositivoAgua));
                cmd.Parameters.Add(new SqlParameter("cantidad", cantidad));
                cmd.Parameters.Add(new SqlParameter("precio", precio));

                var idParameter = new SqlParameter("@nuevoId", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(idParameter);

                cmd.ExecuteNonQuery();

                return (int)idParameter.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<MapperCarritoCompra> ObtenerTodosLosPedidosCompra()
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ObtenerTodosLosPedidosCompra");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                List<MapperCarritoCompra> list = new List<MapperCarritoCompra>();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MapperCarritoCompra carrito = new MapperCarritoCompra();
                    carrito.idPedido = reader.GetInt32(0);
                    carrito.precio = (float)(reader.IsDBNull(1) ? 0 : reader.GetDouble(1));
                    carrito.estado = reader.GetString(2);
                    list.Add(carrito);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<MapperCarritoCompra> ObtenerProductosPorIdPedido(int id)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("ObtenerProductosPorIdPedido");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("idPedido", id));

                List<MapperCarritoCompra> list = new List<MapperCarritoCompra>();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MapperCarritoCompra carrito = new MapperCarritoCompra();
                    carrito.estado = reader.GetString(0);
                    carrito.idProducto = reader.GetInt32(2);
                    carrito.cantidad = reader.GetInt32(3);
                    carrito.precio = (float)(reader.IsDBNull(4) ? 0 : reader.GetDouble(4));
                    carrito.idPedido = id;
                    list.Add(carrito);
                }
                reader.Close();
                cnn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void AjustarStockSensor(int id, int cantidad, int operacion)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AjustarStockSensor");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));
                cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                cmd.Parameters.Add(new SqlParameter("Aumentar", operacion));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void AjustarStockDispositivoAgua(int id, int cantidad, int operacion)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AjustarStockDispositivoAgua");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));
                cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                cmd.Parameters.Add(new SqlParameter("Aumentar", operacion));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void AjustarStockEquipo(int id, int cantidad, int operacion)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("AjustarStockEquipo");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));
                cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                cmd.Parameters.Add(new SqlParameter("Aumentar", operacion));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CambiarEstadoPedido(int id, string estado)
        {
            try
            {
                var cnn = new SqlConnection(GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand("CambiarEstadoPedido");
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));
                cmd.Parameters.Add(new SqlParameter("NuevoEstado", estado));


                cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
