﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using LFA_Proyecto.Help;
using LFA_Proyecto.Modelos;

namespace LFA_Proyecto
{
    public partial class Form1 : Form
    {
        #region Valores
        string resSETS = "";
        string resTOKENS = "";
        string resACTIONS = "";
        string resERROR = "";
        char[] Delimitadores = { ' ', '\t', '\n', '\r' };
        char[] AlfabetoMayuscula = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RebootList();
            var contArchivo = string.Empty;
            var rutaArchivo = string.Empty;

            using (OpenFileDialog actuArchivo = new OpenFileDialog())
            {
                actuArchivo.InitialDirectory = @"~/LFA_Proyecto/PRUEBAS";
                actuArchivo.Filter = "txt files (*.txt)|*.txt";
                actuArchivo.FilterIndex = 2;
                actuArchivo.RestoreDirectory = true;

                if (actuArchivo.ShowDialog() == DialogResult.OK)
                {
                    rutaArchivo = actuArchivo.FileName;
                    var fileStream = actuArchivo.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))//----------------Lectura del archivo----------------\\
                    {
                        #region EXISTENCIAS SETS/TOKENS/ACTIONS/ERROR
                        if (fileStream.Length == 0)
                        {
                            MessageBox.Show("El archivo no contiene información");
                            RebootList();
                            button1_Click(sender, e);
                        }
                        try//SETS no es obligatorio que venga en el archivo
                        {
                            resSETS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("SETS"));
                            SETlabel.Visible = true;
                            SETlabel.Text = "SETS " + ComprobarString(resSETS);
                        }
                        catch (InvalidOperationException)
                        {
                            SETlabel.Visible = true;
                            SETlabel.Text = "SETS " + ComprobarString(resSETS);
                        }
                        try//TOKENS es obligatorio que venga
                        {
                            resTOKENS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("TOKENS"));
                            TOKENlabel.Visible = true;
                            TOKENlabel.Text = "TOKENS se encuentra en el archivo";
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("El archivo no contiene -TOKENS- o se encuentra mal escrito");
                            RebootList();
                            button1_Click(sender, e);
                        }
                        try//ACTIONS es obligatorio que venga
                        {
                            resACTIONS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ACTIONS"));
                            ACTIONlabel.Visible = true;
                            ACTIONlabel.Text = "ACTIONS se encuentra en el archivo";
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("El archivo no contiene -ACTIONS- o se encuentra mal escrito");
                            RebootList();
                            button1_Click(sender, e);
                        }
                        try//ERROR no es obligatorio que venga en el archivo
                        {
                            resERROR = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ERROR"));
                            ERRORlabel.Visible = true;
                            ERRORlabel.Text = "ERROR " + ComprobarString(resERROR);
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("El archivo no contiene almenos un -ERROR- o se encuentra mal escrito");
                            RebootList();
                            button1_Click(sender, e);
                        }
                        #endregion
                        var lecturaAux = string.Empty;

                        while (reader.Peek() > 0)
                        {
                            lecturaAux = reader.ReadLine();
                            #region All SETS Sintaxis //COMPLETO
                            if (lecturaAux.ToString().Replace("\t", "").Replace(" ", "") == "SETS")
                            {
                                while ((lecturaAux = reader.ReadLine()) != resSETS)
                                {
                                    if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == resACTIONS)//Condicion de salida
                                    {
                                        break;
                                    }
                                    Datos.Instance.listaSets.Add(lecturaAux);
                                }
                                if (Datos.Instance.listaSets.Contains(""))
                                {
                                    Datos.Instance.listaSets.RemoveAll(X => X == "");
                                }
                                if (Datos.Instance.listaSets.Count() < 1)//Sii existe SETS en el archivo, debe tener almenos uno
                                {
                                    MessageBox.Show("Si el archivo contiene SETS, debe de llevar almenos un SET");
                                    RebootList();
                                    button1_Click(sender, e);
                                }
                                for (int i = 0; i < Datos.Instance.listaSets.Count(); i++)//Agregar al GridView
                                {
                                    if (Datos.Instance.listaSets.ElementAt(i).Contains("=") && Datos.Instance.listaSets.ElementAt(i).Contains("'"))
                                    {
                                        string[] SeparadorIgual = Datos.Instance.listaSets.ElementAt(i).Replace(" ", "").Trim(Delimitadores).Split('=');//Hacer split y comprobar derecha
                                        SpliterIgual(SeparadorIgual, sender, e);
                                    }
                                    if (!(Datos.Instance.listaSets.ElementAt(i).Contains("'")))
                                    {
                                        string[] SeparadorChar = Datos.Instance.listaSets.ElementAt(i).Replace(" ", "").Trim(Delimitadores).Split('=');//Hacer split y comprobar derecha
                                        SpliterChar(SeparadorChar, sender, e);
                                    }
                                    if (Datos.Instance.listaSets.ElementAt(i).Contains("+"))//Comprobar concatenación con signo +
                                    {
                                        string[] Delimitado = Datos.Instance.listaSets.ElementAt(i).Split('+');//Hacer split y comprobar derecha e izquierda
                                        SpliterMas(Delimitado, sender, e);
                                    }
                                    this.miDato.Rows.Add(i, Datos.Instance.listaSets.ElementAt(i).Replace(" ", ""), "SETS");
                                }
                            }
                            #endregion
                            #region All TOKENS Sintaxis //Falta comprobar la expresion regular(F)
                            if (lecturaAux.ToString().Replace("( |\t)", "").Replace(" ", "") == "TOKENS")
                            {
                                while ((lecturaAux = reader.ReadLine()) != resTOKENS)
                                {
                                    if (lecturaAux == "SETS" || lecturaAux == resERROR || lecturaAux == resACTIONS)//Condicion de salida
                                    {
                                        break;
                                    }
                                    if (lecturaAux == "\t" || lecturaAux == " ")
                                    {
                                        lecturaAux = reader.ReadLine();
                                    }
                                    Datos.Instance.listaToken.Add(lecturaAux);
                                }
                                if (Datos.Instance.listaToken.Contains(""))
                                {
                                    Datos.Instance.listaToken.RemoveAll(X => X == "");
                                }
                                for (int i = 0; i < Datos.Instance.listaToken.Count(); i++)//Agregar al GridView y comprobar sintaxis
                                {
                                    try
                                    {
                                        var Delimitador = Datos.Instance.listaToken.ElementAt(i).Trim(Delimitadores);
                                        if (Delimitador == "")
                                        {
                                            Datos.Instance.listaToken.RemoveAt(i);
                                        }
                                        string myText = Datos.Instance.listaToken.ElementAt(i);
                                        myText = Regex.Replace(myText, @"\s+", "");
                                        if (!myText.Trim(Delimitadores).Contains("TOKEN"))//Error de escritura
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene TOKEN o se encunetra mal escrito");
                                            RebootList();
                                            button1_Click(sender, e);
                                        }
                                        if (Datos.Instance.listaToken.ElementAt(i).All(char.IsDigit))//No contiene dígito----NO FUNCIONA, REVISAR!
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene numeración válida");
                                            RebootList();
                                            button1_Click(sender, e);
                                        }
                                        if (!Datos.Instance.listaToken.ElementAt(i).Contains("="))//Error de igualación
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene =");
                                            RebootList();
                                            button1_Click(sender, e);
                                        }
                                        //Agregar la comprobación de expresión regular
                                        this.miDato.Rows.Add(i, Datos.Instance.listaToken.ElementAt(i), "TOKENS");
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        i = i - 2;
                                    }
                                }
                            }
                            #endregion
                            #region All ACTIONS Sintaxis //COMPLETO
                            if (lecturaAux.ToString().Replace("( |\t)", "").Replace(" ", "") == "ACTIONS")
                            {
                                lecturaAux = reader.ReadLine();//Siguiente linea
                                if (lecturaAux.ToString().Replace(" ", "").Replace("\t", "") == "RESERVADAS()")//Comprobar que le siga RESERVADAS()
                                {
                                    lecturaAux = reader.ReadLine();//Siguiente linea
                                    if (lecturaAux.ToString().Replace(" ", "").Replace("\t", "") == "{")//Comprobar 3ra linea abra con llave {
                                    {
                                        while ((lecturaAux = reader.ReadLine()) != "}")
                                        {
                                            if (lecturaAux == "}")
                                            {
                                                lecturaAux = reader.ReadLine();
                                                break;
                                            }
                                            if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == "SETS")//Condicion de salida
                                            {
                                                if (Datos.Instance.listaAction.Last().Replace(" ", "").Replace("\t", "") != "}")//Sii el ultimo no es de cerrar llave }
                                                {
                                                    MessageBox.Show("ACTIONS debe finalizar en -}-");
                                                    RebootList();
                                                    button1_Click(sender, e);
                                                }
                                                Datos.Instance.listaAction.RemoveAt(Datos.Instance.listaAction.Count() - 1);//Sii lo contiene, se elimina, inesesario
                                                break;
                                            }
                                            Datos.Instance.listaAction.Add(lecturaAux.Replace(" ", "").Replace("\t", ""));//Agregar a su lista respectiva
                                        }
                                        if (Datos.Instance.listaAction.Contains(""))
                                        {
                                            Datos.Instance.listaAction.RemoveAll(X => X == "");
                                        }
                                        for (int i = 0; i < Datos.Instance.listaAction.Count(); i++)//Agregar al GridView y comprobar Sintaxis
                                        {
                                            string[] compERROR = Datos.Instance.listaAction.ElementAt(i).Split('=');
                                            try
                                            {
                                                if (!(int.TryParse(compERROR[0], out int x)))
                                                {
                                                    MessageBox.Show("ACTIONS en la linea " + i + " no inicia con valor numérico");
                                                    RebootList();
                                                    button1_Click(sender, e);
                                                }
                                                if (!(compERROR[1].StartsWith("'")) && !(compERROR[1].EndsWith("'")))
                                                {
                                                    MessageBox.Show("ACTIONS en la linea " + i + " no inicia o finaliza correctamente");
                                                    RebootList();
                                                    button1_Click(sender, e);
                                                }
                                            }
                                            catch (IndexOutOfRangeException)
                                            {
                                                MessageBox.Show("ACTIONS en la linea " + i + " no inicia correctamente");
                                                RebootList();
                                                button1_Click(sender, e);
                                            }
                                            this.miDato.Rows.Add(i, Datos.Instance.listaAction.ElementAt(i), "ACTIONS");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ACTIONS debe iniciar en -{-");
                                        RebootList();
                                        button1_Click(sender, e);
                                    }
                                }
                                else//Sintaxis incorrecto entre ACTIONS & RESERVADAS()
                                {
                                    MessageBox.Show("ACTIONS debe de ir seguido de RESERVADAS()");
                                    RebootList();
                                    button1_Click(sender, e);
                                }
                            }
                            #endregion
                            #region All ERROR Sintaxis //COMPLETO
                            if (lecturaAux.Contains("ERROR"))
                            {
                                Datos.Instance.listaError.Add(lecturaAux.Replace("\t", "").Replace(" ", ""));
                                while ((lecturaAux = reader.ReadLine()) != resERROR)
                                {
                                    if (lecturaAux == resTOKENS || lecturaAux == resACTIONS || lecturaAux == resSETS || lecturaAux == null)//Condicion de salida
                                    {
                                        if (Datos.Instance.listaError.Count() < 1)
                                        {
                                            MessageBox.Show("Archivo debe contener almenos un ERROR");
                                            RebootList();
                                            button1_Click(sender, e);
                                        }
                                        break;
                                    }
                                    Datos.Instance.listaError.Add(lecturaAux);
                                }
                                if (Datos.Instance.listaError.Contains(""))
                                {
                                    Datos.Instance.listaError.RemoveAll(X => X == "");
                                }
                                for (int i = 0; i < Datos.Instance.listaError.Count(); i++)//Agregar al GridView y comprobar Sintaxis
                                {
                                    string[] compERROR = Datos.Instance.listaError.ElementAt(i).Split('=');
                                    try
                                    {
                                        if (!(compERROR[0].Contains("ERROR")))
                                        {
                                            MessageBox.Show("ERROR en la linea " + i + " no inicia correctamente");
                                            RebootList();
                                            button1_Click(sender, e);
                                        }
                                        if (!(int.TryParse(compERROR[1], out int x)))
                                        {
                                            MessageBox.Show("ERROR en la linea " + i + " no contiene valor numérico");
                                            RebootList();
                                            button1_Click(sender, e);
                                        }
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        MessageBox.Show("ERROR en la linea " + i + " no inicia correctamente");
                                        RebootList();
                                        button1_Click(sender, e);
                                    }
                                    this.miDato.Rows.Add(i, Datos.Instance.listaError.ElementAt(i), "ERROR");
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            rutaLabel.Text = rutaArchivo;
            miDato.Visible = true;
            //NuevoArchivo(contArchivo);
            MessageBox.Show("Archivo leido correctamente", rutaArchivo, MessageBoxButtons.OK);//Solo confirmación visual
        }
        String ComprobarString(string myString)
        {
            if (String.IsNullOrEmpty(myString))
            {
                return " no se encuentra en el archivo";
            }
            return " se encuentra en el archivo";
        }
        private void AgregarDiccionario()//Para que un usuario lo pueda editar posteriormente
        {
            if (Datos.Instance.diccionarioColeccion.Count != 0)
            {
                return;
            }
            else
            {
                Datos.Instance.diccionarioColeccion.Add("SETS", "SETS");
                Datos.Instance.diccionarioColeccion.Add("TOKENS", "TOKENS");
                Datos.Instance.diccionarioColeccion.Add("ACTIONS", "ACTIONS");
                Datos.Instance.diccionarioColeccion.Add("ERROR", "ERROR");
                return;
            }
        }
        private void Form1_Load(object sender, EventArgs e)//Expresiones Regulares generadas manualmente
        {
            string thisTOKEN = ER_TOKEN.Text;
            for (int i = 0; i < thisTOKEN.Length; i++)
            {
                Datos.Instance.eTOKEN.Add(ER_TOKEN.Text.Substring(i, 1));//Guarda caracter por caracter para la ER_ET
            }
            Prioridades myPrior = new Prioridades();
            myPrior.OperER();
            myPrior.Metacaracteres();
            myPrior.Unarios();
        }
        private void button2_Click(object sender, EventArgs e)//Poder cambiar la Expresión Regular manualmente(NO USAR)
        {
            Expresion Expresion = new Expresion();
            Expresion.Show();
        }
        private static IEnumerable<string> GetWordList(string linea)
        {
            return linea.Split(' ').Where(st => !st.Equals(""));
        }//Comprobar cantidad de caracteres en una linea
        private void RebootList()
        {
            Datos.Instance.listaAction.Clear();
            Datos.Instance.listaError.Clear();
            Datos.Instance.listaSets.Clear();
            Datos.Instance.listaToken.Clear();
            miDato.Rows.Clear();
        }//Reiniciar listas sin necesidad de cerrar el programa
        private void SpliterMas(string[] Linea, object sender, EventArgs e)
        {
            for (int i = 0; i < Linea.Length; i++)//Recorrer todo el arreglo en busca de errores de Sintaxis
            {
                if (Linea[i] == "")//Donde encuentre una posición vacia
                {
                    if ((i % 2) == 0)
                    {
                        MessageBox.Show("Antes del signo + debe contener una definición");
                        RebootList();
                        button1_Click(sender, e);
                    }
                    if ((i % 2) != 0)
                    {
                        MessageBox.Show("Después del signo + debe contener una definición");
                        RebootList();
                        button1_Click(sender, e);
                    }
                }
            }
            return;
        }
        private void SpliterIgual(string[] Linea, object sender, EventArgs e)
        {
            if (Linea[1] == "''")
            {
                MessageBox.Show("En " + Linea[0] + ":\nDentro de las comillas debe ir una definición");
                RebootList();
                button1_Click(sender, e);
            }
            string[] splitComillas = Linea[1].Split(new string[] { "'" }, StringSplitOptions.None);
            if ((splitComillas.Count() % 2) == 0)
            {
                MessageBox.Show("En " + Linea[1] + "\ndebe empezar y terminar con comillas");
                RebootList();
                button1_Click(sender, e);
            }
            if (Linea[1].Count() < 2)
            {
                MessageBox.Show("En " + Linea[0] + " debe empezar o terminar con comillas");
                RebootList();
                button1_Click(sender, e);
            }
            return;
        }
        private void SpliterChar(string[] Linea, object sender, EventArgs e)
        {
            if (!Linea[1].Contains("CHR"))
            {
                MessageBox.Show("En " + Linea[0] + " debe empezar con CHR");
                RebootList();
                button1_Click(sender, e);
            }
            else
            {
                string[] SplitParentesis = Linea[1].Split(new string[] { "(", ")" }, StringSplitOptions.None);
                for (int j = 0; j < SplitParentesis.Length; j++)
                {
                    if (SplitParentesis[j].Contains(".."))
                    {
                        SplitParentesis[j] = SplitParentesis[j].Replace("..", "");
                    }
                    if ((j % 2) != 0)
                    {
                        try
                        {
                            if (!(int.TryParse(SplitParentesis[j], out int x)))
                            {
                                MessageBox.Show("SETS en la linea " + j + " no contiene valor numérico encerrado en parentesis");
                                RebootList();
                                button1_Click(sender, e);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            MessageBox.Show("CHR en la linea " + j + " debe contener un valor numérico");
                            RebootList();
                            button1_Click(sender, e);
                        }
                    }
                }
            }

        }
    }
}
