using System;
using LFA_Proyecto.Help;

namespace LFA_Proyecto.Modelos
{
    class Prioridades//Según la Procedencia de Operadores de C#...
    {
        public void Metacaracteres()
        {
            Datos.Instance.Metacaracteres.Add(new String[] { "*", "+", "$", "-", "[", "]", "?", "." });//Metacaracteres
        }
        public void Unarios()
        {
            Datos.Instance.Unarios.Add(new String[] { "*", "+", "|", "?", "." });//Unarios
        }
        public void OperER()
        {
            Datos.Instance.OperadoresER.Add(new string[] { "+", "*", "?", "(", ")", "|" });
        }
    }
}
