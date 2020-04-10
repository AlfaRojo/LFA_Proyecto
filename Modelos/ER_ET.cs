using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using LFA_Proyecto.Help;
using LFA_Proyecto.Arbol;

namespace LFA_Proyecto.Modelos//Creador: Ing. Moises Alonso
{
    //  Entradas
    //1.	Tokens de la expresión regular(Símbolos terminales “st”, meta caracteres operadores incluyendo la concatenación “op”)
    //2.	Pila de Tokens llamada “T”
    //3.	Pila de árboles llamada “S”
    //  Salidas
    //1.	Árbol de expresión con el símbolo terminal extendido #

    class ER_ET
    {
        #region Arbol Expresiones
        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            string item { get; set; }
            public bool Leaf => (this.Left == null && this.Right == null);
            public bool Full => (this.Left != null && this.Right != null);
            public Node() { }
            public Node(string value) : this(value, null, null) { }
            public Node(string value, Node Left, Node Right)
            {
                this.item = value;
                this.Left = Left;
                this.Right = Right;
            }
        }
        public class ExpressionTree
        {
            public Node Root { get; set; }
            private Stack<string> PilaT = new Stack<string>();
            private Stack<ExpressionTree> PilaS = new Stack<ExpressionTree>();
            private int lastPrecedence = int.MaxValue;
            private void GetLastPrecedence(string token)
            {
                if (token == "(" || token == ")")
                {
                    lastPrecedence = 4;
                }
                else if (token == "*" || token == "+" || token == "?")
                {
                    lastPrecedence = 3;
                }
                else if (token == ".")
                {
                    lastPrecedence = 2;
                }
                else if (token == "|")
                {
                    lastPrecedence = 1;
                }
            }
            public ExpressionTree()
            {

            }
            /// <summary>
            /// Algoritmo creado por Moises Alonso
            /// Algoritmo de creacion de arbol de expresion a traves de expresion regular
            /// </summary>
            /// <param name="RE"></param>
            public void CreateTree(List<Datos.AllData> RE)
            {
                foreach (var item in RE)
                {
                    int lstPrcdnc = 0;
                    if (item.StringData == "(")
                    {
                        PilaT.Push(item.StringData.ToString());
                        GetLastPrecedence(item.StringData);
                    }
                    else if (item.StringData == ")")
                    {
                        while (PilaT.Count > 0 && PilaT.Peek() != "(")
                        {
                            if (PilaT.Count == 0)
                            {
                                MessageBox.Show("Faltan operadores");
                                return;
                            }
                            if (PilaS.Count < 2)
                            {
                                MessageBox.Show("Faltan operadores");
                                return;
                            }
                            ExpressionTree temp = new ExpressionTree();
                            temp.Root = new Node(PilaT.Pop());
                            ExpressionTree Right = PilaS.Pop();
                            temp.Root.Right = Right.Root;
                            ExpressionTree Left = PilaS.Pop();
                            temp.Root.Left = Left.Root;
                            PilaS.Push(temp);
                        }
                        if (PilaT.Count == 0)
                        {
                            MessageBox.Show("Faltan operadores");
                            return;
                        }
                        PilaT.Pop();
                        GetLastPrecedence(")");
                    }
                    else if (Utilities.Op.Contains(item.StringData))
                    {
                        if (item.StringData == "?" || item.StringData == "*" || item.StringData == "+")
                        {
                            ExpressionTree temp = new ExpressionTree();
                            temp.Root = new Node(item.StringData.ToString());
                            if (PilaS.Count == 0)
                            {
                                MessageBox.Show("Faltan operadores");
                                return;
                            }
                            ExpressionTree Left = new ExpressionTree();
                            Left = PilaS.Pop();
                            temp.Root.Left = Left.Root;
                            PilaS.Push(temp);
                            GetLastPrecedence(item.StringData.ToString());
                        }
                        else if (PilaT.Count > 0 && PilaT.Peek() != "(")
                        {
                            lstPrcdnc = this.lastPrecedence;
                            GetLastPrecedence(item.StringData);

                            if (this.lastPrecedence < lstPrcdnc)
                            {
                                ExpressionTree temp = new ExpressionTree();
                                temp.Root = new Node(PilaT.Pop().ToString());
                                if (PilaS.Count() < 2)
                                {
                                    MessageBox.Show("Faltan operadores");
                                    return; ;
                                }
                                ExpressionTree ctree = PilaS.Pop();
                                temp.Root.Right = ctree.Root;
                                ctree = PilaS.Pop();
                                temp.Root.Left = ctree.Root;
                                PilaS.Push(temp);
                            }
                        }
                        if (item.StringData == "|" || item.StringData == ".")
                        {
                            PilaT.Push(item.StringData);
                            GetLastPrecedence(item.StringData);
                        }
                    }
                    else
                    {
                        ExpressionTree temp = new ExpressionTree();
                        temp.Root = new Node(item.StringData);
                        PilaS.Push(temp);
                    }
                    while (PilaT.Count > 0)
                    {
                        string op = PilaT.Pop();
                        if (op == "(" || PilaS.Count < 2)
                        {
                            MessageBox.Show("Faltan operadores");
                            return;
                        }
                        ExpressionTree temp = new ExpressionTree();
                        temp.Root = new Node(op);
                        ExpressionTree ctree = new ExpressionTree();
                        ctree = PilaS.Pop();
                        temp.Root.Right = ctree.Root;
                        ctree = PilaS.Pop();
                        temp.Root.Left = ctree.Root;
                        PilaS.Push(temp);
                    }
                    if (PilaS.Count != 1)
                    {
                        MessageBox.Show("Faltan operadores");
                        return;
                    }
                    ExpressionTree result = PilaS.Pop();
                    this.Root = result.Root;
                }
            }
        }
        #endregion



        private int GetImport(string eFormato)
        {
            if (eFormato == "(" || eFormato == ")")
            {
                return 4;
            }
            if (eFormato == "*" || eFormato == "?" || eFormato == "+")
            {
                return 3;
            }
            if (eFormato == ".")
            {
                return 2;
            }
            if (eFormato == "|")
            {
                return 1;
            }
            return 0;
        }
        public ArbolB CrearArbol(List<Datos.AllData> listaToken)
        {
            foreach (var item in listaToken)
            {
                var listaAuxiliar = new List<string>();
                listaAuxiliar.Add(item.StringData.Substring(0, 1));
                if (item.StringData == "(")
                {
                    Datos.Instance.PilaT.Push(item.StringData.ToString());
                }
                else if (item.StringData.Contains(")"))
                {
                    while (Datos.Instance.PilaT.Count > 0 && Datos.Instance.PilaT.Peek() != "(")
                    {
                        if (Datos.Instance.PilaT.Count == 0)
                        {
                            MessageBox.Show("Error, faltan operandos");
                            break;
                        }
                        if (Datos.Instance.PilaS.Count < 2)
                        {
                            MessageBox.Show("Error, faltan operandos");
                            break;
                        }
                        var nodoTemp = new ArbolB();
                        nodoTemp.Valores = Datos.Instance.PilaT.Pop();
                        nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        Datos.Instance.PilaS.Push(nodoTemp);
                    }
                    Datos.Instance.PilaT.Pop();
                }
                else if (item.StringData == "." || item.StringData == "*" || item.StringData == "?" || item.StringData == "+" || item.StringData == "|")
                {
                    if (item.StringData.Contains("*") || item.StringData.Contains("?") || item.StringData.Contains("+"))
                    {
                        var nodoTemp = new ArbolB();
                        nodoTemp.Valores = item.StringData;
                        if (Datos.Instance.PilaS.Count <= 0)
                        {
                            MessageBox.Show("Error, faltan operandos");
                            break;
                        }
                        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                        Datos.Instance.PilaS.Push(nodoTemp);
                    }
                    else if (Datos.Instance.PilaT.Count != 0 && !(Datos.Instance.PilaT.Peek().Contains("(")))
                    {
                        var eTok = GetImport(item.StringData);
                        var ePila = GetImport(Datos.Instance.PilaT.Peek());
                        if (eTok <= ePila)
                        {
                            var nodoTemp = new ArbolB();
                            nodoTemp.Valores = item.StringData;
                            if (Datos.Instance.PilaS.Count < 2)
                            {
                                MessageBox.Show("Error, faltan operandos");
                                break;
                            }
                            nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                            nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                            Datos.Instance.PilaS.Push(nodoTemp);
                        }
                    }
                    else if (item.StringData.Contains(".") || item.StringData.Contains("|"))
                    {
                        Datos.Instance.PilaT.Push(item.StringData.ToString());
                    }
                }
                else
                {
                    var nodoTemp = new ArbolB();
                    nodoTemp.Valores = item.StringData.ToString();
                    Datos.Instance.PilaS.Push(nodoTemp);
                }
            }
            while (Datos.Instance.PilaT.Count > 0)
            {
                var nodoTemp = new ArbolB();
                nodoTemp.Valores = Datos.Instance.PilaT.Pop();
                if (nodoTemp.Valores == "(")
                {
                    MessageBox.Show("Error, faltan operandos");
                    return null;
                }
                if (Datos.Instance.PilaS.Count < 2)
                {
                    MessageBox.Show("Error, faltan operandos");
                    return null;
                }
                nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
                nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                Datos.Instance.PilaS.Push(nodoTemp);
            }
            if (Datos.Instance.PilaS.Count != 1)
            {
                MessageBox.Show("Error, faltan operandos");
                return null;
            }
            MessageBox.Show("PilaS");
            return Datos.Instance.PilaS.Pop();
        }
    }
}


