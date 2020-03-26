﻿using System;
using System.Windows.Forms;
using LFA_Proyecto.Help;
using LFA_Proyecto.Arbol;
using System.Collections.Generic;

namespace LFA_Proyecto.Modelos//Creador: Ing. Moises Alonso
{
    //  Entradas
    //1.	Tokens de la expresión regular(Símbolos terminales “st”, meta caracteres operadores incluyendo la concatenación “op”)
    //2.	Pila de Tokens llamada “T”
    //3.	Pila de árboles llamada “S”
    //  Salidas
    //1.	Árbol de expresión con el símbolo terminal extendido #

    class ER_ET
    {
        private int GetImport(string eFormato)
        {
            if (eFormato == "(" || eFormato == ")")
            {
                return 4;
            }
            if (eFormato == "*" || eFormato == "?" || eFormato == "+")
            {
                return 3;
            }
            if (eFormato == ".")
            {
                return 2;
            }
            if (eFormato == "|")
            {
                return 1;
            }
            return 0;
        }
        public ArbolB CrearArbol(List<string> listaToken)
        {
            foreach (var item in listaToken)
            {
                if (item == "(")
                {
                    Datos.Instance.PilaT.Push(item);
                }
                else if (item == ")")
                {
                    while (Datos.Instance.PilaT.Count > 0 && Datos.Instance.PilaT.Peek() != "(")
                    {
                        if (Datos.Instance.PilaT.Count == 0)
                        {
                            Console.WriteLine("Error, faltan operandos");
                            break;
                        }
                        if (Datos.Instance.PilaS.Count < 2)
                        {
                            Console.WriteLine("Error, faltan operandos");
                            break;
                        }
                        var nodoTemp = new ArbolB();
                        nodoTemp.Valores = Datos.Instance.PilaT.Pop();
                        nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        Datos.Instance.PilaS.Push(nodoTemp);
                    }
                    Datos.Instance.PilaT.Pop();
                }
                else if (item == "." || item == "*" || item == "?" || item == "+" || item == "|")
                {
                    if (item == "*" || item == "?" || item == "+")
                    {
                        var nodoTemp = new ArbolB();
                        nodoTemp.Valores = item;
                        if (Datos.Instance.PilaS.Count < 0)
                        {
                            Console.WriteLine("Error, faltan operandos");
                            break;
                        }
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        Datos.Instance.PilaS.Push(nodoTemp);
                    }
                    else if (Datos.Instance.PilaT.Count != 0 && Datos.Instance.PilaT.Peek() != "(")
                    {
                        var eTok = GetImport(item);
                        var ePila = GetImport(Datos.Instance.PilaT.Peek());
                        if (eTok <= ePila)
                        {
                            var nodoTemp = new ArbolB();
                            nodoTemp.Valores = item;
                            if (Datos.Instance.PilaS.Count < 2)
                            {
                                Console.WriteLine("Error, faltan operandos");
                                break;
                            }
                            nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                            nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                            Datos.Instance.PilaS.Push(nodoTemp);
                        }
                    }
                    else if (item == "." || item == "|")
                    {
                        Datos.Instance.PilaT.Push(item);
                    }
                }
                else
                {
                    var nodoTemp = new ArbolB();
                    nodoTemp.Valores = item;
                    Datos.Instance.PilaS.Push(nodoTemp);
                }
            }
            while (Datos.Instance.PilaT.Count > 0)
            {
                var nodoTemp = new ArbolB();
                nodoTemp.Valores = Datos.Instance.PilaT.Pop();
                if (nodoTemp.Valores == "(")
                {
                    Console.WriteLine("Error, faltan operandos");
                    return null;
                }
                if (Datos.Instance.PilaS.Count < 2)
                {
                    Console.WriteLine("Error, faltan operandos");
                    return null;
                }
                nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                Datos.Instance.PilaS.Push(nodoTemp);
            }
            if (Datos.Instance.PilaS.Count != 1)
            {
                Console.WriteLine("Error, faltan operandos");
                return null;
            }
            MessageBox.Show("PilaS");
            return Datos.Instance.PilaS.Pop();
        }
    }
}
    

