using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFA_Proyecto.Modelos
{
    class Prioridades//Según la Procedencia de Operadores de C#...
    {
        public string importancia1 { get; }//Brakets Simbols as ==, ::, ..
        public string importancia2 { get; }//Escaped Characters as /<Special Characters>
        public string importancia3 { get; }//Brackets Expresion as []
        public string importancia4 { get; }//Grouping as ()
        public string importancia5 { get; }//Single -Character- ERE duplication as *, +, ?, {m, n}
        public string importancia6 { get; }//Concatenation
        public string importancia7 { get; }//Anchoring as ^ $
        public string importancia8 { get; }//Alternation |
    }
}
