using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bitacora
    {
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}
		private int IdUsuario;

		public int idUsuario
		{
			get { return IdUsuario; }
			set { IdUsuario = value; }
		}
		private string Mensaje;

		public string mensaje
		{
			get { return Mensaje; }
			set { Mensaje = value; }
		}
		private DateTime Fecha;

		public DateTime fecha
		{
			get { return Fecha; }
			set { Fecha = value; }
		}
		private string Estado;

		public string estado
		{
			get { return Estado; }
			set { Estado = value; }
		}
		public byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static Bitacora Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                IFormatter formatter = new BinaryFormatter();
                return (Bitacora)formatter.Deserialize(ms);
            }
        }
    }
}
