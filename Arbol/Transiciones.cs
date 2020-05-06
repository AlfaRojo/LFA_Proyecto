using LFA_Proyecto.Help;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LFA_Proyecto.Arbol
{
    class Transiciones
    {
        public static int enumeracion = 0;
        public static int numeroHojaNumeral = 0;
        public static Dictionary<int, List<int>> Follows = new Dictionary<int, List<int>>();
        public static Dictionary<int, List<int>> DiccionarioTransiciones = new Dictionary<int, List<int>>();
        //public static List<ArbolB> ListaST = new List<ArbolB>();
        public static List<string> EstadosAux = new List<string>();
        public static int contadorListaEstados = 0;

        #region New
        public List<ArbolB> RecorridoFirstLast = new List<ArbolB>();
        private List<string> TerminalesArbol = new List<string>();
        int ContadorTerminales = 1;
        public Dictionary<int, List<int>> myFollows = new Dictionary<int, List<int>>();
        #endregion

        public void GenerarTransiciones()
        {
            if (Datos.Instance.PilaS.Count != 1)
            {
                return;
            }
            var miArbol = new ArbolB();
            miArbol = Datos.Instance.PilaS.Pop();
            //EnumerarHojas(miArbol);
            //IdentificarNulos(miArbol);
            //IdentificarFirst(miArbol);
            //IdentificarLast(miArbol);
            //RegresarHojas(miArbol);
            //IdentificarEstados(miArbol);
            RecorridoPostorden(miArbol);
            Estados ObtenerEstados = new Estados(myFollows, miArbol, Datos.Instance.SimbolosTerminales);
            Datos.Instance.DiccionarioTransiciones = ObtenerEstados.CrearEstados(miArbol);
            Datos.Instance.DicFollow = myFollows;
            Datos.Instance.PilaS.Push(miArbol);
        }
        #region Old Obtener First-Last-Follow
        //public static int EnumerarHojas(ArbolB tree)
        //{
        //    if (tree != null)
        //    {
        //        EnumerarHojas(tree.HijoIzquierdo);
        //        EnumerarHojas(tree.HijoDerecho);
        //        if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
        //        {
        //            enumeracion++;
        //            tree.Value = enumeracion;
        //        }
        //    }
        //    return enumeracion;
        //}
        //public static void RegresarHojas(ArbolB tree)
        //{
        //    if (tree != null)
        //    {
        //        RegresarHojas(tree.HijoIzquierdo);
        //        RegresarHojas(tree.HijoDerecho);
        //        if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
        //        {
        //            ListaST.Add(tree);
        //        }
        //    }
        //}
        //public static void IdentificarNulos(ArbolB tree)
        //{
        //    if (tree != null)
        //    {
        //        IdentificarNulos(tree.HijoIzquierdo);
        //        IdentificarNulos(tree.HijoDerecho);
        //        if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
        //        {
        //            tree.Nuller = false;
        //        }
        //        if (tree.Dato == "*" || tree.Dato == "?")
        //        {
        //            tree.Nuller = true;
        //        }
        //        else if (tree.Dato == "|" || tree.Dato == "." || tree.Dato == "+")
        //        {
        //            if (tree.Dato == "|")
        //            {
        //                if (tree.HijoIzquierdo.Nuller == true || tree.HijoDerecho.Nuller == true)
        //                {
        //                    tree.Nuller = true;
        //                }
        //                else
        //                {
        //                    tree.Nuller = false;
        //                }
        //            }
        //            else if (tree.Dato == ".")
        //            {
        //                if (tree.HijoIzquierdo.Nuller == true && tree.HijoDerecho.Nuller == true)
        //                {
        //                    tree.Nuller = true;
        //                }
        //                else
        //                {
        //                    tree.Nuller = false;
        //                }
        //            }
        //            else if (tree.Dato == "+")
        //            {
        //                tree.Nuller = false;
        //            }
        //        }
        //    }
        //}
        //public static void IdentificarFirst(ArbolB tree)
        //{
        //    if (tree != null)
        //    {
        //        IdentificarFirst(tree.HijoIzquierdo);
        //        IdentificarFirst(tree.HijoDerecho);
        //        if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
        //        {
        //            tree.First.Add(tree.Value);
        //        }
        //        if (tree.Dato == ".")
        //        {
        //            if (tree.HijoIzquierdo.Nuller == true)
        //            {
        //                tree.First.AddRange(tree.HijoIzquierdo.First);
        //                tree.First.AddRange(tree.HijoDerecho.First);
        //            }
        //            else
        //            {
        //                tree.First.AddRange(tree.HijoIzquierdo.First);
        //            }
        //        }
        //        else if (tree.Dato == "|")
        //        {
        //            tree.First.AddRange(tree.HijoIzquierdo.First);
        //            tree.First.AddRange(tree.HijoDerecho.First);
        //        }
        //        else if (tree.Dato == "*" || tree.Dato == "+" || tree.Dato == "?")
        //        {
        //            tree.First.AddRange(tree.HijoIzquierdo.First);
        //        }
        //    }
        //}
        //public static void IdentificarLast(ArbolB tree)
        //{
        //    if (tree != null)
        //    {
        //        IdentificarLast(tree.HijoIzquierdo);
        //        IdentificarLast(tree.HijoDerecho);
        //        if (tree.HijoIzquierdo == null && tree.HijoDerecho == null)
        //        {
        //            tree.Last.Add(tree.Value);
        //        }
        //        if (tree.Dato == ".")
        //        {
        //            if (tree.HijoDerecho.Nuller == true)
        //            {
        //                tree.Last.AddRange(tree.HijoIzquierdo.Last);
        //                tree.Last.AddRange(tree.HijoDerecho.Last);
        //            }
        //            else
        //            {
        //                tree.Last.AddRange(tree.HijoDerecho.Last);
        //            }
        //        }
        //        else if (tree.Dato == "|")
        //        {
        //            tree.Last.AddRange(tree.HijoIzquierdo.Last);
        //            tree.Last.AddRange(tree.HijoDerecho.Last);
        //        }
        //        else if (tree.Dato == "*" || tree.Dato == "+" || tree.Dato == "?")
        //        {
        //            tree.Last.AddRange(tree.HijoIzquierdo.Last);
        //        }
        //    }
        //}
        #endregion

        #region Obtener First-Last-Nullers

        private void RecorridoPostorden(ArbolB raiz)
        {
            if (raiz != null)
            {
                RecorridoPostorden(raiz.HijoIzquierdo);
                RecorridoPostorden(raiz.HijoDerecho);
                RecorridoFirstLast.Add(raiz);
                if (raiz.Eshoja)
                {
                    raiz.Numero = ContadorTerminales;
                    raiz.First.Add(ContadorTerminales);
                    raiz.Last.Add(ContadorTerminales);
                    // inicializo el diccionario con los simbolos de los terminales y las listas las inicializo
                    myFollows.Add(ContadorTerminales, new List<int>());
                    TerminalesArbol.Add(raiz.Dato);
                    ContadorTerminales++;
                }
                else if (raiz.Dato == "*")
                {
                    raiz.Nuller = true;
                    raiz.First = raiz.HijoIzquierdo.First;
                    raiz.Last = raiz.HijoIzquierdo.Last;
                    foreach (var LastC1 in raiz.HijoIzquierdo.Last)
                    {
                        foreach (var firstC1 in raiz.HijoIzquierdo.First)
                        {
                            myFollows.TryGetValue(LastC1, out var followexistentes);

                            if (!followexistentes.Contains(firstC1))
                            {
                                myFollows.FirstOrDefault(x => x.Key == LastC1).Value.Add(firstC1);
                            }
                        }
                    }
                }
                else if (raiz.Dato == "+")
                {
                    raiz.First = raiz.HijoIzquierdo.First;
                    raiz.Last = raiz.HijoIzquierdo.Last;
                    foreach (var LastC1 in raiz.HijoIzquierdo.Last)
                    {
                        foreach (var firstC1 in raiz.HijoIzquierdo.First)
                        {
                            myFollows.TryGetValue(LastC1, out var followsexistentes);

                            if (!followsexistentes.Contains(firstC1))
                            {
                                myFollows.FirstOrDefault(x => x.Key == LastC1).Value.Add(firstC1);
                            }
                        }
                    }
                }
                else if (raiz.Dato == "?")
                {
                    raiz.Nuller = true;
                    raiz.First = raiz.HijoIzquierdo.First;
                    raiz.Last = raiz.HijoIzquierdo.Last;
                }
                else if (raiz.Dato == "|")
                {
                    if (raiz.HijoIzquierdo.Nuller == true || raiz.HijoDerecho.Nuller == true)
                    {
                        raiz.Nuller = true;
                    }
                    raiz.First.AddRange(raiz.HijoIzquierdo.First);
                    raiz.First.AddRange(raiz.HijoDerecho.First);
                    raiz.Last.AddRange(raiz.HijoIzquierdo.Last);
                    raiz.Last.AddRange(raiz.HijoDerecho.Last);
                }
                else if (raiz.Dato == ".")
                {
                    if (raiz.HijoIzquierdo.Nuller == true && raiz.HijoDerecho.Nuller == true)
                    {
                        raiz.Nuller = true;
                    }
                    if (raiz.HijoIzquierdo.Nuller == true)
                    {
                        raiz.First.AddRange(raiz.HijoIzquierdo.First);
                        raiz.First.AddRange(raiz.HijoDerecho.First);
                    }
                    else
                    {
                        raiz.First.AddRange(raiz.HijoIzquierdo.First);
                    }
                    if (raiz.HijoDerecho.Nuller == true)
                    {
                        raiz.Last.AddRange(raiz.HijoIzquierdo.Last);
                        raiz.Last.AddRange(raiz.HijoDerecho.Last);
                    }
                    else
                    {
                        raiz.Last.AddRange(raiz.HijoDerecho.Last);
                    }
                    foreach (var LastC1 in raiz.HijoIzquierdo.Last)
                    {
                        foreach (var firstC2 in raiz.HijoDerecho.First)
                        {
                            myFollows.TryGetValue(LastC1, out var valor);
                            // se valido que si el count es 0 no da error al verificar si lo contine
                            if (!valor.Contains(firstC2))
                            {
                                myFollows.FirstOrDefault(x => x.Key == LastC1).Value.Add(firstC2);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
