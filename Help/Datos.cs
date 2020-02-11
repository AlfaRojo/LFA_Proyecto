using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFA_Proyecto.Modelos;

namespace LFA_Proyecto.Help
{
    class Datos
    {
        private static Datos _instance = null;
        public static Datos Instance
        {
            get
            {
                if (_instance == null) _instance = new Datos();
                {
                    return _instance;
                }
            }
        }
        public Dictionary<string, string> DiccionarioColeccion = new Dictionary<string, string>();
        public List<Valores> listaValores = new List<Valores>();
        //Agregar
    }
}
