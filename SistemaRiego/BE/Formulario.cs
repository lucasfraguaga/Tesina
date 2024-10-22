using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
	public class Formulario
	{
        private int IdFormulario;

        public int idFormulario
        {
            get { return IdFormulario; }
            set { IdFormulario = value; }
        }
        private int IdCliente;

        public int idCliente
        {
            get { return IdCliente; }
            set { IdCliente = value; }
        }

        private bool Viabilidad;

        public bool viabilidad
        {
            get { return Viabilidad; }
            set { Viabilidad = value; }
        }

        private string EstadoPago;

        public string estadoPago
        {
            get { return EstadoPago; }
            set { EstadoPago = value; }
        }

        private string EstadoFabricacion;

        public string estadoFrabricacion
        {
            get { return EstadoFabricacion; }
            set { EstadoFabricacion = value; }
        }
        private string EstadoInstalacion;

        public string estadoInstalacion
        {
            get { return EstadoInstalacion; }
            set { EstadoInstalacion = value; }
        }

        private List<Cultivo> Cultivos;

		public List<Cultivo> cultivos
		{
			get { return Cultivos; }
			set { Cultivos = value; }
		}

		private string DescripcionAgua;

		public string descripcionAgua
		{
			get { return DescripcionAgua; }
			set { DescripcionAgua = value; }
		}
		private string DescripcionZona;

		public string desscipcionZona
		{
			get { return DescripcionZona; }
			set { DescripcionZona = value; }
		}
		private bool DisponibilidadAgua;

		public bool disponibilidadAgua
		{
			get { return DisponibilidadAgua; }
			set { DisponibilidadAgua = value; }
		}
		private string Direccion;

		public string direccion
		{
			get { return Direccion; }
			set { Direccion = value; }
		}
		private List<DispositivoAgua> DispositivosDeAgua;

		public List<DispositivoAgua> dispositivosDeAgua
		{
			get { return DispositivosDeAgua; }
			set { DispositivosDeAgua = value; }
		}
		private string DistanciaCubrir;

		public string distanciaCubrir
		{
			get { return DistanciaCubrir; }
			set { DistanciaCubrir = value; }
		}
		private List<Sensor> Mediciones;

		public List<Sensor> mediciones
		{
			get { return Mediciones; }
			set { Mediciones = value; }
		}

		private string DescripcionViabilidad;

		public string descripcionViabilidad
		{
			get { return DescripcionViabilidad; }
			set { DescripcionViabilidad = value; }
		}

        private bool ViabilidadEquipo;

        public bool viabilidadEquipo
        {
            get { return ViabilidadEquipo; }
            set { ViabilidadEquipo = value; }
        }
		private string DescripcionViabilidadEquipo;

		public string descripcionViabilidadEquipo
		{
			get { return DescripcionViabilidadEquipo; }
			set { DescripcionViabilidadEquipo = value; }
		}



	}
}
