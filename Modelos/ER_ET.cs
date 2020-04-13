using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using LFA_Proyecto.Help;
using LFA_Proyecto.Arbol;

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
        private int lastPrecedence = int.MaxValue;
        private void GetLastPrecedence(string token)
        {
            if (token == "(" || token == ")")
            {
                lastPrecedence = 4;
            }
            else if (token == "*" || token == "+" || token == "?")
            {
                lastPrecedence = 3;
            }
            else if (token == ".")
            {
                lastPrecedence = 2;
            }
            else if (token == "|")
            {
                lastPrecedence = 1;
            }
        }
        /// <summary>
        /// <para>Algoritmo creado por Moises Alonso</para>
        /// Algoritmo de creación de árbol de expresion a traves de Expresión Regular
        /// </summary>
        /// <param name="RE"></param>
        public void PruebaArbol(List<Datos.AllData> RE)
        {
            int lstPrcdnc = 0;
            foreach (var item in RE)
            {
                if (item.StringData == "(")
                {
                    Datos.Instance.PilaT.Push(item.StringData);
                    GetLastPrecedence(item.StringData);
                }
                else if (item.StringData == ")")
                {
                    while (Datos.Instance.PilaT.Count > 0 && Datos.Instance.PilaT.Peek() != "(")
                    {
                        if (Datos.Instance.PilaT.Count == 0)
                        {
                            MessageBox.Show("Faltan operadores");
                            return;
                        }
                        if (Datos.Instance.PilaS.Count < 2)
                        {
                            MessageBox.Show("Faltan operadores");
                            return;
                        }
                        var nodoTemp = new ArbolB();
                        nodoTemp.Dato = Datos.Instance.PilaT.Pop();
                        nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        Datos.Instance.PilaS.Push(nodoTemp);
                    }
                    if (Datos.Instance.PilaT.Count == 0)
                    {
                        MessageBox.Show("Faltan operadores");
                        return;
                    }
                    Datos.Instance.PilaT.Pop();
                    GetLastPrecedence(")");
                }
                else if (Utilities.Op.Contains(item.StringData))
                {
                    if (item.StringData == "?" || item.StringData == "*" || item.StringData == "+")
                    {
                        var nodoTemp = new ArbolB();
                        nodoTemp.Dato = item.StringData;
                        if (Datos.Instance.PilaS.Count == 0)
                        {
                            MessageBox.Show("Error, faltan operandos");
                            break;
                        }
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        Datos.Instance.PilaS.Push(nodoTemp);
                        GetLastPrecedence(item.StringData);
                    }
                    else if (Datos.Instance.PilaT.Count > 0 && Datos.Instance.PilaT.Peek() != "(")
                    {
                        lstPrcdnc = this.lastPrecedence;
                        GetLastPrecedence(item.StringData);
                        if (this.lastPrecedence < lstPrcdnc)
                        {
                            var nodoTemp = new ArbolB();
                            nodoTemp.Dato = Datos.Instance.PilaT.Pop();
                            if (Datos.Instance.PilaS.Count() < 2)
                            {
                                MessageBox.Show("Faltan operadores");
                                return;
                            }
                            nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                            nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                            Datos.Instance.PilaS.Push(nodoTemp);
                        }
                    }
                    if (item.StringData == "|" || item.StringData == ".")
                    {
                        Datos.Instance.PilaT.Push(item.StringData);
                        GetLastPrecedence(item.StringData);
                    }
                }
                else
                {
                    var nodoTemp = new ArbolB();
                    nodoTemp.Dato = item.StringData;
                    Datos.Instance.PilaS.Push(nodoTemp);
                }
            }
            while (Datos.Instance.PilaT.Count > 0)
            {
                var nodoTemp = new ArbolB();
                nodoTemp.Dato = Datos.Instance.PilaT.Pop();
                if (nodoTemp.Dato == "(" || Datos.Instance.PilaS.Count < 2)
                {
                    MessageBox.Show("Faltan operadores");
                    return;
                }
                nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                Datos.Instance.PilaS.Push(nodoTemp);
            }
            if (Datos.Instance.PilaS.Count != 1)
            {
                MessageBox.Show("Faltan operadores");
                return;
            }
        }
    }
}



