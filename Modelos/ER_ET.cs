using System.Windows.Forms;
using LFA_Proyecto.Help;
using LFA_Proyecto.Arbol;
using System.Collections.Generic;
using System.Linq;

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
		#region Arbol Emma
		class Node
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
		class ExpressionTree
		{
			public Node Root { get; set; }
			private Stack<string> tokens = new Stack<string>();
			private Stack<ExpressionTree> trees = new Stack<ExpressionTree>();
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
			public void CreateTree(List<string> RE)
			{
				int lstPrcdnc = 0;
				for (int i = 0; i < RE.Count; i++)
				{
					if (RE[i] == "(")
					{
						tokens.Push(RE[i].ToString());
						GetLastPrecedence(RE[i]);
					}
					else if (RE[i] == ")")
					{
						while (tokens.Count > 0 && tokens.Peek() != "(")
						{
							if (tokens.Count == 0)
							{
								MessageBox.Show("Faltan operadores");
								return;
							}
							if (trees.Count < 2)
							{
								MessageBox.Show("Faltan operadores");
								return;
							}
							ExpressionTree temp = new ExpressionTree();
							temp.Root = new Node(tokens.Pop());
							ExpressionTree Right = trees.Pop();
							temp.Root.Right = Right.Root;
							ExpressionTree Left = trees.Pop();
							temp.Root.Left = Left.Root;
							trees.Push(temp);
						}
						if (tokens.Count == 0)
						{
							MessageBox.Show("Faltan operadores");
							return;
						}
						tokens.Pop();
						GetLastPrecedence(")");
					}
					else if (Utilities.Op.Contains(RE[i]))	
					{
						if (RE[i] == "?" || RE[i] == "*" || RE[i] == "+")
						{
							ExpressionTree temp = new ExpressionTree();
							temp.Root = new Node(RE[i].ToString());

							if (trees.Count == 0)
							{
								MessageBox.Show("Faltan operadores");
								return;
							}

							ExpressionTree Left = new ExpressionTree();
							Left = trees.Pop();
							temp.Root.Left = Left.Root;

							trees.Push(temp);
							GetLastPrecedence(RE[i].ToString());
						}
						else if (tokens.Count > 0 && tokens.Peek() != "(")
						{
							lstPrcdnc = this.lastPrecedence;
							GetLastPrecedence(RE[i]);

							if (this.lastPrecedence < lstPrcdnc)
							{
								ExpressionTree temp = new ExpressionTree();
								temp.Root = new Node(tokens.Pop().ToString());

								if (trees.Count() < 2)
								{
									MessageBox.Show("Faltan operadores");
									return; ;
								}
								ExpressionTree ctree = trees.Pop();
								temp.Root.Right = ctree.Root;
								ctree = trees.Pop();
								temp.Root.Left = ctree.Root;
								trees.Push(temp);
							}
						}
						if (RE[i] == "|" || RE[i] == ".")
						{
							tokens.Push(RE[i]);
							GetLastPrecedence(RE[i]);
						}
					}
					else
					{
						ExpressionTree temp = new ExpressionTree();
						temp.Root = new Node(RE[i]);

						trees.Push(temp);
					}
				}
				while (tokens.Count > 0)
				{
					string op = tokens.Pop();
					if (op == "(" || trees.Count < 2)
					{
						MessageBox.Show("Faltan operadores");
						return;
					}
					ExpressionTree temp = new ExpressionTree();
					temp.Root = new Node(op);
					ExpressionTree ctree = new ExpressionTree();
					ctree = trees.Pop();
					temp.Root.Right = ctree.Root;
					ctree = trees.Pop();
					temp.Root.Left = ctree.Root;
					trees.Push(temp);
				}
				if (trees.Count != 1)
				{
					MessageBox.Show("Faltan operadores");
					return;
				}
				ExpressionTree result = trees.Pop();
				this.Root = result.Root;
			}
		}
		#endregion


		//private int GetImport(string eFormato)
		//{
		//    if (eFormato == "(" || eFormato == ")")
		//    {
		//        return 4;
		//    }
		//    if (eFormato == "*" || eFormato == "?" || eFormato == "+")
		//    {
		//        return 3;
		//    }
		//    if (eFormato == ".")
		//    {
		//        return 2;
		//    }
		//    if (eFormato == "|")
		//    {
		//        return 1;
		//    }
		//    return 0;
		//}
		//public ArbolB CrearArbol(List<Datos.AllData> listaToken)
		//{
		//    foreach (var item in listaToken[0].StringData)
		//    {
		//        MessageBox.Show("Actual - " + item);
		//        if (item.ToString() == "(")
		//        {
		//            Datos.Instance.PilaT.Push(item.ToString());
		//        }
		//        else if (item.ToString() == ")")
		//        {
		//            while (Datos.Instance.PilaT.Count > 0 && Datos.Instance.PilaT.Peek() != "(")
		//            {
		//                if (Datos.Instance.PilaT.Count == 0)
		//                {
		//                    MessageBox.Show("Error, faltan operandos");
		//                    break;
		//                }
		//                if (Datos.Instance.PilaS.Count < 2)
		//                {
		//                    MessageBox.Show("Error, faltan operandos");
		//                    break;
		//                }
		//                var nodoTemp = new ArbolB();
		//                nodoTemp.Valores = Datos.Instance.PilaT.Pop();
		//                nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
		//                nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
		//                Datos.Instance.PilaS.Push(nodoTemp);
		//            }
		//            Datos.Instance.PilaT.Pop();
		//        }
		//        else if (item.ToString() == "." || item.ToString() == "*" || item.ToString() == "?" || item.ToString() == "+" || item.ToString() == "|")
		//        {
		//            if (item.ToString() == "*" || item.ToString() == "?" || item.ToString() == "+")
		//            {
		//                var nodoTemp = new ArbolB();
		//                nodoTemp.Valores = item.ToString();
		//                if (Datos.Instance.PilaS.Count < 0)
		//                {
		//                    MessageBox.Show("Error, faltan operandos");
		//                    break;
		//                }
		//                nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
		//                Datos.Instance.PilaS.Push(nodoTemp);
		//            }
		//            else if (Datos.Instance.PilaT.Count != 0 && Datos.Instance.PilaT.Peek() != "(")
		//            {
		//                var eTok = GetImport(item.ToString());
		//                var ePila = GetImport(Datos.Instance.PilaT.Peek());
		//                if (eTok <= ePila)
		//                {
		//                    var nodoTemp = new ArbolB();
		//                    nodoTemp.Valores = item.ToString();
		//                    if (Datos.Instance.PilaS.Count < 2)
		//                    {
		//                        MessageBox.Show("Error, faltan operandos");
		//                        break;
		//                    }
		//                    nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
		//                    nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
		//                    Datos.Instance.PilaS.Push(nodoTemp);
		//                }
		//            }
		//            else if (item.ToString() == "." || item.ToString() == "|")
		//            {
		//                Datos.Instance.PilaT.Push(item.ToString());
		//            }
		//        }
		//        else
		//        {
		//            var nodoTemp = new ArbolB();
		//            nodoTemp.Valores = item.ToString();
		//            Datos.Instance.PilaS.Push(nodoTemp);
		//        }
		//    }
		//    while (Datos.Instance.PilaT.Count > 0)
		//    {
		//        var nodoTemp = new ArbolB();
		//        nodoTemp.Valores = Datos.Instance.PilaT.Pop();
		//        if (nodoTemp.Valores == "(")
		//        {
		//            MessageBox.Show("Error, faltan operandos");
		//            return null;
		//        }
		//        if (Datos.Instance.PilaS.Count < 2)
		//        {
		//            MessageBox.Show("Error, faltan operandos");
		//            return null;
		//        }
		//        nodoTemp.HijoDerecho = Datos.Instance.PilaS.Pop();
		//        nodoTemp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
		//        Datos.Instance.PilaS.Push(nodoTemp);
		//    }
		//    if (Datos.Instance.PilaS.Count != 1)
		//    {
		//        MessageBox.Show("Error, faltan operandos");
		//        return null;
		//    }
		//    MessageBox.Show("PilaS");
		//    return Datos.Instance.PilaS.Pop();
		//}
	}
}
    

