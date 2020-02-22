using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFA_Proyecto.Help;

namespace LFA_Proyecto.Modelos
{
    class Prioridades//Según la Procedencia de Operadores de C#...
    {
        public void Metacaracteres()
        {
            Datos.Instance.Metacaracteres.Add(new String[] { "*", "+", "$", "-", "[", "]", "?", "." });
        }
        public void Unarios()
        {
            Datos.Instance.Unarios.Add(new String[] { "*","+","|", "?", "."});//Unarios
        }
    }
}
