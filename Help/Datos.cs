using LFA_Proyecto.Arbol;
using System.Collections.Generic;

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
        /// <summary>
        /// Utilizado para generar el Arbol de Expresion
        /// </summary>
        public Stack<ArbolB> PilaS = new Stack<ArbolB>();
        /// <summary>
        /// Utilizado para generar el Arbol de Expresion
        /// </summary>
        public Stack<string> PilaT = new Stack<string>();

        /// <summary>
        /// Almacena SETS para ser utilizados en comprobacion
        /// </summary>
        public List<string> listaSets = new List<string>();
        /// <summary>
        /// Almacena TOKENS para ser utilizados en la Expresion Regular 
        /// para posteriormente en la creacion del arbol
        /// </summary>
        public List<string> listaToken = new List<string>();
        public List<string> listaAction = new List<string>();
        public List<string> listaError = new List<string>();

        /// <summary>
        /// Almacena las tablas de transicion para ser imprimidas luego
        /// </summary>
        public List<AllData> SimbolosTerminales = new List<AllData>();
        /// <summary>
        /// Guarda los elementos de las Actions
        /// </summary>
        public List<AllActions> SimbolosAction = new List<AllActions>();
        /// <summary>
        /// Almacena las tablas de transicion para ser imprimidas luego
        /// </summary>
        public Dictionary<int, List<int>> DiccTrans = new Dictionary<int, List<int>>();
        /// <summary>
        /// Almacena los valores Follow para ser impresos
        /// </summary>
        public Dictionary<int, List<int>> DicFollow = new Dictionary<int, List<int>>();
        /// <summary>
        /// Guarda los elementos sin repetir
        /// </summary>
        public List<string> SimbolosTotales = new List<string>();
        /// <summary>
        /// Guarda las reservadas
        /// </summary>
        public List<string> misReservas = new List<string>();

        /// <summary>
        /// Diccionario donde se almacenan las transiciones
        /// </summary>
        public Dictionary<List<int>, Dictionary<string, List<int>>> DiccionarioTransiciones = new Dictionary<List<int>, Dictionary<string, List<int>>>();
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
        public struct AllActions
        {
            public AllActions(int intValue, string typeAct, string strValue)
            {
                IntegerData = intValue;
                typeAction = typeAct;
                StringData = strValue;
            }
            public int IntegerData { get; set; }
            public string typeAction { get; private set; }
            public string StringData { get; private set; }
        }
    }
}
