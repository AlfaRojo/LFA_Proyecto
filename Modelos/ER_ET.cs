using LFA_Proyecto.Arbol;
using LFA_Proyecto.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LFA_Proyecto.Modelos//Creador: Ing. Moises Alonso
{
    /// <summary>
    ///   <para>Entradas:</para>
    /// <para>Tokens de la expresión regular</para>
    /// <para>Pila de Tokens</para>
    /// <para>Pila de árboles</para>
    /// <para>Salidas:</para>
    ///	<returns>Árbol de expresión</returns>
    /// </summary>
    class ER_ET
    {
        Dictionary<string, int> Precedencia = new Dictionary<string, int> { { "+", 3 }, { "?", 3 }, { "*", 3 }, { ".", 2 }, { "|", 1 } };
        /// <summary>
        /// <para>Algoritmo creado por Moises Alonso</para>
        /// Algoritmo de creación de árbol de expresion a traves de Expresión Regular
        /// </summary>
        /// <param name="RE"></param>
        public void Tree(List<Datos.AllData> RE)
        {
            foreach (var item in RE)
            {
                if (item.StringData == "(")
                {
                    Datos.Instance.PilaT.Push(item.StringData);
                }
                else if (item.StringData == ")")
                {
                    while (Datos.Instance.PilaT.Count > 0 && Datos.Instance.PilaT.Peek() != "(")
                    {
                        if (Datos.Instance.PilaT.Count == 0)
                        {
                            throw new Exception("Faltan operadores");
                        }
                        if (Datos.Instance.PilaS.Count < 2)
                        {
                            throw new Exception("Faltan operadores");
                        }
                        var nodoTemp = new ArbolB();
                        nodoTemp.Dato = Datos.Instance.PilaT.Pop();
                        nodoTemp.Padre = null;
                        nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                        nodoTemp.HijoDerecho.Padre = nodoTemp.Dato;
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        nodoTemp.HijoIzquierdo.Padre = nodoTemp.Dato;
                        Datos.Instance.PilaS.Push(nodoTemp);
                    }
                    Datos.Instance.PilaT.Pop();
                }
                else if (Utilities.Op.Contains(item.StringData))
                {
                    if (item.StringData == "?" || item.StringData == "*" || item.StringData == "+")
                    {
                        var nodoTemp = new ArbolB();
                        nodoTemp.Dato = item.StringData;
                        nodoTemp.Padre = null;
                        if (Datos.Instance.PilaS.Count == 0)
                        {
                            throw new Exception("Error, faltan operandos");
                        }
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        nodoTemp.HijoIzquierdo.Padre = nodoTemp.Dato;
                        Datos.Instance.PilaS.Push(nodoTemp);
                    }
                    else if (Datos.Instance.PilaT.Count != 0)
                    {
                        while (Datos.Instance.PilaT.Peek() != "(" && (VerificarPrecedencia(item.StringData) == true))
                        {
                            var nodoTemp = new ArbolB();
                            nodoTemp.Dato = Datos.Instance.PilaT.Pop();
                            nodoTemp.Padre = null;
                            if (Datos.Instance.PilaS.Count() < 2)
                            {
                                throw new Exception("Faltan operadores");
                            }
                            else
                            {
                                var precedencia = VerificarPrecedencia(item.StringData);
                                nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                                nodoTemp.HijoDerecho.Padre = nodoTemp.Dato;
                                nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                                nodoTemp.HijoIzquierdo.Padre = nodoTemp.Dato;
                                Datos.Instance.PilaS.Push(nodoTemp);
                            }
                        }
                    }
                    if (item.StringData == "|" || item.StringData == ".")
                    {
                        Datos.Instance.PilaT.Push(item.StringData);
                    }
                }
                else
                {
                    var nodoTemp = new ArbolB();
                    nodoTemp.Dato = item.StringData;
                    nodoTemp.Padre = null;
                    Datos.Instance.PilaS.Push(nodoTemp);
                }
            }
            while (Datos.Instance.PilaT.Count > 0)
            {
                if (Datos.Instance.PilaT.Peek() == "(" || Datos.Instance.PilaS.Count < 2)
                {
                    throw new Exception("Faltan operadores");
                }
                var nodoTemp = new ArbolB();
                nodoTemp.Dato = Datos.Instance.PilaT.Pop();
                nodoTemp.Padre = null;
                nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                nodoTemp.HijoDerecho.Padre = nodoTemp.Dato;
                nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                nodoTemp.HijoIzquierdo.Padre = nodoTemp.Dato;
                Datos.Instance.PilaS.Push(nodoTemp);
            }
            if (Datos.Instance.PilaS.Count != 1)
            {
                throw new Exception("Faltan operadores");
            }
        }
        public bool VerificarPrecedencia(string TokenEvaluar)
        {
            Precedencia.TryGetValue(TokenEvaluar, out int TokenEvaluarValor);
            Precedencia.TryGetValue(Datos.Instance.PilaT.Peek(), out int TokenCompararValor);
            if (TokenEvaluarValor <= TokenCompararValor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}



