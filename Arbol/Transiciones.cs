﻿using LFA_Proyecto.Help;
using LFA_Proyecto.Modelos;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LFA_Proyecto.Arbol
{
    class Transiciones
    {
        public static int enumeracion = 0;
        public static Dictionary<int, List<int>> dictionaryFollows = new Dictionary<int, List<int>>();
        public static Dictionary<int, List<int>> Auxiliar = new Dictionary<int, List<int>>();
        public static List<ArbolB> ListaST = new List<ArbolB>();
        public void GenerarTransiciones()
        {
            if (Datos.Instance.PilaS.Count != 1)
            {
                return;
            }
            var miArbol = new ArbolB();
            miArbol = Datos.Instance.PilaS.Pop();
            EnumerarHojas(miArbol);
            RegresarHojas(miArbol);
            IdentificarNulos(miArbol);
            IdentificarFirst(miArbol);
            IdentificarLast(miArbol);
            IdentificarFollows(miArbol);
            CrearTrans(miArbol);
            Datos.Instance.PilaS.Push(miArbol);
            Datos.Instance.DicFollow = dictionaryFollows;
            Datos.Instance.DiccTrans = Auxiliar;
        }
        #region Transiciones
        public static int EnumerarHojas(ArbolB tree)
        {
            if (tree != null)
            {
                EnumerarHojas(tree.HijoIzquierdo);
                EnumerarHojas(tree.HijoDerecho);
                if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
                {
                    enumeracion++;
                    tree.Value = enumeracion;
                }
            }
            return enumeracion;
        }
        public static void RegresarHojas(ArbolB tree)
        {
            if (tree != null)
            {
                RegresarHojas(tree.HijoIzquierdo);
                RegresarHojas(tree.HijoDerecho);
                if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
                {
                    ListaST.Add(tree);
                }
            }
        }
        public static void IdentificarNulos(ArbolB tree)
        {
            if (tree != null)
            {
                IdentificarNulos(tree.HijoIzquierdo);
                IdentificarNulos(tree.HijoDerecho);
                if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
                {
                    tree.Nuller = false;
                }
                if (tree.Dato == "*" || tree.Dato == "?")
                {
                    tree.Nuller = true;
                }
                else if (tree.Dato == "|" || tree.Dato == "." || tree.Dato == "+")
                {
                    if (tree.Dato == "|")
                    {
                        if (tree.HijoIzquierdo.Nuller == true || tree.HijoDerecho.Nuller == true)
                        {
                            tree.Nuller = true;
                        }
                        else
                        {
                            tree.Nuller = false;
                        }
                    }
                    else if (tree.Dato == ".")
                    {
                        if (tree.HijoIzquierdo.Nuller == true && tree.HijoDerecho.Nuller == true)
                        {
                            tree.Nuller = true;
                        }
                        else
                        {
                            tree.Nuller = false;
                        }
                    }
                    else if (tree.Dato == "+")
                    {
                        tree.Nuller = false;
                    }
                }
            }
        }
        public static void IdentificarFirst(ArbolB tree)
        {
            if (tree != null)
            {
                IdentificarFirst(tree.HijoIzquierdo);
                IdentificarFirst(tree.HijoDerecho);
                if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
                {
                    tree.First.Add(tree.Value);
                }
                if (tree.Dato == ".")
                {
                    if (tree.HijoIzquierdo.Nuller == true)
                    {
                        tree.First.AddRange(tree.HijoIzquierdo.First);
                        tree.First.AddRange(tree.HijoDerecho.First);
                    }
                    else
                    {
                        tree.First.AddRange(tree.HijoIzquierdo.First);
                    }
                }
                else if (tree.Dato == "|")
                {
                    tree.First.AddRange(tree.HijoIzquierdo.First);
                    tree.First.AddRange(tree.HijoDerecho.First);
                }
                else if (tree.Dato == "*" || tree.Dato == "+" || tree.Dato == "?")
                {
                    tree.First.AddRange(tree.HijoIzquierdo.First);
                }
            }
        }
        public static void IdentificarLast(ArbolB tree)
        {
            if (tree != null)
            {
                IdentificarLast(tree.HijoIzquierdo);
                IdentificarLast(tree.HijoDerecho);
                if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
                {
                    tree.Last.Add(tree.Value);
                }
                if (tree.Dato == ".")
                {
                    if (tree.HijoDerecho.Nuller == true)
                    {
                        tree.Last.AddRange(tree.HijoIzquierdo.Last);
                        tree.Last.AddRange(tree.HijoDerecho.Last);
                    }
                    else
                    {
                        tree.Last.AddRange(tree.HijoDerecho.Last);
                    }
                }
                else if (tree.Dato == "|")
                {
                    tree.Last.AddRange(tree.HijoIzquierdo.Last);
                    tree.Last.AddRange(tree.HijoDerecho.Last);
                }
                else if (tree.Dato == "*" || tree.Dato == "+" || tree.Dato == "?")
                {
                    tree.Last.AddRange(tree.HijoIzquierdo.Last);
                }
            }
        }
        public static Dictionary<int, List<int>> IdentificarFollows(ArbolB tree)
        {
            if (tree != null)
            {
                IdentificarFollows(tree.HijoIzquierdo);
                IdentificarFollows(tree.HijoDerecho);
                if (tree.Dato == "." || tree.Dato == "*" || tree.Dato == "+")
                {
                    if (tree.Dato == ".")
                    {
                        foreach (var item in tree.HijoIzquierdo.Last)
                        {
                            if (dictionaryFollows.ContainsKey(item))
                            {
                                var listaParcial = dictionaryFollows[item];
                                listaParcial.AddRange(tree.HijoDerecho.First);
                                List<int> uniqueList = listaParcial.Distinct().ToList();
                                dictionaryFollows[item] = uniqueList;
                            }
                            else
                            {
                                dictionaryFollows.Add(item, tree.HijoDerecho.First);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in tree.HijoIzquierdo.Last)
                        {
                            if (dictionaryFollows.ContainsKey(item))
                            {
                                var listaParcial = dictionaryFollows[item];
                                listaParcial.AddRange(tree.HijoIzquierdo.First);
                                List<int> uniqueList = listaParcial.Distinct().ToList();
                                dictionaryFollows[item] = uniqueList;
                            }
                            else
                            {
                                dictionaryFollows.Add(item, tree.HijoIzquierdo.First);
                            }
                        }
                    }
                }
            }
            return dictionaryFollows;
        }
        public void CrearTrans(ArbolB tree)//Dictionary<int, List<int>> dictionarioFollows,
        {
            var firstRaiz = tree.First;
            RegresarHojas(tree);
            var diccionarioTrans = new Dictionary<int, List<int>>();
            diccionarioTrans = Auxiliar;
            //var uniqueValues = new Dictionary<int, List<int>>();
            var transicionesActuales = firstRaiz;
            if (diccionarioTrans.Count == 0)
            {
                diccionarioTrans.Add(tree.Value, tree.First);
            }
            foreach (var item in transicionesActuales)
            {
                var listaParcial = new List<int>();
                foreach (var Nodo in ListaST)
                {
                    if (Nodo.Value == item)
                    {
                        if (dictionaryFollows.ContainsKey(item))
                        {
                            var follower = dictionaryFollows[item];
                            if (listaParcial.Count > 0)//Eliminar elementos repetido de la listsa
                            {
                                for (int i = 0; i < follower.Count; i++)
                                {
                                    for (int j = 0; j < listaParcial.Count; j++)
                                    {
                                        if (listaParcial.ElementAt(j) == follower[i])
                                        {
                                            follower.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            listaParcial.AddRange(follower);//Agregar elementos *NO* repetidos
                        }
                    }
                }
                diccionarioTrans.Add(item, listaParcial);
            }
            //uniqueValues = diccionarioTrans.GroupBy(pair => pair.Value)
            //                         .Select(group => group.First())
            //                         .ToDictionary(pair => pair.Key, pair => pair.Value);//Obtiene elementos no repetidos en diccionario
            Auxiliar = diccionarioTrans;
        }
        #endregion
    }
}
