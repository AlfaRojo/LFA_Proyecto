using LFA_Proyecto.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFA_Proyecto.Arbol
{
    class Estados
    {
        private Dictionary<List<int>, Dictionary<string, List<int>>> TablaEstados = new Dictionary<List<int>, Dictionary<string, List<int>>>();
        private Dictionary<int, List<int>> Follows;
        private List<ArbolB> listaST;
        private ArbolB miArbol;
        private List<Datos.AllData> simbolosTerminales;

        public Estados(Dictionary<int, List<int>> follows, List<ArbolB> listaST, ArbolB miArbol, List<Datos.AllData> simbolosTerminales)
        {
            Follows = follows;
            this.listaST = listaST;
            this.miArbol = miArbol;
            this.simbolosTerminales = simbolosTerminales;
        }
        public Dictionary<List<int>, Dictionary<string, List<int>>> CrearEstados(ArbolB Arbol)
        {
            Queue<List<int>> EstadosAprobar = new Queue<List<int>>();
            List<List<int>> EstadosHistorial = new List<List<int>>();
            var diccModificar = new Dictionary<string, List<int>>();
            var EstadoInicial = Arbol.First;
            EstadosHistorial.Add(EstadoInicial);
            foreach (var Simbolo in listaST)
            {
                var ListaConcordancia = new List<int>();
                var ListaFollows = new List<int>();
                foreach (var item in EstadoInicial)
                {
                    var SimbolosArbol = simbolosTerminales[item - 1];
                    if (Simbolo.Dato == SimbolosArbol.StringData)
                    {
                        ListaConcordancia.Add(item);
                    }
                }
                foreach (var SimblosItem in ListaConcordancia)
                {
                    Follows.TryGetValue(SimblosItem, out var follows);
                    ListaFollows.AddRange(follows);
                }
                diccModificar.Add(Simbolo.Dato, ListaFollows);
                if (!EstadosAprobar.Any(c => c.SequenceEqual(ListaFollows)) && !EstadosHistorial.Any(c => c.SequenceEqual(ListaFollows)) && ListaFollows.Count != 0)
                {
                    EstadosAprobar.Enqueue(ListaFollows);
                }
            }
            TablaEstados.Add(EstadoInicial, diccModificar);
            while (EstadosAprobar.Count() != 0)
            {
                var diccModificarNoInicial = new Dictionary<string, List<int>>();
                var Estado = EstadosAprobar.Dequeue();
                EstadosHistorial.Add(Estado);
                foreach (var Simbolo in listaST)
                {
                    var ListaConcordancia = new List<int>();
                    var ListaFollows = new List<int>();
                    foreach (var item in Estado)
                    {
                        var SimbolosArbol = simbolosTerminales[item - 1];
                        if (Simbolo.Dato == SimbolosArbol.StringData)
                        {
                            ListaConcordancia.Add(item);
                        }
                    }
                    foreach (var SimblosItem in ListaConcordancia)
                    {
                        Follows.TryGetValue(SimblosItem, out var follows);
                        ListaFollows.AddRange(follows);
                    }
                    diccModificarNoInicial.Add(Simbolo.Dato, ListaFollows);
                    if (!EstadosAprobar.Any(c => c.SequenceEqual(ListaFollows)) && !EstadosHistorial.Any(c => c.SequenceEqual(ListaFollows)) && ListaFollows.Count != 0)
                    {
                        EstadosAprobar.Enqueue(ListaFollows);
                    }
                }
                TablaEstados.Add(Estado, diccModificarNoInicial);
            }
            return TablaEstados;
        }

    }
}
