using System;
using System.Linq;
using System.Windows.Forms;
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
        public void CodMoe()
        {
            while (Datos.Instance.eTOKEN.Count() > 0)//Mientras existan TOKENS en la expresión regular
            {
                for (int i = 0; i < Datos.Instance.eTOKEN.Count(); i++)
                {
                    string TokenActual = Datos.Instance.eTOKEN.ElementAt(i);//Obtener TOKEN actual
                    try
                    {
                        if (TokenActual == Datos.Instance.listaSets.First(X => X.Contains(TokenActual)))//Sii el Token es SIMBOLO TERMINAL(SETS)
                        {
                            var Ensamblando = new ArbolB//Se convierte en arbol
                            {
                                Valores = ""
                            };
                            Datos.Instance.PilaS.Push(Ensamblando);//Se hace Push con el nuevo arbol generado
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        if (TokenActual == "(")//Sii TOKEN es (
                        {
                            Datos.Instance.PilaT.Push(TokenActual);//Hacer Push a con el TOKEN (
                        }
                    }
                    if (TokenActual == ")")//Sii TOKEN es (
                    {
                        while (Datos.Instance.PilaT.Count() >= 0 && Datos.Instance.PilaT.Peek() != "(")//Mientras la longitud de T sea mayor o igual a 0
                                                                                                       //y el último dato insertado en T sea diferente de “(“
                        {
                            if (Datos.Instance.PilaT.Count() == 0)//Sii la longitud de la pila es 0
                            {
                                MessageBox.Show("Existe un error, faltan operadores");//Error
                                return;
                            }
                            if (Datos.Instance.PilaS.Count() < 1)//Sii la longitud de la pila es menor a 2
                            {
                                MessageBox.Show("Existe un error, faltan operadores");//Error
                                return;
                            }
                            var EnsamblandoTemp = new ArbolB
                            {
                                Valores = Datos.Instance.PilaT.Pop(),//Hacer Pop y Crear arbol(nodo) del Pop en pila T
                                HijoDerecho = Datos.Instance.PilaS.Pop(),//Hacer Pop y Asignar hijos derecho
                                HijoIzquierdo = Datos.Instance.PilaS.Pop()//Hacer Pop y Asignar hijos izquierdo
                            };
                            Datos.Instance.PilaS.Push(EnsamblandoTemp);//Se lleva a la pila
                        }
                        Datos.Instance.PilaT.Pop();//Quitar ultimo elemento de la pila
                    }
                    else if (TokenActual == Datos.Instance.Metacaracteres.First(X => X.Contains(".")).ToString())//Sii el TOKEN es concatenación(.) - Guardado en lista
                    {
                        string[] auxiliar = Datos.Instance.Metacaracteres.First(X => X.Contains("."));//Para usarlo luego
                        for (int j = 0; j < Datos.Instance.Unarios.Count; j++)//Se comprueba en toda la lista de Unarios
                        {
                            int eTOK = GetImport(Datos.Instance.eTOKEN.ElementAt(i));
                            int ePEEK = GetImport(Datos.Instance.PilaT.Peek());
                            if (eTOK == 0 || ePEEK == 0)
                            {
                                MessageBox.Show("Existe un error, faltan operadores");//Error
                                return;
                            }
                            if (auxiliar == Datos.Instance.Unarios[j])//Sii el Metacaracter es Unario
                            {
                                var Ensamblando = new ArbolB//Crearle un nodo
                                {
                                    Valores = auxiliar[0],
                                };
                                if (Datos.Instance.PilaS.Count() < 0)//Si la pila está vacia
                                {
                                    MessageBox.Show("Existe un error, faltan operadores");//Error
                                    return;
                                }
                                Ensamblando.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                                Datos.Instance.PilaS.Push(Ensamblando);
                            }
                            else if (Datos.Instance.PilaT.Count() != 0 && Datos.Instance.PilaT.Peek() != "(" && eTOK < ePEEK)// &TOKEN es menor a ultimo op en T - Datos.Instance.eTOKEN.ElementAt(i-1) < Datos.Instance.PilaT.Peek()
                            {
                                var Temporal = new ArbolB
                                {
                                    Valores = Datos.Instance.PilaT.Pop(),//Hacer Pop y Crear arbol(nodo) del Pop en pila T
                                };
                                if (Datos.Instance.PilaS.Count() < 2)
                                {
                                    MessageBox.Show("Existe un error, faltan operadores");//Error
                                    return;
                                }
                                Temporal.HijoDerecho = Datos.Instance.PilaS.Pop();//Agregar hijo Derecho
                                Temporal.HijoIzquierdo = Datos.Instance.PilaS.Pop();//Agregar hijo Derecho
                                Datos.Instance.PilaS.Push(Temporal);//Agregar nuevo arbol a PilaS
                            }
                            else if (TokenActual != auxiliar.ToString())//Sii op no es unario
                            {
                                Datos.Instance.PilaT.Push(TokenActual);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error! No es un Token reconocido");//Error
                        return;
                    }
                }
            }
            while (Datos.Instance.PilaT.Count() > 0)
            {
                var Temp = new ArbolB
                {
                    Valores = Datos.Instance.PilaT.Pop(),
                };
                if (Temp.Valores == "(")
                {
                    MessageBox.Show("Existe un error, faltan operadores");//Error
                    return;
                }
                if (Datos.Instance.PilaS.Count() != 0)
                {
                    MessageBox.Show("Existe un error, faltan operadores");//Error
                    return;
                }
                Temp.HijoDerecho = Datos.Instance.PilaS.Pop();
                Temp.HijoIzquierdo = Datos.Instance.PilaS.Pop();
                Datos.Instance.PilaS.Push(Temp);
            }
            if (Datos.Instance.PilaS.Count() != 1)
            {
                MessageBox.Show("Existe un error, faltan operadores");//Error
                return;
            }
            Datos.Instance.PilaS.Pop();//Arbol final
        }
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
    }
}
