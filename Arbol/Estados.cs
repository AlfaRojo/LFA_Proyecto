using LFA_Proyecto.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFA_Proyecto.Modelos;

namespace LFA_Proyecto.Arbol
{
    class Estados
    {
        private Dictionary<List<int>, Dictionary<string, List<int>>> TablaEstados = new Dictionary<List<int>, Dictionary<string, List<int>>>();
        private Dictionary<int, List<int>> Follows;
        private ArbolB miArbol;
        private List<string> TerminalesArbol = new List<string>();
        private List<string> Terminales = new List<string>();

        public Estados(Dictionary<int, List<int>> follows, ArbolB miArbol, List<Datos.AllData> simbolosTerminales)
        {
            Follows = follows;
            this.miArbol = miArbol;
            GetNodos(simbolosTerminales);
        }
        public void GetNodos(List<Datos.AllData> ST)
        {
            foreach (var item in ST)
            {
                if (!Utilities.Ter.Contains(item.StringData))
                {
                    this.TerminalesArbol.Add(item.StringData);
                }
            }
            foreach (var item in TerminalesArbol)
            {
                if (!Terminales.Contains(item) && item != "#")
                {
                    Terminales.Add(item);
                }
            }
        }
        public Dictionary<List<int>, Dictionary<string, List<int>>> CrearEstados(ArbolB Arbol)
        {
            Queue<List<int>> EstadosAprobar = new Queue<List<int>>();
            List<List<int>> EstadosHistorial = new List<List<int>>();
            var diccModificar = new Dictionary<string, List<int>>();
            var EstadoInicial = Arbol.First;
            EstadosHistorial.Add(EstadoInicial);
            foreach (var Simbolo in Terminales)
            {
                var ListaConcordancia = new List<int>();
                var ListaFollows = new List<int>();
                foreach (var item in EstadoInicial)
                {
                    var SimbolosArbol = TerminalesArbol[item - 1];//Arbol
                    if (Simbolo == SimbolosArbol)
                    {
                        ListaConcordancia.Add(item);
                    }
                }
                foreach (var SimblosItem in ListaConcordancia)
                {
                    Follows.TryGetValue(SimblosItem, out var follows);
                    ListaFollows.AddRange(follows);
                }
                diccModificar.Add(Simbolo, ListaFollows);
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
                foreach (var Simbolo in Terminales)
                {
                    var ListaConcordancia = new List<int>();
                    var ListaFollows = new List<int>();
                    foreach (var item in Estado)
                    {
                        var SimbolosArbol = TerminalesArbol[item - 1];
                        if (Simbolo == SimbolosArbol)
                        {
                            ListaConcordancia.Add(item);
                        }
                    }
                    foreach (var SimblosItem in ListaConcordancia)
                    {
                        Follows.TryGetValue(SimblosItem, out var follows);
                        ListaFollows.AddRange(follows);
                    }
                    diccModificarNoInicial.Add(Simbolo, ListaFollows);
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
