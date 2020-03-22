using System.Windows.Forms;

namespace LFA_Proyecto.Arbol
{
    class PostOrden
    {
        internal class DataGridViewUpdater
        {
            internal void UpdateDataGridView(DataGridView dv)
            {
                void myPost(ArbolB thisTree)//Recorrido postorden
                {
                    int First = 1;
                    int Last = 1;
                    if (thisTree == null)
                    {
                        return;
                    }
                    else
                    {
                        myPost(thisTree.HijoIzquierdo);
                        myPost(thisTree.HijoDerecho);
                        if (thisTree.HijoDerecho != null || thisTree.HijoIzquierdo != null)//Sii es padre
                        {
                            myPost(thisTree);//No se asignan valores
                        }
                        thisTree.Last = Last;//Asignar
                        thisTree.First = First;//Asignar
                        Last++;
                        First++;
                        dv.Rows.Add(thisTree.Valores);//Mostrar datos
                    }
                }
            }
        }
    }
}
