using System.Collections.Generic;
using LFA_Proyecto.Arbol;

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
        public Stack<ArbolB> PilaS = new Stack<ArbolB>();//Arbol de expresiones
        public Stack<string> PilaT = new Stack<string>();//Se usa para guardar los TOKENS

        public List<string> listaSets = new List<string>();//Almacena todo de SETS
        public List<string> listaToken = new List<string>();//Almacena todo de TOKENS
        public List<string> listaAction = new List<string>();//Almacena todo de ACTIONS/RESERVADAS()
        public List<string> listaError = new List<string>();//Almacena todo de ERROR

        public List<AllData> SimbolosTerminales = new List<AllData>();//Almacena los simbolos terminales
        //Agregar
        public struct AllData
        {
            public AllData(int intValue, string strValue)
            {
                IntegerData = intValue;
                StringData = strValue;
            }   
            public int IntegerData { get; private set; }
            public string StringData { get; private set; }
        }
    }
}
