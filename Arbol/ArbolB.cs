
namespace LFA_Proyecto.Arbol
{
    class ArbolB
    {
        public string Valores { get; set; }
        public ArbolB HijoDerecho { get; set; }
        public ArbolB HijoIzquierdo { get; set; }
        public int First;
        public int Last;
        public int Follow;
        public bool Nuller;//? and *
    }
}
