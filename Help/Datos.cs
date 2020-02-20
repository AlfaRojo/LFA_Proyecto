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
        public Dictionary<string, string> diccionarioColeccion = new Dictionary<string, string>();//Para que el usuario pueda modificar luego
        public Stack<string> pilaArbol = new Stack<string>();//Arbol de expresiones
        public Stack<string> pilaAux = new Stack<string>();//Se usa para guardar los TOKENS
        public List<string> eTOKEN = new List<string>();//Guardando la expresion regular para comprobar en las pilas
        public List<string> listaSets = new List<string>();//Almacena todo de SETS
        public List<string> listaToken = new List<string>();//Almacena todo de TOKENS
        public List<string> listaAction = new List<string>();//Almacena todo de ACTIONS/RESERVADAS()
        public List<string> listaError = new List<string>();//Almacena todo de ERROR
        //Agregar
    }
}
