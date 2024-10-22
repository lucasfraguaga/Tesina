using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BLL
{
    public class BLLGetorIdiomas
    {
        Conexion conexion = new Conexion();
        public void cambiarIdioma(List<object> objetosConTag)
        {
            List<ItemIdioma> listaIdioma = new List<ItemIdioma>();
            listaIdioma = conexion.TraerLenguaje((ObserverLenguaje.GetLenguaje.idioma.languageId));

            foreach (var objeto in objetosConTag)
            {
                if (objeto is Control control)
                {
                    string tagToFind = control.Tag?.ToString();
                    string textoIdioma = listaIdioma.FirstOrDefault(aux => aux.tag.Equals(tagToFind, StringComparison.OrdinalIgnoreCase))?.contenido;
                    if (textoIdioma != null)
                    {
                        control.Text = textoIdioma;
                    }
                }
                if (objeto is ToolStripMenuItem menuItem)
                {
                    string newText = listaIdioma.FirstOrDefault(aux => string.Equals(aux.tag, menuItem.Tag?.ToString(), StringComparison.OrdinalIgnoreCase))?.contenido;
                    if (!string.IsNullOrEmpty(newText))
                    {
                        menuItem.Text = newText;
                    }
                }
            }
        }

        public List<ItemIdiomaNuevoDisplay> GetContent()
        {
            return conexion.GetContent();
        }

        public void CargarLenguaje(string code, string name)
        {
            conexion.CargarLenguaje(code, name);
        }
        public void InsertVersionContentFromDotNet(List<ItemIdiomaNuevoDisplay> list)
        {
            conexion.InsertVersionContentFromDotNet(list);
        }
    }
}
