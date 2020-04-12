
namespace LFA_Proyecto.Arbol
{
    class ArbolB
    {
        public string Dato { get; set; }
        public ArbolB HijoDerecho { get; set; }
        public ArbolB HijoIzquierdo { get; set; }
        public object Root { get; internal set; }

        public int Value;
        public string First;
        public string Last;
        public string Follow;
        public bool Nuller;//? and *
    }
}
