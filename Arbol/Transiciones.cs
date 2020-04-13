﻿using LFA_Proyecto.Help;
using LFA_Proyecto.Modelos;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LFA_Proyecto.Arbol
{
    class Transiciones
    {
        private int Value = 1;
        public void GenerarTransiciones()
        {
            try
            {
                var miArbol = new ArbolB();
                miArbol = Datos.Instance.PilaS.Pop();
                GetValue(miArbol);
                GetNuller(miArbol);
                GetFirst(miArbol);
                GetLast(miArbol);
                GetFollow(miArbol);
                Datos.Instance.PilaS.Push(miArbol);
            }
            catch (System.Exception)
            {
                MessageBox.Show("No se ha cargado ningun dato");//Solo confirmación visual
                return;
            }
        }
        private void GetNuller(ArbolB ArbolER)
        {
            if (ArbolER != null)
            {
                GetNuller(ArbolER.HijoIzquierdo);
                GetNuller(ArbolER.HijoDerecho);
                if (ArbolER.HijoDerecho == null && ArbolER.HijoIzquierdo == null)
                {
                    ArbolER.Nuller = false;
                }
                if (ArbolER.Dato == "|")
                {
                    if (ArbolER.HijoIzquierdo.Nuller == true || ArbolER.HijoDerecho.Nuller == true)
                    {
                        ArbolER.Nuller = true;
                    }
                }
                if (ArbolER.Dato == ".")
                {
                    if (ArbolER.HijoIzquierdo.Nuller == true && ArbolER.HijoDerecho.Nuller == true)
                    {
                        ArbolER.Nuller = true;
                    }
                }
                if (ArbolER.Dato == "*" || ArbolER.Dato == "+" || ArbolER.Dato == "?")
                {
                    ArbolER.Nuller = true;
                }
            }
        }
        private void GetFollow(ArbolB ArbolER)
        {
            if (ArbolER != null)
            {
                GetFollow(ArbolER.HijoIzquierdo);
                GetFollow(ArbolER.HijoDerecho);
                if (ArbolER.Dato == "." || ArbolER.Dato == "*" || ArbolER.Dato == "+")
                {
                    if (ArbolER.Dato == ".")
                    {
                        foreach (var item in ArbolER.HijoIzquierdo.Last)
                        {
                            ArbolER.Follow = ArbolER.HijoDerecho.First + ",";
                        }
                    }
                    else
                    {
                        foreach (var item in ArbolER.HijoIzquierdo.Last)
                        {
                            ArbolER.Follow = ArbolER.HijoIzquierdo.First + ",";
                        }
                    }
                }
            }
        }
        private void GetLast(ArbolB ArbolER)
        {
            if (ArbolER != null)
            {
                GetLast(ArbolER.HijoIzquierdo);
                GetLast(ArbolER.HijoDerecho);
                if (ArbolER.Dato == "|")
                {
                    ArbolER.Last = ArbolER.HijoIzquierdo.Last + "," + ArbolER.HijoDerecho.Last;
                }
                if (ArbolER.Dato == ".")
                {
                    if (ArbolER.HijoDerecho.Nuller == true)
                    {
                        ArbolER.Last = ArbolER.HijoIzquierdo.Last + "," + ArbolER.HijoDerecho.Last;
                    }
                    else
                    {
                        ArbolER.Last = ArbolER.HijoDerecho.Last;
                    }
                }
                if (ArbolER.Dato == "*" || ArbolER.Dato == "+" || ArbolER.Dato == "?")
                {
                    ArbolER.Last = ArbolER.HijoIzquierdo.Last;
                }
            }
        }
        private void GetFirst(ArbolB ArbolER)
        {
            if (ArbolER != null)
            {
                GetFirst(ArbolER.HijoIzquierdo);
                GetFirst(ArbolER.HijoDerecho);
                if (ArbolER.Dato == "|")
                {
                    ArbolER.First = ArbolER.HijoIzquierdo.First + "," + ArbolER.HijoDerecho.First;
                }
                if (ArbolER.Dato == ".")
                {
                    if (ArbolER.HijoIzquierdo.Nuller == true)
                    {
                        ArbolER.First = ArbolER.HijoIzquierdo.First + "," + ArbolER.HijoDerecho.First;
                    }
                    else
                    {
                        ArbolER.First = ArbolER.HijoIzquierdo.First;
                    }
                }
                if (ArbolER.Dato == "*" || ArbolER.Dato == "+" || ArbolER.Dato == "?")
                {
                    ArbolER.First = ArbolER.HijoIzquierdo.First;
                }
            }
        }
        private void GetValue(ArbolB ArbolER)
        {
            if (ArbolER != null)
            {
                GetValue(ArbolER.HijoIzquierdo);
                GetValue(ArbolER.HijoDerecho);
                if (Utilities.Nullers.Contains(ArbolER.Dato))
                {
                    ArbolER.Nuller = true;
                }
                if (!(Utilities.Op.Contains(ArbolER.Dato)))
                {
                    ArbolER.Value = Value;//Asignar
                    ArbolER.First = Value.ToString();
                    ArbolER.Last = Value.ToString();
                    Value++;
                }
            }
        }
    }
}
