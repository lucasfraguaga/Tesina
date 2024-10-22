using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ItemIdiomaNuevoDisplay
    {
		private int Id;

		public int id
		{
			get { return Id; }
			set { Id = value; }
		}

		private string Content;

		public string content
		{
			get { return Content; }
			set { Content = value; }
		}

		private string Traduccion;

		public string traduccion
		{
			get { return Traduccion; }
			set { Traduccion = value; }
		}



	}
}
