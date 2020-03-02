﻿using System.Collections.Generic;
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
        public Dictionary<string, string> diccionarioColeccion = new Dictionary<string, string>();//Para que el usuario pueda modificar luego
        public Stack<ArbolB> PilaS = new Stack<ArbolB>();//Arbol de expresiones
        public Stack<string> PilaT = new Stack<string>();//Se usa para guardar los TOKENS

        public List<string> eSET = new List<string>();//Guardando la expresion regular para comprobar en las pilas
        public List<string> eTOKEN = new List<string>();//Guardando la expresion regular para comprobar en las pilas
        public List<string> eACTION = new List<string>();//Guardando la expresion regular para comprobar en las pilas
        public List<string> eERROR = new List<string>();//Guardando la expresion regular para comprobar en las pilas

        public List<string> listaSets = new List<string>();//Almacena todo de SETS
        public List<string> listaToken = new List<string>();//Almacena todo de TOKENS
        public List<string> listaAction = new List<string>();//Almacena todo de ACTIONS/RESERVADAS()
        public List<string> listaError = new List<string>();//Almacena todo de ERROR

        public List<string[]> Metacaracteres = new List<string[]>();
        public List<string[]> Unarios = new List<string[]>();
        public List<string[]> OperadoresER = new List<string[]>();
        //Agregar
    }
}
