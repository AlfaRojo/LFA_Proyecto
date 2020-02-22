using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            while (Datos.Instance.eTOKEN.Count() > 0 )//Mientras existan TOKENS en la expresión regular
            {
                string TokenActual = Datos.Instance.eTOKEN.ElementAt(0);//Obtener TOKEN
                if (TokenActual == Datos.Instance.listaSets.First(X=>X.Contains(TokenActual)))//Sii el Token es SIMBOLO TERMINAL(SETS)
                {
                    var Ensamblando = new ArbolB//Se convierte en arbol
                    {
                        Valores = ""
                    };
                    Datos.Instance.PilaS.Push(Ensamblando);//Se hace Push con el nuevo arbol generado
                }
                else if (TokenActual == "(")//Sii TOKEN es (
                {
                    Datos.Instance.PilaT.Push(TokenActual);//Hacer Push a con el TOKEN (
                }
                if (TokenActual == ")")//Sii TOKEN es (
                {
                    while (Datos.Instance.PilaT.Count() >= 0 && Datos.Instance.PilaT.Peek() != "(")//Mientras la longitud de T sea mayor o igual a 0
                        //y el último dato insertado en T sea diferente de “(“
                    {
                        if (Datos.Instance.PilaT.Count() == 0)//Sii la longitud de la pila es 0
                        {
                            MessageBox.Show("Existe un error, faltan operadores");//Error
                        }
                        if (Datos.Instance.PilaS.Count() < 1)//Sii la longitud de la pila es menor a 2
                        {
                            MessageBox.Show("Existe un error, faltan operadores");//Error
                        }
                        var PopAuxT = Datos.Instance.PilaT.Pop();//Hacer POP a T
                        var PopAuxS = Datos.Instance.PilaS.Pop();//Hacer POP a S
                        var PopAuxS2 = Datos.Instance.PilaS.Pop();//Hacer POP a S
                        var EnsamblandoTemp = new ArbolB
                        {
                            Valores = PopAuxT,//Crear arbol(nodo) del Pop en pila T
                            HijoDerecho = PopAuxS,//Asignar hijos derecho
                            HijoIzquierdo = PopAuxS2//Asignar hijos izquierdo
                        };
                        Datos.Instance.PilaS.Push(EnsamblandoTemp);//Se lleva a la pila
                    }
                    Datos.Instance.PilaT.Pop();//Quitar ultimo elemento de la pila
                }
                if (TokenActual == "*")//Sii el TOKEN es concatenación(*)
                {

                }
            }
        }
    }
}
